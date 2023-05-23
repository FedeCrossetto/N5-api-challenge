using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using N5.Api.DataAccess.IBusiness;
using N5.Api.Models;
using Xunit;
using N5.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using N5.Api.IRepository;
using N5.Api.Business;

namespace N5.Test
{
    public class PermissionTests
    {
        private readonly Mock<IPermissionBusinessLogic> _permissionBusinessLogicMock;
        private readonly PermissionController _controller;

        public PermissionTests()
        {
            _permissionBusinessLogicMock = new Mock<IPermissionBusinessLogic>();
            _controller = new PermissionController(_permissionBusinessLogicMock.Object);
        }

        List<Permiso> permissions = new List<Permiso> { new Permiso { Id = 1, NombreEmpleado = "Federico", ApellidoEmpleado = "Crossetto", TipoPermiso = 1, FechaPermiso = DateTime.Now },
                                                         new Permiso { Id = 1, NombreEmpleado = "Mathias", ApellidoEmpleado = "Vazquez", TipoPermiso = 2, FechaPermiso = DateTime.Now }  };
        Permiso permission = new Permiso { Id = 1, NombreEmpleado = "Federico", ApellidoEmpleado = "Crossetto", TipoPermiso = 1, FechaPermiso = DateTime.Now };


        #region Controllers
        //Valido que retorne OK.
        [Fact]
        public async Task GetAllPermissions_Ok_Test()
        {
            // Arrange
            //var permissions = new List<Permiso> { new Permiso { Id = 1, NombreEmpleado = "Federico", ApellidoEmpleado = "Crossetto", TipoPermiso = 1, FechaPermiso = DateTime.Now } };
            var permissionBusinessLogicMock = new Mock<IPermissionBusinessLogic>();
            permissionBusinessLogicMock.Setup(b => b.GetAllPermissions()).ReturnsAsync(permissions);
            var controller = new PermissionController(permissionBusinessLogicMock.Object);

            // Act
            var result = await controller.GetAllPermissions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPermissions = Assert.IsAssignableFrom<List<Permiso>>(okResult.Value);
            Assert.Equal(permissions, returnedPermissions);
        }

        //Valido que me retorne un Ok
        [Fact]
        public async Task GetPermissionById_Ok_Test()
        {
            // Arrange
            var permissionBusinessLogicMock = new Mock<IPermissionBusinessLogic>();
            permissionBusinessLogicMock.Setup(b => b.GetPermissionById(1)).ReturnsAsync(permission);
            var controller = new PermissionController(permissionBusinessLogicMock.Object);

            // Act
            var result = await controller.GetPermissionById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPermission = Assert.IsType<Permiso>(okResult.Value);
            Assert.Equal(permission, returnedPermission);
        }

        //Valida que retorne un NotFound
        [Fact]
        public async Task GetPermissionByIdTest_NotFound_Test()
        {
            // Arrange
            var permissionId = 1;
            var exceptionMessage = "Permission not found";
            _permissionBusinessLogicMock.Setup(b => b.GetPermissionById(permissionId)).ThrowsAsync(new ArgumentException(exceptionMessage));

            // Act
            var result = await _controller.GetPermissionById(permissionId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<string>>(notFoundResult.Value);
            Assert.False(apiResponse.Success);
        }

        //Valida que el ModelState retorne Ok cuando es correcto.
        [Fact]
        public async Task CreatePermission_Ok_Test()
        {
            // Arrange
            _controller.ModelState.Clear();

            // Act
            var result = await _controller.CreatePermission(permission);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<object>>((result as OkObjectResult)?.Value);
            Assert.True(apiResponse.Success);
        }

        //Valida que el ModelState retorne BadRequest cuando es incorrecto
        [Fact]
        public async Task CreatePermission_BadRequestModelState_Test()
        {
            // Arrange
            var exceptionMessage = "Invalid data";
            _controller.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            var result = await _controller.CreatePermission(permission);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<string>>((result as BadRequestObjectResult)?.Value);
            Assert.False(apiResponse.Success);
            Assert.Equal(exceptionMessage, apiResponse.ErrorMessage);
        }

        //Valida que el endpoint retorne BadRequest ante un problema 
        [Fact]
        public async Task CreatePermission_BadRequest_Test()
        {
            // Arrange
            var exceptionMessage = "Permission Type not found";
            _permissionBusinessLogicMock.Setup(b => b.CreatePermission(permission)).ThrowsAsync(new ArgumentException(exceptionMessage));

            // Act
            var result = await _controller.CreatePermission(permission);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<string>>((result as BadRequestObjectResult)?.Value);
            Assert.False(apiResponse.Success);
        }

        [Fact]
        public async Task UpdatePermission_Ok_Testl()
        {
            // Arrange
            var updatedPermission = new Permiso { Id = 1, NombreEmpleado = "Federico2", ApellidoEmpleado = "Crossetto2", TipoPermiso = 2, FechaPermiso = DateTime.Now };
            var permissionBusinessLogicMock = new Mock<IPermissionBusinessLogic>();
            permissionBusinessLogicMock.Setup(b => b.UpdatePermission(1, updatedPermission)).Verifiable();
            var controller = new PermissionController(permissionBusinessLogicMock.Object);

            // Act
            var result = await controller.UpdatePermission(1, updatedPermission);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<string>>(okResult.Value);
            Assert.True(apiResponse.Success);
            permissionBusinessLogicMock.Verify(b => b.UpdatePermission(1, updatedPermission), Times.Once);
        }

        [Fact]
        public async Task UpdatePermission_NotFound_Test()
        {
            // Arrange
            var exceptionMessage = "Permission not found";
            var permissionBusinessLogicMock = new Mock<IPermissionBusinessLogic>();
            permissionBusinessLogicMock.Setup(b => b.UpdatePermission(4, It.IsAny<Permiso>())).ThrowsAsync(new ArgumentException(exceptionMessage));
            var controller = new PermissionController(permissionBusinessLogicMock.Object);

            // Act
            var result = await controller.UpdatePermission(4, new Permiso());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<string>>(notFoundResult.Value);
            Assert.False(apiResponse.Success);
            Assert.Equal(exceptionMessage, apiResponse.ErrorMessage);
        }

        #endregion

    }
}
