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
    /// Controller class for viewing enrolled courses
    /// </summary>
    [Authorize]
    public class ViewEnrolledCoursesController : Controller
    {
        #region Fields

        /// <summary>
        /// Field to the logger
        /// </summary>
        private readonly ILogger<ViewEnrolledCoursesController> _logger;

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
        public ViewEnrolledCoursesController(ILogger<ViewEnrolledCoursesController> logger,ApplicationUserManager userManager)
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
        /// <returns>A view back to the view enrolled courses view</returns>
        public async Task<IActionResult> Index()
        {
            var student = new StudentModel();
            await GetEnrolledCoursesAsync(student);

            return View("Views/EnrolledCourses/ViewEnrolledCourses.cshtml",student);
        }

        #endregion

        #region Non Action Methods

        /// <summary>
        /// Gets all enrolled courses for a student
        /// </summary>
        /// <param name="student">The student to get the course for</param>
        private async Task GetEnrolledCoursesAsync(StudentModel student)
        {
            //Get the student Id
            var foundStudentId =  await GetStudentId();
            student.Courses = await studentBusinessObject.GetEnrolledCoursesAsync(foundStudentId);
        }


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
