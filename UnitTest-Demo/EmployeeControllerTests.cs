using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitTest_Demo.Controllers;
using UnitTest_Demo.Model;
using UnitTest_Demo.Services;
using Xunit;

namespace UnitTest_Demo.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);
        }

        [Fact]
        public void Get_ReturnsListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = 1, Name = "John Doe" },
                new Employee { EmployeeId = 2, Name = "Jane Smith" }
            };
            _mockEmployeeService.Setup(service => service.GetEmployees()).Returns(employees);

            // Act
            var result = _employeeController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<List<Employee>>(okResult.Value);
            Assert.Equal(employees.Count, returnedEmployees.Count);
        }

        [Fact]
        public void Get_WithValidId_ReturnsEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employee = new Employee { EmployeeId = employeeId, Name = "John Doe" };
            _mockEmployeeService.Setup(service => service.GetEmployee(employeeId)).Returns(employee);

            // Act
            var result = _employeeController.Get(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(employeeId, returnedEmployee.EmployeeId);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var employeeId = 1;
            _mockEmployeeService.Setup(service => service.GetEmployee(employeeId)).Returns((Employee)null);

            // Act
            var result = _employeeController.Get(employeeId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_WithValidEmployee_ReturnsCreatedResponse()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, Name = "John Doe" };
            _mockEmployeeService.Setup(service => service.Add(employee)).Returns(employee);

            // Act
            var result = _employeeController.Post(employee);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(EmployeeController.Get), createdResult.ActionName);
            Assert.Equal(employee.EmployeeId, createdResult.RouteValues["id"]);
            Assert.Equal(employee, createdResult.Value);
        }

        [Fact]
        public void Post_WithInvalidEmployee_ReturnsBadRequest()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, Name = "John Doe" };
            _employeeController.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _employeeController.Post(employee);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_WithValidId_ReturnsOk()
        {
            // Arrange
            var employeeId = 1;
            _mockEmployeeService.Setup(service => service.Delete(employeeId)).Returns(true);

            // Act
            var result = _employeeController.Delete(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var employeeId = 1;
            _mockEmployeeService.Setup(service => service.Delete(employeeId)).Returns(false);

            // Act
            var result = _employeeController.Delete(employeeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
