using BackendGastos.Controller.Controllers;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.Services;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;

namespace BackendGastos.Test
{
    public class SubCategoriaGastoTest
    {
        private readonly Mock<IValidator<SubCategoriaGastoDto>> _mockSubCategoriaGastoValidator;
        private readonly Mock<IValidator<InsertUpdateSubCategoriaGastoDto>> _mockInsertUpdateSubCategoriaGastoValidator;
        private readonly Mock<ISubCategoriaGastoService> _mockSubCategoriaGastoService;
        private readonly SubCategoriaGastoController _controller;

        public SubCategoriaGastoTest()
        {
            _mockSubCategoriaGastoValidator = new Mock<IValidator<SubCategoriaGastoDto>>();
            _mockInsertUpdateSubCategoriaGastoValidator = new Mock<IValidator<InsertUpdateSubCategoriaGastoDto>>();
            _mockSubCategoriaGastoService = new Mock<ISubCategoriaGastoService>();
            _controller = new SubCategoriaGastoController(
                _mockSubCategoriaGastoValidator.Object,
                _mockInsertUpdateSubCategoriaGastoValidator.Object,
                _mockSubCategoriaGastoService.Object);
        }

        [Fact]
        public async Task Get_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaGastoDto> { new() { Id = 1, Descripcion = "Test" } };
            _mockSubCategoriaGastoService.Setup(s => s.Get()).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<SubCategoriaGastoDto>>(result);
            Assert.Equal(subCategorias, result);
            _mockSubCategoriaGastoService.Verify(s => s.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsEmpitySubCategoriaGastoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaGastoDto>();
            _mockSubCategoriaGastoService.Setup(s => s.Get()).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<SubCategoriaGastoDto>>(result);
            Assert.Empty(result);
            Assert.Equal(subCategorias, result);
            _mockSubCategoriaGastoService.Verify(s => s.Get(), Times.Once);
        }


        [Fact]
        public async Task Get_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            _mockSubCategoriaGastoService.Setup(s => s.GetById(It.IsAny<long>())).ReturnsAsync((SubCategoriaGastoDto)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetById(It.IsAny<long>()), Times.Once);
        }


        [Fact]
        public async Task GetByCategoriaGasto_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaGastoDto> { new() { Id = 1, Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByCategoriaGasto(1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByCategoriaGasto(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByCategoriaGasto(1), Times.Once);
        }


        [Fact]
        public async Task GetByCategoriaGasto_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByCategoriaGasto(1)).ReturnsAsync((List<SubCategoriaGastoDto>)null);

            // Act
            var result = await _controller.GetByCategoriaGasto(1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByCategoriaGasto(1), Times.Once);
        }


        [Fact]
        public async Task GetByUsuario_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaGastoDto> { new() { Id = 1, Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByUser(1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByUser(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByUser(1), Times.Once);
        }


        [Fact]
        public async Task GetByUser_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByUser(1)).ReturnsAsync((List<SubCategoriaGastoDto>)null);

            // Act
            var result = await _controller.GetByUser(1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByUser(1), Times.Once);
        }


        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var subCategorias = new List<SubCategoriaGastoDto> { new() { Id = 1, Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 } };
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByUserAndCategoriaGasto(1, 1)).ReturnsAsync(subCategorias);

            // Act
            var result = await _controller.GetByUserAndCategoriaGasto(1, 1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategorias, objectResult.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByUserAndCategoriaGasto(1, 1), Times.Once);
        }


        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsNull()
        {
            // Arrange
            _mockSubCategoriaGastoService.Setup(s => s.GetActiveByUserAndCategoriaGasto(1, 1)).ReturnsAsync((List<SubCategoriaGastoDto>)null);

            // Act
            var result = await _controller.GetByUserAndCategoriaGasto(1, 1);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetActiveByUserAndCategoriaGasto(1, 1), Times.Once);
        }




        [Fact]
        public async Task Get_ReturnsOk_WhenSubCategoriaGastoDtoFound()
        {
            // Arrange
            var subCategoriaGastoDto = new SubCategoriaGastoDto { Id = 1, Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1};
            _mockSubCategoriaGastoService.Setup(s => s.GetById(It.IsAny<long>())).ReturnsAsync(subCategoriaGastoDto);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategoriaGastoDto, objectResult.Value);
            _mockSubCategoriaGastoService.Verify(s => s.GetById(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("Descripcion", "Error") });
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaGastoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(false);
            _mockSubCategoriaGastoService.Setup(s => s.Errors).Returns(["Error"]);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var subCategoriaGastoDto = new SubCategoriaGastoDto { Id = 1, Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1};
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaGastoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaGastoService.Setup(s => s.Add(insertDto)).ReturnsAsync(subCategoriaGastoDto);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(subCategoriaGastoDto, createdAtActionResult.Value);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Add(insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("Descripcion", "Error") });
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaGastoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(false);
            _mockSubCategoriaGastoService.Setup(s => s.Errors).Returns(["Error"]);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaGastoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaGastoService.Setup(s => s.Update(It.IsAny<long>(), insertDto)).ReturnsAsync((SubCategoriaGastoDto)null);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Update(It.IsAny<long>(), insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Test", CategoriaGastoId = 1, UsuarioId = 1 };
            var subCategoriaGastoDto = new SubCategoriaGastoDto { Id = 1, Descripcion = "Test" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateSubCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(validationResult);
            _mockSubCategoriaGastoService.Setup(s => s.Validate(insertDto)).ReturnsAsync(true);
            _mockSubCategoriaGastoService.Setup(s => s.Update(It.IsAny<long>(), insertDto)).ReturnsAsync(subCategoriaGastoDto);

            // Act
            var result = await _controller.Put(1, insertDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategoriaGastoDto, okResult.Value);
            _mockInsertUpdateSubCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockSubCategoriaGastoService.Verify(s => s.Update(It.IsAny<long>(), insertDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            _mockSubCategoriaGastoService.Setup(s => s.Delete(It.IsAny<long>())).ReturnsAsync((SubCategoriaGastoDto)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            _mockSubCategoriaGastoService.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSubCategoriaGastoDtoDeleted()
        {
            // Arrange
            var subCategoriaGastoDto = new SubCategoriaGastoDto { Id = 1, Descripcion = "Test" };
            _mockSubCategoriaGastoService.Setup(s => s.Delete(It.IsAny<long>())).ReturnsAsync(subCategoriaGastoDto);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(subCategoriaGastoDto, okResult.Value);
            _mockSubCategoriaGastoService.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
        }
    }
}
