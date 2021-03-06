using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRL
    {
        public List<EmployeeModel> GetAllEmployeeDetail();
        public EmployeeModel RegisterEmployeeData(EmployeeModel employee);
        public EmployeeModel UpdateEmployeeData(EmployeeModel employee, int ID);
        public EmployeeModel ReturnSpecificRecord(int iD);
        public EmployeeModel DeleteSpecificEmployeeData(int iD);
    }
}
