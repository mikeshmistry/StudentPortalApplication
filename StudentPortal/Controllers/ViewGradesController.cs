using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentPortal.BL.BusinessClasses;
using StudentPortal.Security.Application;
using StudentPortal.UI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortal.UI.Controllers
{
    /// <summary>
    /// Controller class for viewing grades
    /// </summary>
    [Authorize]
    public class ViewGradesController : Controller
    {
        #region Fields

        /// <summary>
        /// Field to the logger
        /// </summary>
        private readonly ILogger<ViewGradesController> _logger;

        /// <summary>
        /// Field to the student business object
        /// </summary>
        private readonly Student studentBusinessObject;

        private ApplicationUserManager _applicationUserManager;

        #endregion

        #region Constructors



        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="logger">The logger to use</param>
        public ViewGradesController(ILogger<ViewGradesController> logger, ApplicationUserManager userManager)
        {
            _logger = logger;
            studentBusinessObject = new Student();
            _applicationUserManager = userManager;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Get action to the default 
        /// </summary>
        /// <returns>A view back to the view grades view</returns>
        public async Task<IActionResult> Index()
        {
            var student = new StudentModel();
            var studentId = await GetStudentId();

            student.Grades = await studentBusinessObject.GetGradesAsync(studentId);
            return View("Views/Grades/ViewGrades.cshtml", student);
        }

        #endregion

        #region Non Action Methods

        /// <summary>
        /// Non action method to retrieve the logged in students studentId 
        /// </summary>
        /// <returns>A the studentId</returns>
        private async Task<int> GetStudentId()
        {
            var studentId = 0;
            //get the user
            var user = await _applicationUserManager.GetUserAsync(User);

            //found the user
            if (user != null)
                studentId = _applicationUserManager.GetStudentId(user);

            return studentId;
        }

        #endregion 

    }
}
