using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Interfaces;
using CommonLayer.ResponseModel;
using System.Data.SqlClient;
using System.Data;
using RepositoryLayer.CustomException;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class EmployeeRL : IEmployeeRL
    {
        EmployeeModel employee;
        List<EmployeeModel> EmployeeModelList;
        static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpDatabase;User Id=mahesh;Password=root";
        static readonly SqlConnection connection = new SqlConnection(connectionString);
        static void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.CONNECTION_FAILED, "connection failed");
                }
            }
        }
        static void CloseConnection()
        {
            if (connection != null && !connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.CONNECTION_FAILED, "connection failed");
                }
            }
        }

        public List<EmployeeModel> GetAllEmployeeDetail()
        {
            return this.GetAllEmployeePayrollData();
        }
        private List<EmployeeModel> GetAllEmployeePayrollData()
        {
            EmployeeModelList = new List<EmployeeModel>();
            SqlCommand command = new SqlCommand("dbo.GetAllEmployeeData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                EstablishConnection();
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    employee = new EmployeeModel();
                    employee.EmployeeID = rd["EmpID"] == DBNull.Value ? default : (int)rd["EmpID"];
                    employee.EmployeeName = rd["EmpName"] == DBNull.Value ? default : (string)rd["EmpName"];
                    employee.Gender = rd["Gender"] == DBNull.Value ? default : (string)rd["Gender"];
                    employee.Salary = rd["Salary"] == DBNull.Value ? default : (decimal)rd["Salary"];
                    employee.StartDate = rd["StartDate"] == DBNull.Value ? default : (DateTime)rd["StartDate"];
                    employee.Notes = rd["Notes"] == DBNull.Value ? default : (string)rd["Notes"];
                    employee.Department = rd["DepartmentNames"] == DBNull.Value ? default : ((string)rd["DepartmentNames"]).Split(",");
                    EmployeeModelList.Add(employee);
                }
                rd.Close();
                if (EmployeeModelList == null)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.NO_DATA_FOUND, "no data found");
                }
                return EmployeeModelList.OrderBy(Emp => Emp.EmployeeID).ToList();
            }
            catch (SqlException)
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); ;
                }
            }
            return null;
        }

        public EmployeeModel RegisterEmployeeData(EmployeeModel employee)
        {
            try
            {
                EstablishConnection();
                SqlCommand cmd = new SqlCommand("dbo.InsertEmployeeRecord", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                string DepartmentNames = employee.Department == null ? "" : string.Join(",", employee.Department);
                cmd.Parameters.AddWithValue("@Department",  DepartmentNames);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                SqlDataReader rd =  cmd.ExecuteReader();
                employee = new EmployeeModel();
                if (rd.Read())
                {
                    employee.EmployeeID = rd["EmpID"] == DBNull.Value ? default : (int)rd["EmpID"];
                    employee.EmployeeName = rd["EmpName"] == DBNull.Value ? default : (string)rd["EmpName"];
                    employee.Gender = rd["Gender"] == DBNull.Value ? default : (string)rd["Gender"];
                    employee.Salary = rd["Salary"] == DBNull.Value ? default : (decimal)rd["Salary"];
                    employee.StartDate = rd["StartDate"] == DBNull.Value ? default : (DateTime)rd["StartDate"];
                    employee.Notes = rd["Notes"] == DBNull.Value ? default : (string)rd["Notes"];
                    employee.Department = rd["DepartmentNames"] == DBNull.Value ? default : ((string)rd["DepartmentNames"]).Split(",");
                }
                else
                {
                    employee = null;
                }
                rd.Close();
                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                try
                {
                    CloseConnection();
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.Message); ;
                }
            }
            return null;
        }
        public EmployeeModel UpdateEmployeeData(EmployeeModel employee, int ID)
        {
            try
            {
                EstablishConnection();
                SqlCommand cmd = new SqlCommand("dbo.UpdateEmployeeRecord", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpID", ID);
                cmd.Parameters.AddWithValue("@EmpName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Department", string.Join(",", employee.Department));
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                SqlDataReader rd = cmd.ExecuteReader();
                employee = new EmployeeModel();
                if (rd.Read())
                {
                    employee.EmployeeID = rd["EmpID"] == DBNull.Value ? default : (int)rd["EmpID"];
                    employee.EmployeeName = rd["EmpName"] == DBNull.Value ? default : (string)rd["EmpName"];
                    employee.Gender = rd["Gender"] == DBNull.Value ? default : (string)rd["Gender"];
                    employee.Salary = rd["Salary"] == DBNull.Value ? default : (decimal)rd["Salary"];
                    employee.StartDate = rd["StartDate"] == DBNull.Value ? default : (DateTime)rd["StartDate"];
                    employee.Notes = rd["Notes"] == DBNull.Value ? default : (string)rd["Notes"];
                    employee.Department = rd["DepartmentNames"] == DBNull.Value ? default : ((string)rd["DepartmentNames"]).Split(",");
                }
                else
                {
                    employee = null;
                }
                rd.Close();
                return employee;
            }
            catch (Exception )
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.Message); ;
                }
            }
            return null;
        }

        public EmployeeModel ReturnSpecificRecord(int ID)
        {
            employee = new EmployeeModel();
            SqlCommand command = new SqlCommand("dbo.GetSpecificEmployeeData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                EstablishConnection();
                command.Parameters.AddWithValue("@EmpID", ID);
                SqlDataReader rd = command.ExecuteReader();
                if (rd.Read())
                {
                    employee.EmployeeID = rd["EmpID"] == DBNull.Value ? default : (int)rd["EmpID"];
                    employee.EmployeeName = rd["EmpName"] == DBNull.Value ? default : (string)rd["EmpName"];
                    employee.Gender = rd["Gender"] == DBNull.Value ? default : (string)rd["Gender"];
                    employee.Salary = rd["Salary"] == DBNull.Value ? default : (decimal)rd["Salary"];
                    employee.StartDate = rd["StartDate"] == DBNull.Value ? default : (DateTime)rd["StartDate"];
                    employee.Notes = rd["Notes"] == DBNull.Value ? default : (string)rd["Notes"];
                    employee.Department = rd["DepartmentNames"] == DBNull.Value ? default : ((string)rd["DepartmentNames"]).Split(",");
                }
                rd.Close();
                if (employee == null)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.NO_DATA_FOUND, "no data found");
                }
                return employee;
            }
            catch (SqlException)
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); ;
                }
            }
            return null;
        }

        public EmployeeModel DeleteSpecificEmployeeData(int ID)
        {
            EmployeeModel employee = new EmployeeModel();
            try
            {
                EstablishConnection();
                SqlCommand cmd = new SqlCommand("dbo.DeleteEmployeeRecord", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpID", ID);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    employee.EmployeeID = rd["EmpID"] == DBNull.Value ? default : (int)rd["EmpID"];
                    employee.EmployeeName = rd["EmpName"] == DBNull.Value ? default : (string)rd["EmpName"];
                    employee.Gender = rd["Gender"] == DBNull.Value ? default : (string)rd["Gender"];
                    employee.Salary = rd["Salary"] == DBNull.Value ? default : (decimal)rd["Salary"];
                    employee.StartDate = rd["StartDate"] == DBNull.Value ? default : (DateTime)rd["StartDate"];
                    employee.Notes = rd["Notes"] == DBNull.Value ? default : (string)rd["Notes"];
                    employee.Department = rd["DepartmentNames"] == DBNull.Value ? default : ((string)rd["DepartmentNames"]).Split(",");
                }
                else
                {
                    employee = null;
                }
                rd.Close();
                return employee;
            }
            catch (Exception)
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.Message); ;
                }
            }
            return null;
        }
    }
}
