using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CommonLayer.ResponseModel
{
   public class EmployeeModel
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string ProfileImage { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
        public string[] Department { get; set; }
        public string Notes { get; set; }

    }
}
