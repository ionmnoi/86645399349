using UnitTest_Demo.Model;
using System.Collections.Generic;

namespace UnitTest_Demo.Services 
{

    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int id);
        Employee Add(Employee employee);
        bool Delete(int id);
    }
}
