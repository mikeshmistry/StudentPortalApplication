using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using StudentPortal.BL.BusinessClasses;
using StudentPortal.Security.Application;
using StudentPortal.UI.Models.Models;
using StudentPortal.UI.Models.SecurityModels;

namespace StudentPortal.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private Student studentBusinessObject;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            studentBusinessObject = new Student();
        }

        [BindProperty]
        public RegisterUserModel RegisterUserModel { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }



        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
       
            if (ModelState.IsValid)
            {

                //verify that the student exists
                var student = new StudentModel() { StudentId = RegisterUserModel.StudentId, FirstName = RegisterUserModel.FirstName, LastName = RegisterUserModel.LastName };
               
                
                
                var isRegistered = await studentBusinessObject.IsRegisteredAsync(student);

                //user is registered 
                if (isRegistered)
                {
                    var user = new ApplicationUser { UserName = RegisterUserModel.UserName, Email = RegisterUserModel.Email, StudentId = student.StudentId};
                    var result = await _userManager.CreateAsync(user, RegisterUserModel.Password);

                    if (result.Succeeded)
                    {
                       
                        _logger.LogInformation("User created a new account with password.");
                        return LocalRedirect(returnUrl);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    //student is not registered 
                    ModelState.AddModelError(string.Empty, "You are not registered as a student");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
