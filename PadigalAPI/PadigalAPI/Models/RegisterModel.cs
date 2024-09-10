namespace PadigalAPI.Models
{
    /// <summary>
    /// Represents a registration model for creating a new user.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
