using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentPortal.Security.Application;
using StudentPortal.UI.Models.Models;
using System.Threading.Tasks;

namespace StudentPortal.UI.Controllers
{
    /// <summary>
    /// Controller For the Logged in student
    /// </summary>
    [Authorize]
    public class MainController : Controller
    {
        #region Action Methods

        /// <summary>
        /// Get action method for main
        /// </summary>
        /// <returns>The main view for the logged in user</returns>
        public IActionResult Index()
        {

            return RedirectToAction("Index","ViewEnrolledCourses");
           
        }

        #endregion
    }
}
