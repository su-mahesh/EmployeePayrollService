using System.Collections.Generic;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using CommonLayer.ResponseModel;
using System;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        Cloudinary cloudinary;
        Account myAccount = new Account { ApiKey = "115989138851348", ApiSecret = "FeABSr0I4fJnihaBgRdTTZtqw2c", Cloud = "cloud-mahesh" };
        IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
            cloudinary = new Cloudinary(myAccount);
        }
        public List<EmployeeModel> GetAllEployeesData()
        {
            try
            {
                return employeeRL.GetAllEmployeeDetail();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeModel RegisterEmployeeData(EmployeeModel employee)
        {
            try
            {
                string Baseb64 = (employee.ProfileImage);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(employee.EmployeeName, Baseb64),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "EmployeePayroll"
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                employee.ProfileImage = uploadResult.Url.AbsoluteUri;

                return employeeRL.RegisterEmployeeData(employee);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
         
        public EmployeeModel UpdateEmployeeData(EmployeeModel employee, int ID)
        {
            try
            {
                return employeeRL.UpdateEmployeeData(employee, ID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public EmployeeModel ReturnSpecificRecord(int ID)
        {
            try
            {
                return employeeRL.ReturnSpecificRecord(ID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public EmployeeModel DeleteSpecificEmployeeData(int ID)
        {
            try
            {
                return employeeRL.DeleteSpecificEmployeeData(ID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
