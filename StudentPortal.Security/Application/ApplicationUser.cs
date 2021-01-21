using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Security.Application
{
    /// <summary>
    /// Class to represent an user
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        #region Properties
        
        /// <summary>
        /// The Student Id of the student
        /// </summary>
        [Required]
        public int StudentId { get; set; } 

        #endregion 

    }
}
