using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PadigalAPI.Models;
using PadigalAPI.Services;

namespace PadigalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            ITokenService tokenService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _emailService = emailService;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user and sends a confirmation email.
        /// </summary>
        /// <param name="model">The registration model containing user details.</param>
        /// <returns>A response indicating whether the registration was successful.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Registration failed for email {Email}. Errors: {Errors}", model.Email, result.Errors);
                return BadRequest(result.Errors);
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(user, "viewer"); // TODO: Cambiar default role assignment

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

            _logger.LogInformation("User registered successfully. Confirmation email sent to {Email}.", model.Email);
            return Ok("User registered successfully. Please check your email to confirm your account.");
        }

        /// <summary>
        /// Confirms the user's email using a confirmation token.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>A response indicating whether the email was confirmed successfully.</returns>
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Email confirmation failed: User not found. UserId: {UserId}", userId);
                return BadRequest("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogInformation("Email confirmed successfully for UserId: {UserId}", userId);
                return Ok("Email confirmed successfully");
            }

            _logger.LogWarning("Email confirmation failed for UserId: {UserId}", userId);
            return BadRequest("Email confirmation failed");
        }

        /// <summary>
        /// Resends the email confirmation link to the user if their email is not confirmed.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A response indicating whether the email was resent successfully.</returns>
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Resend confirmation email failed: User not found. Email: {Email}", email);
                return NotFound("User not found.");
            }

            if (user.EmailConfirmed)
            {
                _logger.LogWarning("Resend confirmation email failed: Email already confirmed. Email: {Email}", email);
                return BadRequest("Email is already confirmed.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Auth",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

            _logger.LogInformation("Confirmation email resent to {Email}.", email);
            return Ok("Confirmation email resent. Please check your email.");
        }

        /// <summary>
        /// Logs in a user and generates a JWT token if the email is confirmed.
        /// </summary>
        /// <param name="request">The login request model containing email and password.</param>
        /// <returns>A response with the JWT token if login is successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Login failed for email {Email}. Invalid email or password.", request.Email);
                return Unauthorized("Invalid email or password.");
            }

            if (!user.EmailConfirmed)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                _logger.LogInformation("Email not confirmed. A new confirmation email sent to {Email}.", user.Email);
                return Unauthorized("Email not confirmed. A new confirmation email has been sent. Please confirm your email before logging in.");
            }

            var token = _tokenService.GenerateToken(user);
            _logger.LogInformation("User logged in successfully. Email: {Email}", request.Email);
            return Ok(new { Token = token });
        }
    }
}
