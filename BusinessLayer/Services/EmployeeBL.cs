using System.Collections.Generic;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using CommonLayer.ResponseModel;
using System;

namespace BusinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }
        public List<EmployeeModel> GetAllEployeesData()
        {
            return employeeRL.GetAllEmployeeDetail();
        }

        public EmployeeModel RegisterEmployeeData(EmployeeModel employee)
        {
            try
            {
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
