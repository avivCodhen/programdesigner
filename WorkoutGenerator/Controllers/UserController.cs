using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WorkoutGenerator.Data;
using WorkoutGenerator.Models;

namespace WorkoutGenerator.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public UserController(SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> RegisterAsync(RegisterViewModel vm)
        {
            var returnUrl = vm.ReturnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = vm.Email, Email = vm.Email};
                var result = await _signInManager.UserManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    var code = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new {area="Identity",userId = user.Id, code = code},
                        protocol: Request.Scheme);


                    await _emailSender.SendEmailAsync(vm.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            return Ok();
        }
    }
}