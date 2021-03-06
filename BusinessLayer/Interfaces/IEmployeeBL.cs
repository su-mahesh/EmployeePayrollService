using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeBL
    {
        public List<EmployeeModel> GetAllEployeesData();
        public EmployeeModel RegisterEmployeeData(EmployeeModel employee);
        public EmployeeModel UpdateEmployeeData(EmployeeModel employee, int ID);
        public EmployeeModel ReturnSpecificRecord(int ID);
        public EmployeeModel DeleteSpecificEmployeeData(int iD);
    }
}
