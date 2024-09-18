using Microsoft.AspNetCore.Identity;

namespace PadigalAPI.Models
{
    /// <summary>
    /// Represents a user in the application, inheriting from IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
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
