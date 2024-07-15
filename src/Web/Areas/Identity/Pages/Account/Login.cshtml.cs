using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Identity;
using Core;
using Microsoft.Extensions.Options;

namespace Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager, IOptions<LoginOptions> options)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            Options = options.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        public string ErrorMessage { get; set; }

        public LoginOptions Options { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
            _logger.LogInformation("Trying to login.");

            var user = await _userManager.FindByNameAsync(Input.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, true);

            if (result.Succeeded)
            {
                DeleteLoginAttemptsFromCookie();
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                DeleteLoginAttemptsFromCookie();
                _logger.LogWarning("User account locked out.");
                ModelState.AddModelError(string.Empty, "You are blocked. Wait 5 minutes and try again.");
                return Page();
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed. Confirm your email.");
                return Page();
            }

            SetLoginAttemptToCookie();
            return Page();
        }

        private void SetLoginAttemptToCookie()
        {
            int loginAttempt = 1;
            int loginAttemptsLeft;

            if (HttpContext.Request.Cookies.ContainsKey(nameof(Options.MaxFailedAccessAttempts)))
            {
                int loginAttemptFromCookie = Int32.Parse(Request.Cookies[nameof(Options.MaxFailedAccessAttempts)]!);
                loginAttemptsLeft = loginAttemptFromCookie - loginAttempt;
            }
            else
            {
                loginAttemptsLeft = Options.MaxFailedAccessAttempts - loginAttempt;
            }

            Response.Cookies.Append(nameof(Options.MaxFailedAccessAttempts), loginAttemptsLeft.ToString());
            ModelState.AddModelError(string.Empty, $"Invalid login attempt. Attempts left: {loginAttemptsLeft}.");
        }

        private void DeleteLoginAttemptsFromCookie()
        {
            if (HttpContext.Request.Cookies.ContainsKey(nameof(Options.MaxFailedAccessAttempts)))
            {
                HttpContext.Response.Cookies.Delete(nameof(Options.MaxFailedAccessAttempts));
            }
        }
    }
}
