using Microsoft.AspNetCore.Mvc;
using UnitTest_Demo.Model;

namespace UnitTest_Demo.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _employees = new List<Employee>
                    {
                        new Employee { EmployeeId = 1, Name = "John", PhoneNumber = "1234567890", EmailId = ""},
                        new Employee { EmployeeId = 2, Name = "Jane", PhoneNumber = "9876543210", EmailId = ""},
                    };
        }

        //Implement a new method that receives an EmployeeId and a PhoneNumber that updates the employee's PhoneNumber


        // Rest of the code...
    }
}
