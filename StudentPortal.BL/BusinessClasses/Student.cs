using Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StudentPortal.UI.Models.Models;

namespace StudentPortal.BL.BusinessClasses
{
    /// <summary>
    /// Business class to represent a student
    /// </summary>
    public class Student : BusinessBase
    {
        #region Field

        /// <summary>
        /// Field for the repository 
        /// </summary>
        private readonly StudentRepository studentRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Student() : base()
        {
            studentRepository = new StudentRepository(StudentGradingContext);
        }

        #endregion


        #region Business Logic Methods

        /// <summary>
        /// Method to Add a student to the database
        /// </summary>
        /// <param name="student">The student to be added</param>
        /// <returns>True if the student was added</returns>
        public async Task<bool> AddStudentAsync(StudentModel student)
        {
            var added = true;


            try
            {
                var convertedStudent = CreateStudentEnitity(student);
                await Task.Run(() => studentRepository.Add(convertedStudent));
            }
            catch (Exception ex)
            {
                //TODO: add login
                added = false;
            }

            return added;
        }

        /// <summary>
        /// Method to check if a student is registered
        /// </summary>
        /// <param name="student">The student to check</param>
        /// <returns>True if the student is registered</returns>
        public async Task<bool> IsRegisteredAsync(StudentModel student)
        {
            var isRegistered = false;
            try
            {
                var foundStudent = await FindStudentAsync(student.StudentId);

                //student is found the id matched and first and last names match
                if (foundStudent != null && foundStudent.FirstName == student.FirstName && foundStudent.LastName == student.LastName)
                    isRegistered = true;

            }
            catch(Exception ex)
            {

            }

            return isRegistered;
        }

        /// <summary>
        /// Method to find a student by the id 
        /// </summary>
        /// <param name="id">The student id for the student to find</param>
        /// <returns>A student if found. Null otherwise</returns>
        public async Task<StudentModel> FindStudentAsync(int id)
        {
            var student = new StudentModel();
            try
            {
                //find the student
                var foundStudent = await Task.Run(() => studentRepository.GetId(id));

                if (foundStudent != null)
                {
                    //add studentId if found
                    if (foundStudent.StudentId != 0)
                        student.StudentId = foundStudent.StudentId;

                    student.FirstName = foundStudent.FirstName;
                    student.LastName = foundStudent.LastName;
                }
            }
            catch (Exception ex)
            {

            }

            return student;
        }

        /// <summary>
        /// Method to delete a student
        /// </summary>
        /// <param name="studentId">the student to be deleted</param>
        /// <returns>True if the student was deleted</returns>
        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var deleted = true;

            try
            {
                await Task.Run(() => studentRepository.DeleteStudent(studentId));
            }
            catch (Exception ex)
            {
                deleted = false;
            }

            return deleted;
        }

        /// <summary>
        /// Method to Update a student
        /// </summary>
        /// <param name="student">The student to update</param>
        /// <returns>True if the student was updated</returns>
        public async Task<bool> UpdateStudentAsync(StudentModel student)
        {
            var updated = true;

            try
            {
                var convertedStudent = CreateStudentEnitity(student);
                await Task.Run(() => studentRepository.Update(convertedStudent));
            }
            catch (Exception ex)
            {
                updated = false;
            }

            return updated;
        }

        /// <summary>
        /// Method to get all students 
        /// </summary>
        /// <returns>A list of all students</returns>
        public async Task<List<StudentModel>> GetAllStudentAsync()
        {
            List<StudentModel> studentList = new List<StudentModel>();

            try
            {
                var allStudents = await Task.Run(() => studentRepository.GetAll());

                studentList = (from student in allStudents
                               select new StudentModel()
                               {
                                   StudentId = student.StudentId,
                                   FirstName = student.FirstName,
                                   LastName = student.LastName,
                                   FullName = student.FirstName + " " + student.LastName

                               }).ToList<StudentModel>();

            }

            catch (Exception ex)
            {

            }



            return studentList;
        }

        /// <summary>
        /// Method to return all the course that the student is enrolled in
        /// </summary>
        /// <param name="studentId">The id of the student</param>
        /// <returns>A list of enrolled courses</returns>
        public async Task<List<CourseModel>> GetEnrolledCoursesAsync(int studentId)
        {
            //returned list of enrolled course
            List<CourseModel> courseList = new List<CourseModel>();

            try
            {
                List<Entities.Course> courses = await Task.Run(() => studentRepository.GetEnrolledCourses(studentId));

                //convert to course Model
                courseList = (from course in courses
                              select new CourseModel
                              {
                                  CourseId = course.CourseId,
                                  Name = course.Name,
                                  Description = course.Description
                                  
                              }).ToList<CourseModel>();
  
            }
            catch(Exception ex)
            {

            }

            return courseList;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to convert a student to an student entity
        /// </summary>
        /// <param name="student">The Student to convert</param>
        /// <returns>A student entity object</returns>
        private Entities.Student CreateStudentEnitity(StudentModel student)
        {
            var convertedStudent = new Entities.Student();

            if (student != null)
            {
                //add if the student id is not 0
                if (student.StudentId != 0)
                    convertedStudent.StudentId = student.StudentId;

                convertedStudent.FirstName = student.FirstName;
                convertedStudent.LastName = student.LastName;
            }
            return convertedStudent;
        }

        #endregion
    }
}

