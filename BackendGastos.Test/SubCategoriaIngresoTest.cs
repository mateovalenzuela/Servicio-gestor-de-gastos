using BackendGastos.Controller.Controllers;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Test
{
    public class SubCategoriaIngresoTest
    {
        private readonly Mock<IValidator<SubCategoriaIngresoDto>> _mockSubCategoriaIngresoValidator;
        private readonly Mock<IValidator<InsertUpdateSubCategoriaIngresoDto>> _mockInsertUpdateSubCategoriaIngresoValidator;
        private readonly Mock<ISubCategoriaIngresoService> _mockSubCategoriaIngresoService;
        private readonly SubCategoriaIngresoController _controller;

        public SubCategoriaIngresoTest()
        {
            _mockSubCategoriaIngresoValidator = new Mock<IValidator<SubCategoriaIngresoDto>>();
            _mockInsertUpdateSubCategoriaIngresoValidator = new Mock<IValidator<InsertUpdateSubCategoriaIngresoDto>>();
            _mockSubCategoriaIngresoService = new Mock<ISubCategoriaIngresoService>();

            _controller = new SubCategoriaIngresoController(
                _mockSubCategoriaIngresoValidator.Object,
                _mockInsertUpdateSubCategoriaIngresoValidator.Object,
                _mockSubCategoriaIngresoService.Object
            );
        }


        [Fact]
        public async Task Get_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaIngresoDto> { new() { Id = 1, Descripcion = "Test" , CategoriaIngresoId = 1, UsuarioId = 1} };
            _mockSubCategoriaIngresoService.Setup(s => s.Get()).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.Equal(subCategorias, result);
            _mockSubCategoriaIngresoService.Verify(s => s.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsEmpitySubCategoriaIngresoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaIngresoDto>();
            _mockSubCategoriaIngresoService.Setup(s => s.Get()).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.Equal(subCategorias, result);
            Assert.Empty(result);
            _mockSubCategoriaIngresoService.Verify(s => s.Get(), Times.Once);
        }


        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaIngresoDto> { new() { Id = 1, Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByCategoriaIngreso(1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByCategoriaIngreso(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByCategoriaIngreso(1), Times.Once);
        }


        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByCategoriaIngreso(1)).ReturnsAsync((List<SubCategoriaIngresoDto>)null);

            // Act
            var result = await _controller.GetByCategoriaIngreso(1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByCategoriaIngreso(1), Times.Once);
        }


        [Fact]
        public async Task GetByUsuario_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaIngresoDto> { new() { Id = 1, Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByUser(1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByUser(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByUser(1), Times.Once);
        }


        [Fact]
        public async Task GetByUser_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByUser(1)).ReturnsAsync((List<SubCategoriaIngresoDto>)null);

            // Act
            var result = await _controller.GetByUser(1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByUser(1), Times.Once);
        }


        [Fact]
        public async Task GetByUserAndCategoriaIngreso_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaIngresoDto> { new() { Id = 1, Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByUserAndCategoriaIngreso(1, 1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByUserAndCategoriaIngreso(1, 1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByUserAndCategoriaIngreso(1, 1), Times.Once);
        }


        [Fact]
        public async Task GetByUserAndCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaIngresoService.Setup(s => s.GetActiveByUserAndCategoriaIngreso(1, 1)).ReturnsAsync((List<SubCategoriaIngresoDto>)null);

            // Act
            var result = await _controller.GetByUserAndCategoriaIngreso(1, 1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetActiveByUserAndCategoriaIngreso(1, 1), Times.Once);
        }


        [Fact]
        public async Task Get_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotFound()
        {
            // Arrange
            _mockSubCategoriaIngresoService.Setup(s => s.GetById(It.IsAny<long>())).ReturnsAsync((SubCategoriaIngresoDto)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockSubCategoriaIngresoService.Verify(s => s.GetById(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenSubCategoriaIngresoDtoFound()
        {
            // Arrange
            var subCategoriaIngresoDto = new SubCategoriaIngresoDto { Id = 1, Descripcion = "Test" , CategoriaIngresoId = 1, UsuarioId =1};
            _mockSubCategoriaIngresoService.Setup(s => s.GetById(It.IsAny<long>())).ReturnsAsync(subCategoriaIngresoDto);

            // Act
            var result = await _controller.Get(subCategoriaIngresoDto.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategoriaIngresoDto, (result.Result as OkObjectResult).Value);
            _mockSubCategoriaIngresoService.Verify(s => s.GetById(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "SubCategoria" , CategoriaIngresoId = 1, UsuarioId = 1};
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("Descripcion", "Error") });
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "SubCategoria", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaIngresoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(false);
            _mockSubCategoriaIngresoService.Setup(s => s.Errors).Returns(["Error"]);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var subCategoriaIngresoDto = new SubCategoriaIngresoDto { Id = 1, Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaIngresoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaIngresoService.Setup(s => s.Add(insertDto)).ReturnsAsync(subCategoriaIngresoDto);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(subCategoriaIngresoDto, createdAtActionResult.Value);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Add(insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("Descripcion", "Error") });
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaIngresoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(false);
            _mockSubCategoriaIngresoService.Setup(s => s.Errors).Returns(["Error"]);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Errors, Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotFound()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaIngresoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaIngresoService.Setup(s => s.Update(It.IsAny<long>(), insertDto)).ReturnsAsync((SubCategoriaIngresoDto)null);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Update(It.IsAny<long>(), insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var subCategoriaIngresoDto = new SubCategoriaIngresoDto { Id = 1, Descripcion = "Test", CategoriaIngresoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaIngresoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaIngresoService.Setup(s => s.Update(It.IsAny<long>(), insertDto)).ReturnsAsync(subCategoriaIngresoDto);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategoriaIngresoDto, okResult.Value);
            _mockInsertUpdateSubCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaIngresoService.Verify(s => s.Update(It.IsAny<long>(), insertDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotFound()
        {
            // Arrange
            _mockSubCategoriaIngresoService.Setup(s => s.Delete(It.IsAny<long>())).ReturnsAsync((SubCategoriaIngresoDto)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockSubCategoriaIngresoService.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSubCategoriaIngresoDtoDeleted()
        {
            // Arrange
            var subCategoriaIngresoDto = new SubCategoriaIngresoDto { Id = 1, Descripcion = "Test" , CategoriaIngresoId = 1, UsuarioId = 1};
            _mockSubCategoriaIngresoService.Setup(s => s.Delete(It.IsAny<long>())).ReturnsAsync(subCategoriaIngresoDto);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(subCategoriaIngresoDto, okResult.Value);
            _mockSubCategoriaIngresoService.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
        }





    }
}
