﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190929060202_SupersetWorkoutExercise")]
    partial class SupersetWorkoutExercise
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseType");

                    b.Property<int>("MuscleType");

                    b.Property<string>("Name");

                    b.Property<int>("Utility");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.FitnessProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationUserId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.MuscleExercises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MuscleType");

                    b.Property<int?>("WorkoutHistoryId");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutHistoryId");

                    b.ToTable("MuscleExercises");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Reps");

                    b.Property<double>("Rest");

                    b.Property<int?>("WorkoutExerciseId");

                    b.Property<int?>("WorkoutExerciseId1");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutExerciseId");

                    b.HasIndex("WorkoutExerciseId1");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DaysType");

                    b.Property<int>("TemplateType");

                    b.Property<int>("TrainerLevelType");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("MuscleExercisesId");

                    b.Property<string>("Name");

                    b.Property<string>("SupersetName");

                    b.HasKey("Id");

                    b.HasIndex("MuscleExercisesId");

                    b.ToTable("WorkoutExercises");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutHistories");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.YoutubeVideoQuery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("LinkId");

                    b.Property<string>("Query");

                    b.HasKey("Id");

                    b.ToTable("YoutubeVideoQueries");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutGenerator.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.FitnessProgram", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("FitnessPrograms")
                        .HasForeignKey("ApplicationUserId")
                        .HasConstraintName("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutGenerator.Data.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.MuscleExercises", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.WorkoutHistory")
                        .WithMany("MuscleExercises")
                        .HasForeignKey("WorkoutHistoryId");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Set", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.WorkoutExercise")
                        .WithMany("Sets")
                        .HasForeignKey("WorkoutExerciseId");

                    b.HasOne("WorkoutGenerator.Data.WorkoutExercise")
                        .WithMany("SupersetSets")
                        .HasForeignKey("WorkoutExerciseId1");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Workout", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.Template", "Template")
                        .WithMany("Workouts")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutExercise", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.MuscleExercises")
                        .WithMany("Exercises")
                        .HasForeignKey("MuscleExercisesId");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutHistory", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.Workout", "Workout")
                        .WithMany("WorkoutHistories")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
