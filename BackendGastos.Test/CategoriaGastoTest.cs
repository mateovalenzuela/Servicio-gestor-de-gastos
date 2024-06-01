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
using BackendGastos.Repository.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BackendGastos.Test
{
    public class CategoriaGastoTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProyectoGastosTestContext _context;
        private readonly CategoriaGastoController _controller;

        private readonly InsertUpdateCategoriaGastoDto _validCat;
        private readonly InsertUpdateCategoriaGastoDto _validCat2;
        private readonly InsertUpdateCategoriaGastoDto _invalidCat;
        private readonly InsertUpdateCategoriaGastoDto _duplicateCat;

        public CategoriaGastoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);

            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var categoriaGastoService = _serviceProvider.GetRequiredService<ICategoriaGastoService>();
            var categoriaGastoValidator = _serviceProvider.GetRequiredService<IValidator<CategoriaGastoDto>>();
            var insertUpdateCategoriaGastoValidator = _serviceProvider.GetRequiredService<IValidator<InsertUpdateCategoriaGastoDto>>();

            _controller = new CategoriaGastoController(
                categoriaGastoValidator,
                insertUpdateCategoriaGastoValidator,
                categoriaGastoService);

            _validCat = new InsertUpdateCategoriaGastoDto { Descripcion = "Streaming Services" };
            _validCat2 = new InsertUpdateCategoriaGastoDto { Descripcion = "Food" };
            _invalidCat = new InsertUpdateCategoriaGastoDto { Descripcion = "" };
            _duplicateCat = new InsertUpdateCategoriaGastoDto { Descripcion = "Food" };
        }

        private async Task<List<CategoriaGastoDto>> ArrangeAddStup()
        {
            // Arrange
            var usuarios = new List<AuthenticationUsuario>
            {
                new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "password1",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false },
                new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "password2",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false }
            };

            _context.AuthenticationUsuarios.AddRange(usuarios);
            _context.SaveChanges();

            var categoriaGastoService = _serviceProvider.GetRequiredService<ICategoriaGastoService>();

            var addedCategoria1 = await categoriaGastoService.Add(_validCat);
            var addedCategoria2 = await categoriaGastoService.Add(_validCat2);

            var expectedCategorias = new List<CategoriaGastoDto> { addedCategoria1, addedCategoria2 };
            return expectedCategorias;
        }

        [Fact]
        public async Task Get_ReturnsListOfCategoriaGastoDtos()
        {
            // Arrange
            var expectedCategorias = await ArrangeAddStup();

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<CategoriaGastoDto>>(result);

            var resultList = result.ToList();
            Assert.Equal(resultList, expectedCategorias);
        }

        [Fact]
        public async Task Get_ReturnsEmptyList()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<CategoriaGastoDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenCategoriaGastoExists()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var catSelected = categoriasAgregadas.ElementAt(0);

            // Act
            var result = await _controller.Get(catSelected.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<CategoriaGastoDto>(objectResult.Value);

            Assert.NotNull(actualValue);
            Assert.Equal(catSelected, actualValue);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var _ = ArrangeAddStup();

            // Act
            var result = await _controller.Add(_invalidCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var _ = await ArrangeAddStup();

            // Act
            var result = await _controller.Add(_duplicateCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var insertedCats = await ArrangeAddStup();
            var newCat = new InsertUpdateCategoriaGastoDto { Descripcion = "Transport" };

            // Act
            var result = await _controller.Add(newCat);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<CategoriaGastoDto>(createdAtActionResult.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddStup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();
            var invalidCat = new InsertUpdateCategoriaGastoDto { Descripcion = "" };

            // Act
            var result = await _controller.Put(selectedCategoria.Id, invalidCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Single(errors);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddStup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = insertedCategorias.ElementAt(1).Descripcion };

            // Act
            var result = await _controller.Put(selectedCategoria.Id, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var servicioNecesario = _serviceProvider.GetRequiredService<ICategoriaGastoService>();
            var updatedCategoria = await servicioNecesario.GetById(selectedCategoria.Id);
            Assert.Equal(selectedCategoria, updatedCategoria);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Arrange
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "New Category" };
            var idCat = 100L;

            // Act
            var result = await _controller.Put(idCat, updateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddStup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();
            var updateDto = new InsertUpdateCategoriaGastoDto { Descripcion = "Updated Category" };
            var expectedCategoria = new CategoriaGastoDto
            {
                Id = selectedCategoria.Id,
                Descripcion = updateDto.Descripcion
            };

            // Act
            var result = await _controller.Put(selectedCategoria.Id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedCategoria, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenCategoriaGastoDoesNotExist()
        {
            // Arrange
            var _ = await ArrangeAddStup();
            var idCat = 100L;

            // Act
            var result = await _controller.Delete(idCat);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenCategoriaGastoDeleted()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddStup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();

            // Act
            var result = await _controller.Delete(selectedCategoria.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedCategoria, okResult.Value);
            Assert.Null(_context.GastosCategoriagastos.FirstOrDefault(c => c.Id == selectedCategoria.Id && c.Baja == false));
        }
    }

}
