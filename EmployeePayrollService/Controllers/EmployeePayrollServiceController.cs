using BusinessLayer.Interfaces;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeePayrollWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeePayrollController : ControllerBase
    {
        IEmployeeBL employeeBL;
        List<EmployeeModel> employees;
        public EmployeePayrollController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        [HttpGet]
        public ActionResult GetAllEployeesData()
        {
            employees = employeeBL.GetAllEployeesData();
            return Ok(new { Message = "Successful", data = employees });
        }

        [HttpGet("{ID}")]
        public ActionResult ReturnSpecificRecord(int ID)
        {
            try
            {
                EmployeeModel employee = employeeBL.ReturnSpecificRecord(ID);
                if (employee != null)
                {
                    return Ok(new { Message = "Successful", data = employee });
                }
                else
                    return BadRequest(new { success = false, Message = "employee id doesn't exist" });
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, exception.Message });
            }            
        }

        [HttpPost("Register")]
        public ActionResult RegisterEmployeeData(EmployeeModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeModel result = employeeBL.RegisterEmployeeData(employee);

                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "employee registered successfully", data = result });
                    }
                    else
                        return this.BadRequest(new { success = false, Message = "employee register unsuccessfull" });
                }
                else
                    throw new Exception("Model is not valid");
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, exception.Message });
            }            
        }

        [HttpPut("Update/{ID}")]
        public ActionResult UpdateEmployeeData(EmployeeModel employee, int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeModel result = employeeBL.UpdateEmployeeData(employee, ID);

                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "employee updated successfully", data = result });
                    }
                    else
                        return this.BadRequest(new { success = false, Message = "employee update unsuccessfull" });
                }
                else
                    throw new Exception("Model is not valid");
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete("Delete/{ID}")]
        public ActionResult DeleteSpecificRecord(int ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeModel result = employeeBL.DeleteSpecificEmployeeData(ID);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "employee deleted successfully", data = result });
                    }
                    else
                        return this.BadRequest(new { success = false, Message = "employee delete unsuccessfull" });
                }
                else
                    throw new Exception("Model is not valid");
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
