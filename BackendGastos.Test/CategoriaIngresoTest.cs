using BackendGastos.Controllers;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BackendGastos.Test
{
    public class CategoriaIngresoTest
    {
        private readonly Mock<IValidator<CategoriaIngresoDto>> _mockCategoriaIngresoValidator;
        private readonly Mock<IValidator<InsertUpdateCategoriaIngresoDto>> _mockInsertUpdateCategoriaIngresoValidator;
        private readonly Mock<ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>> _mockCategoriaIngresoService;
        private readonly CategoriaIngresoController _controller;

        public CategoriaIngresoTest()
        {
            _mockCategoriaIngresoValidator = new Mock<IValidator<CategoriaIngresoDto>>();
            _mockInsertUpdateCategoriaIngresoValidator = new Mock<IValidator<InsertUpdateCategoriaIngresoDto>>();
            _mockCategoriaIngresoService = new Mock<ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>>();

            _controller = new CategoriaIngresoController(
                _mockCategoriaIngresoValidator.Object,
                _mockInsertUpdateCategoriaIngresoValidator.Object,
                _mockCategoriaIngresoService.Object
            );
        }

        //
        // GET
        //

        [Fact]
        public async Task Get_ReturnsCategoriaIngresoDtos_WhenCategoriasExist()
        {
            // Arrange
            var categoriaIngresos = new List<CategoriaIngresoDto>
        {
            new CategoriaIngresoDto { Id = 1, Descripcion = "Categoria 1" },
            new CategoriaIngresoDto { Id = 2, Descripcion = "Categoria 2" }
        };
            _mockCategoriaIngresoService.Setup(service => service.Get())
                .ReturnsAsync(categoriaIngresos);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Categoria 1", result.First().Descripcion);
            _mockCategoriaIngresoService.Verify(service => service.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCategoriasExist()
        {
            // Arrange
            _mockCategoriaIngresoService.Setup(service => service.Get())
                .ReturnsAsync(new List<CategoriaIngresoDto>());

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockCategoriaIngresoService.Verify(service => service.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ThrowsException_WhenServiceFails()
        {
            // Arrange
            _mockCategoriaIngresoService.Setup(service => service.Get())
                .ThrowsAsync(new System.Exception("Service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _controller.Get());
            Assert.Equal("Service error", exception.Message);
            _mockCategoriaIngresoService.Verify(service => service.Get(), Times.Once);
        }

        //
        // GET BY ID
        //

        [Fact]
        public async Task GetById_ReturnsOkResult_WithCategoriaIngresoDto_WhenCategoriaExists()
        {
            // Arrange
            var categoriaId = 1;
            var categoriaIngreso = new CategoriaIngresoDto { Id = categoriaId, Descripcion = "Categoria 1" };
            _mockCategoriaIngresoService.Setup(service => service.GetById(categoriaId))
                .ReturnsAsync(categoriaIngreso);

            // Act
            var result = await _controller.Get(categoriaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CategoriaIngresoDto>(okResult.Value);
            Assert.Equal(categoriaId, returnValue.Id);
            _mockCategoriaIngresoService.Verify(service => service.GetById(categoriaId), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenCategoriaDoesNotExist()
        {
            // Arrange
            var categoriaId = 1;
            _mockCategoriaIngresoService.Setup(service => service.GetById(categoriaId))
                .ReturnsAsync((CategoriaIngresoDto)null);

            // Act
            var result = await _controller.Get(categoriaId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockCategoriaIngresoService.Verify(service => service.GetById(categoriaId), Times.Once);
        }

        [Fact]
        public async Task GetById_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var categoriaId = 1;
            _mockCategoriaIngresoService.Setup(service => service.GetById(categoriaId))
                .ThrowsAsync(new System.Exception("Service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _controller.Get(categoriaId));
            Assert.Equal("Service error", exception.Message);
            _mockCategoriaIngresoService.Verify(service => service.GetById(categoriaId), Times.Once);
        }

        //
        // ADD
        //

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Invalid Categoria" };
            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Descripcion", "Nombre is required")
        };
            var validationResult = new ValidationResult(validationFailures);
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(validationFailures, badRequestResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Invalid Categoria" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto)).Returns(false);
            _mockCategoriaIngresoService.SetupGet(s => s.Errors).Returns(new List<string> { "Service validation failed" });

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(_mockCategoriaIngresoService.Object.Errors, badRequestResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Valid Categoria" };
            var validationResult = new ValidationResult();
            var categoriaIngresoDto = new CategoriaIngresoDto { Id = 1, Descripcion = "Valid Categoria" };
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto)).Returns(true);
            _mockCategoriaIngresoService.Setup(s => s.Add(insertDto)).ReturnsAsync(categoriaIngresoDto);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.Get), createdAtActionResult.ActionName);
            Assert.Equal(categoriaIngresoDto.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(categoriaIngresoDto, createdAtActionResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Add(insertDto), Times.Once);
        }

        [Fact]
        public async Task Add_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Categoria" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto)).Returns(true);
            _mockCategoriaIngresoService.Setup(s => s.Add(insertDto))
                .ThrowsAsync(new System.Exception("Service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _controller.Add(insertDto));
            Assert.Equal("Service error", exception.Message);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Add(insertDto), Times.Once);
        }

        //
        // PUT
        //

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var id = 1L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Invalid Categoria" };
            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Descripcion", "Nombre is required")
        };
            var validationResult = new ValidationResult(validationFailures);
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Put(id, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(validationFailures, badRequestResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var id = 1L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Invalid Categoria" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto, id)).Returns(false);
            _mockCategoriaIngresoService.SetupGet(s => s.Errors).Returns(new List<string> { "Service validation failed" });

            // Act
            var result = await _controller.Put(id, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(_mockCategoriaIngresoService.Object.Errors, badRequestResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto, id), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenUpdateReturnsNull()
        {
            // Arrange
            var id = 1L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Categoria" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto, id)).Returns(true);
            _mockCategoriaIngresoService.Setup(s => s.Update(id, insertDto)).ReturnsAsync((CategoriaIngresoDto)null);

            // Act
            var result = await _controller.Put(id, insertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto, id), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Update(id, insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenValid()
        {
            // Arrange
            var id = 1L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Categoria" };
            var validationResult = new ValidationResult();
            var categoriaIngresoDto = new CategoriaIngresoDto { Id = id, Descripcion = "Categoria" };
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto, id)).Returns(true);
            _mockCategoriaIngresoService.Setup(s => s.Update(id, insertDto)).ReturnsAsync(categoriaIngresoDto);

            // Act
            var result = await _controller.Put(id, insertDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(categoriaIngresoDto, okResult.Value);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto, id), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Update(id, insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var id = 1L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Categoria" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaIngresoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaIngresoService.Setup(s => s.Validate(insertDto, id)).Returns(true);
            _mockCategoriaIngresoService.Setup(s => s.Update(id, insertDto))
                .ThrowsAsync(new System.Exception("Service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _controller.Put(id, insertDto));
            Assert.Equal("Service error", exception.Message);
            _mockInsertUpdateCategoriaIngresoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Validate(insertDto, id), Times.Once);
            _mockCategoriaIngresoService.Verify(s => s.Update(id, insertDto), Times.Once);
        }

        //
        // DELETE
        //

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var id = 1L;
            _mockCategoriaIngresoService.Setup(s => s.Delete(id)).ReturnsAsync((CategoriaIngresoDto)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            _mockCategoriaIngresoService.Verify(s => s.Delete(id), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenServiceReturnsCategoriaIngresoDto()
        {
            // Arrange
            var id = 1L;
            var categoriaIngresoDto = new CategoriaIngresoDto { Id = id, Descripcion = "Categoria" };
            _mockCategoriaIngresoService.Setup(s => s.Delete(id)).ReturnsAsync(categoriaIngresoDto);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(categoriaIngresoDto, okResult.Value);
            _mockCategoriaIngresoService.Verify(s => s.Delete(id), Times.Once);
        }

        [Fact]
        public async Task Delete_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var id = 1L;
            _mockCategoriaIngresoService.Setup(s => s.Delete(id))
                .ThrowsAsync(new System.Exception("Service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _controller.Delete(id));
            Assert.Equal("Service error", exception.Message);
            _mockCategoriaIngresoService.Verify(s => s.Delete(id), Times.Once);
        }

    }
}