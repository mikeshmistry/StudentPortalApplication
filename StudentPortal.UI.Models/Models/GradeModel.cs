using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPortal.UI.Models.Models
{
    /// <summary>
    /// Model to represent a grade
    /// </summary>
   public class GradeModel
    {

            #region Properties

            /// <summary>
            /// GradeId
            /// </summary>
            public int GradeId { get; set; }

            /// <summary>
            /// Grade
            /// </summary>
            public string LetterGrade { get; set; }

        
            /// <summary>
            /// Student assigned to the grade 
            /// </summary>
            public StudentModel Student { get; set; }


            /// <summary>
            /// Course associated with the grade
            /// </summary>
            public CourseModel Course { get; set; }

            #endregion


        }
    }
