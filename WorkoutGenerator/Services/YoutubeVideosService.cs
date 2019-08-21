using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Services
{
    public class YoutubeVideosService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;

        public YoutubeVideosService(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration.GetSection("IHostedServices");
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = _configuration.GetSection(GetType().Name);
            if (config.GetValue<bool>("enabled"))
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var youtubeService = new YouTubeService(new BaseClientService.Initializer {
                        ApiKey = config.GetValue<string>("key"),
                        ApplicationName = "workoutgenerator"
                    });

                    var exercises = db.Exercises.ToList();
                    var youtubeQueries = db.YoutubeVideoQueries.Select(x=>x.Query).ToList();
                    var leftovers = exercises.Select(x => x.Name).Except(youtubeQueries);
                    var searchListRequest = youtubeService.Search.List("snippet");

                    foreach (string exercise in leftovers)
                    {
                        searchListRequest.Q = $"How to {exercise}";
                        searchListRequest.MaxResults = 1;
                        SearchListResponse searchListResponse;
                        try
                        {
                            searchListResponse = await searchListRequest.ExecuteAsync(cancellationToken);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Thread.Sleep(1000* 60);
                            await StartAsync(cancellationToken);
                            return;
                        }
                        db.YoutubeVideoQueries.Add(new YoutubeVideoQuery
                        {
                            Query = exercise,
                            LinkId = searchListResponse.Items.First().Id.VideoId
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
