using BackendGastos.Controllers;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BackendGastos.Test
{
    public class CategoriaIngresoTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly CategoriaIngresoController _controller;
        private readonly ProyectoGastosTestContext _context;
        private readonly InsertUpdateCategoriaIngresoDto _validCat;
        private readonly InsertUpdateCategoriaIngresoDto _validCat2;
        private readonly InsertUpdateCategoriaIngresoDto _validCat3;
        private readonly InsertUpdateCategoriaIngresoDto _invalidCat;
        private readonly InsertUpdateCategoriaIngresoDto _repeatedCat;
        private readonly InsertUpdateCategoriaIngresoDto _createCat;

        public CategoriaIngresoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);

            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var  categoriaIngresoService = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();
            var categoriaIngresoValidator = _serviceProvider.GetRequiredService<IValidator<CategoriaIngresoDto>>();
            var inserUpdateCategoriaIngresoValidator = _serviceProvider.GetRequiredService<IValidator<InsertUpdateCategoriaIngresoDto>>();


            _controller = new CategoriaIngresoController(
                categoriaIngresoValidator,
                inserUpdateCategoriaIngresoValidator,
                categoriaIngresoService
            );

            _validCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "Sueldo" };
            _validCat2 = new InsertUpdateCategoriaIngresoDto { Descripcion = "Inversiones" };
            _validCat3 = new InsertUpdateCategoriaIngresoDto { Descripcion = "Prestamo" };
            _invalidCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "" };
            _repeatedCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "Sueldo" };
            _createCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "Dividendos" };

        }


        private async Task<List<CategoriaIngresoDto>> ArrangeAddSetup()
        {
            // Arrange

            // Add two users
            var usuarios = new List<AuthenticationUsuario>
            {
            new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "fwewfwefwe",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "fwewfwefwe",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            };

            _context.AuthenticationUsuarios.AddRange(usuarios);
            _context.SaveChanges();


            var servicioNecesario = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();

            var addedCategoria1 = await servicioNecesario.Add(_validCat);
            var addedCategoria2 = await servicioNecesario.Add(_validCat2);
            var addedCategoria3 = await servicioNecesario.Add(_validCat3);

            var insertedSubCategorias = new List<CategoriaIngresoDto> { addedCategoria1, addedCategoria2, addedCategoria3 };
            return insertedSubCategorias;
        }

        //
        // GET
        //

        [Fact]
        public async Task Get_ReturnsCategoriaIngresoDtos_WhenCategoriasExist()
        {
            // Arrange
            var insertedCategoriasIngreso = await ArrangeAddSetup();

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(insertedCategoriasIngreso, result);
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCategoriasExist()
        {
            // Arrange
            

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        
        
        //
        // GET BY ID
        //

        [Fact]
        public async Task GetById_ReturnsOkResult_WithCategoriaIngresoDto_WhenCategoriaExists()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();

            // Act
            var result = await _controller.Get(selectedCategoria.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CategoriaIngresoDto>(okResult.Value);
            Assert.Equal(selectedCategoria, returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenCategoriaDoesNotExist()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var idCat = 100L;

            // Act
            var result = await _controller.Get(idCat);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }
        
        //
        // ADD
        //

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_invalidCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(1, errors.Count);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_repeatedCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(insertedCategorias.Count, _context.GastosCategoriaigresos.Where(c => c.Baja == false).ToList().Count);
        }
        
        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            

            // Act
            var result = await _controller.Add(_createCat);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var servicioNecesario = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();
            var insertedCategorias = await servicioNecesario.Get();
            Assert.Equal(insertedCategorias.FirstOrDefault(), createdAtActionResult.Value);
            Assert.Equal(1, insertedCategorias.Count());

        }

        //
        // PUT
        //

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var insertedCategoria = insertedCategorias.FirstOrDefault();
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "P" };

            // Act
            var result = await _controller.Put(insertedCategoria.Id, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(1, errors.Count);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = insertedCategorias.ElementAt(1).Descripcion };

            // Act
            var result = await _controller.Put(selectedCategoria.Id, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var servicioNecesario = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();
            var updatedCategoria = await servicioNecesario.GetById(selectedCategoria.Id);
            Assert.Equal(selectedCategoria, updatedCategoria);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenUpdateReturnsNull()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var idCat = 100L;
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Nueva Categoria" };

            // Act
            var result = await _controller.Put(idCat, insertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);

        }
        
        [Fact]
        public async Task Put_ReturnsOk_WhenValid()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();
            var insertDto = new InsertUpdateCategoriaIngresoDto { Descripcion = "Categoria Actualizada" };

            // Act
            var result = await _controller.Put(selectedCategoria.Id, insertDto);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var expectedObject = new CategoriaIngresoDto { Id = selectedCategoria.Id, Descripcion = insertDto.Descripcion };
            Assert.Equal(expectedObject, objectResult.Value);
        }

        
        //
        // DELETE
        //

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var _ = await ArrangeAddSetup();
            var idSubCat = 100L;

            // Act
            var result = await _controller.Delete(idSubCat);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenServiceReturnsCategoriaIngresoDto()
        {
            // Arrange
            var insertedCategorias = await ArrangeAddSetup();
            var selectedCategoria = insertedCategorias.FirstOrDefault();

            // Act
            var result = await _controller.Delete(selectedCategoria.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedCategoria, okResult.Value);

            var servicioNecesario = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();
            var deletedCategoria = await servicioNecesario.GetById(selectedCategoria.Id);
            Assert.Null(deletedCategoria);
        }

    }
}