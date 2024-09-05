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

        public AuthController(UserManager<ApplicationUser> userManager, IEmailService emailSender, ITokenService tokenService)
        {
            _userManager = userManager;
            _emailService = emailSender;
            _tokenService = tokenService;

        }

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
                return BadRequest(result.Errors);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

            return Ok("User registered successfully. Please check your email to confirm your account.");
        }


        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("User not found");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }
            return BadRequest("Email confirmation failed");
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.EmailConfirmed)
            {
                return BadRequest("Email is already confirmed.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Auth",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

            return Ok("Confirmation email resent. Please check your email.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!user.EmailConfirmed)
            {
                // Enviar nuevo correo de confirmación
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                return Unauthorized("Email not confirmed. A new confirmation email has been sent. Please confirm your email before logging in.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }


    }

}
