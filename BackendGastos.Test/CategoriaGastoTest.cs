using BackendGastos.Controller.Controllers;
using BackendGastos.Service.DTOs.CategoriaGasto;
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

namespace BackendGastos.Test
{
    public class CategoriaGastoTest
    {
        private readonly Mock<IValidator<CategoriaGastoDto>> _mockCategoriaGastoValidator;
        private readonly Mock<IValidator<InsertUpdateCategoriaGastoDto>> _mockInsertUpdateCategoriaGastoValidator;
        private readonly Mock<ICategoriaGastoService> _mockCategoriaGastoService;
        private readonly CategoriaGastoController _controller;

        public CategoriaGastoTest()
        {
            _mockCategoriaGastoValidator = new Mock<IValidator<CategoriaGastoDto>>();
            _mockInsertUpdateCategoriaGastoValidator = new Mock<IValidator<InsertUpdateCategoriaGastoDto>>();
            _mockCategoriaGastoService = new Mock<ICategoriaGastoService>();

            _controller = new CategoriaGastoController(
                _mockCategoriaGastoValidator.Object,
                _mockInsertUpdateCategoriaGastoValidator.Object,
                _mockCategoriaGastoService.Object
            );
        }

        [Fact]
        public async Task Get_ReturnsListOfCategoriaGastoDtos()
        {
            // Arrange
            var categoriaGastoList = new List<CategoriaGastoDto>
        {
            new CategoriaGastoDto { Id = 1, Descripcion = "Gasto1" },
            new CategoriaGastoDto { Id = 2, Descripcion = "Gasto2" }
        };
            _mockCategoriaGastoService.Setup(s => s.Get()).ReturnsAsync(categoriaGastoList);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Gasto1", result.First().Descripcion);
            _mockCategoriaGastoService.Verify(service => service.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsEmpityList()
        {
            // Arrange
            _mockCategoriaGastoService.Setup(s => s.Get()).ReturnsAsync([]);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockCategoriaGastoService.Verify(service => service.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Arrange
            var id = 1L;
            _mockCategoriaGastoService.Setup(s => s.GetById(id)).ReturnsAsync((CategoriaGastoDto)null);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            _mockCategoriaGastoService.Verify(service => service.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenCategoriaGastoExists()
        {
            // Arrange
            var id = 1L;
            var categoriaGasto = new CategoriaGastoDto { Id = id, Descripcion = "Gasto" };
            _mockCategoriaGastoService.Setup(s => s.GetById(id)).ReturnsAsync(categoriaGasto);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<CategoriaGastoDto>(okResult.Value);
            Assert.Equal(id, model.Id);
            _mockCategoriaGastoService.Verify(service => service.GetById(id), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaGastoDto() { Descripcion = ""};
            var validationFailures = new List<ValidationFailure>
            {
                new("Descripcion", "La Descripcion es requerida")
            };
            var validationResult = new ValidationResult(validationFailures);
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Add(insertDto);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Gasto" };
            var validationResult = new ValidationResult();
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default))
                .ReturnsAsync(validationResult);

            _mockCategoriaGastoService.Setup(s => s.Validate(insertDto)).Returns(false);
            _mockCategoriaGastoService.SetupGet(s => s.Errors).Returns(["La categoria existe"]);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var insertDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Gasto" };
            var categoriaGastoDto = new CategoriaGastoDto { Id = 1, Descripcion = "Gasto" };
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(insertDto, default)).ReturnsAsync(new ValidationResult());
            _mockCategoriaGastoService.Setup(s => s.Validate(insertDto)).Returns(true);
            _mockCategoriaGastoService.Setup(s => s.Add(insertDto)).ReturnsAsync(categoriaGastoDto);

            // Act
            var result = await _controller.Add(insertDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsAssignableFrom<CategoriaGastoDto>(createdAtActionResult.Value);
            Assert.Equal(1, model.Id);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(insertDto, default), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Validate(insertDto), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Add(insertDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var id = 1L;
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Categoria Actualizada"};
            var validationResult = new ValidationResult(new List<ValidationFailure> { new("Descripcion", "Error") });
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(updateDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.Put(id, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(updateDto, default), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var id = 1L;
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Gasto" };
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(updateDto, default)).ReturnsAsync(new ValidationResult());
            _mockCategoriaGastoService.Setup(s => s.Validate(updateDto, id)).Returns(false);
            _mockCategoriaGastoService.SetupGet(s => s.Errors).Returns(["Error"]);

            // Act
            var result = await _controller.Put(id, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(updateDto, default), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Validate(updateDto, id), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Arrange
            var id = 1L;
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Gasto" };
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(updateDto, default)).ReturnsAsync(new ValidationResult());
            _mockCategoriaGastoService.Setup(s => s.Validate(updateDto, id)).Returns(true);
            _mockCategoriaGastoService.Setup(s => s.Update(id, updateDto)).ReturnsAsync((CategoriaGastoDto)null);

            // Act
            var result = await _controller.Put(id, updateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(updateDto, default), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Validate(updateDto, id), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Update(id, updateDto), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenCategoriaGastoExists()
        {
            // Arrange
            var id = 1L;
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Gasto" };
            var categoriaGastoDto = new CategoriaGastoDto { Id = id, Descripcion = "Gasto" };
            _mockInsertUpdateCategoriaGastoValidator.Setup(v => v.ValidateAsync(updateDto, default)).ReturnsAsync(new ValidationResult());
            _mockCategoriaGastoService.Setup(s => s.Validate(updateDto, id)).Returns(true);
            _mockCategoriaGastoService.Setup(s => s.Update(id, updateDto)).ReturnsAsync(categoriaGastoDto);

            // Act
            var result = await _controller.Put(id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<CategoriaGastoDto>(okResult.Value);
            Assert.Equal(id, model.Id);
            _mockInsertUpdateCategoriaGastoValidator.Verify(v => v.ValidateAsync(updateDto, default), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Validate(updateDto, id), Times.Once);
            _mockCategoriaGastoService.Verify(s => s.Update(id, updateDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Arrange
            var id = 1L;
            _mockCategoriaGastoService.Setup(s => s.Delete(id)).ReturnsAsync((CategoriaGastoDto)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            _mockCategoriaGastoService.Verify(s => s.Delete(id), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenCategoriaGastoExists()
        {
            // Arrange
            var id = 1L;
            var categoriaGastoDto = new CategoriaGastoDto { Id = id, Descripcion = "Gasto" };
            _mockCategoriaGastoService.Setup(s => s.Delete(id)).ReturnsAsync(categoriaGastoDto);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CategoriaGastoDto>(okResult.Value);
            Assert.Equal(id, model.Id);
            _mockCategoriaGastoService.Verify(s => s.Delete(id), Times.Once);
        }
    }
}
