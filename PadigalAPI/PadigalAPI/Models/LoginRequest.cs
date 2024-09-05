namespace PadigalAPI.Models
{
    /// <summary>
    /// Represents a login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
