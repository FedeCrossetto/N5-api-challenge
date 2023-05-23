using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using N5.Api.Controllers;
using N5.Api.DataAccess.IBusiness;
using N5.Api.IRepository;
using N5.Api.Models;

namespace N5.Test
{
    public class PermissionTypeTest
    {
        private Mock<IPermissionTypeBusinessLogic> _mockPermissionTypeBusinessLogic;
        private PermissionTypeController _controller;

        public PermissionTypeTest()
        {
            _mockPermissionTypeBusinessLogic = new Mock<IPermissionTypeBusinessLogic>();
            _controller = new PermissionTypeController(_mockPermissionTypeBusinessLogic.Object);
        }

        List<TipoPermiso> permissionTypes = new List<TipoPermiso> { new TipoPermiso { Id = 1, Descripcion = "Admin"},
                                                                    new TipoPermiso { Id = 1, Descripcion = "User" }};

        TipoPermiso permissionType = new TipoPermiso { Id = 1, Descripcion = "Generic" };

        //
        [Fact]
        public async Task GetAllPermissionTypes_Ok_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.GetAllPermissionTypes()).ReturnsAsync(permissionTypes);

            // Act
            var result = await _controller.GetAllPermissions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Same(permissionTypes, okResult.Value);
        }

        [Fact]
        public async Task GetPermissionTypeById_Ok_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.GetPermissionTypeById(1)).ReturnsAsync(permissionType);

            // Act
            var result = await _controller.GetPermissionTypeById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Same(permissionType, okResult.Value);
        }

        [Fact]
        public async Task GetPermissionTypeById_NotFound_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.GetPermissionTypeById(1)).Throws<Exception>();

            // Act
            var result = await _controller.GetPermissionTypeById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreatePermissionType_Ok_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.CreatePermissionType(permissionType)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreatePermissionType(permissionType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task CreatePermissionType_BadRequest_Test()
        {
            // Arrange
            _controller.ModelState.AddModelError("propertyName", "Error message");

            // Act
            var result = await _controller.CreatePermissionType(permissionType);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreatePermissionType_ErrorMessage_Test()
        {
            // Arrange
            var errorMessage = "Error message";
            _mockPermissionTypeBusinessLogic.Setup(b => b.CreatePermissionType(permissionType))
                .Throws(new ArgumentException(errorMessage));

            // Act
            var result = await _controller.CreatePermissionType(permissionType);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequestResult.Value);
            Assert.False(response.Success);
            Assert.Equal(errorMessage, response.ErrorMessage);
        }


        [Fact]
        public async Task UpdatePermissionType_Ok_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.UpdatePermissionType(1, permissionType)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePermissionType(1, permissionType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(okResult.Value);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task UpdatePermissionType_NotFound_Test()
        {
            // Arrange
            _mockPermissionTypeBusinessLogic.Setup(b => b.UpdatePermissionType(1, permissionType)).Throws<ArgumentException>();

            // Act
            var result = await _controller.UpdatePermissionType(1, permissionType);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
