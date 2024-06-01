using BackendGastos.Controller.Controllers;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.Services;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using Microsoft.Extensions.DependencyInjection;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaGasto;
using System.ComponentModel;

namespace BackendGastos.Test
{
    /*
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }

    [Collection("Database collection")]*/
    public class SubCategoriaGastoTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly SubCategoriaGastoController _controller;
        private readonly ProyectoGastosTestContext _context;
        private readonly InsertUpdateCategoriaGastoDto _validCat;
        private readonly InsertUpdateCategoriaGastoDto _validCat2;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat2;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat3;
        private readonly InsertUpdateSubCategoriaGastoDto _ivalidSubCat;
        private readonly InsertUpdateSubCategoriaGastoDto _repeatedSubCat;
        private readonly InsertUpdateSubCategoriaGastoDto _createSubCat;

        public SubCategoriaGastoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);

            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var subCategoriaGastoService = _serviceProvider.GetRequiredService<ISubCategoriaGastoService>();
            var subCategoriaGastoValidator = _serviceProvider.GetRequiredService<IValidator<SubCategoriaGastoDto>>();
            var insertUpdateSubCategoriaGastoValidator = _serviceProvider.GetRequiredService<IValidator<InsertUpdateSubCategoriaGastoDto>>();

            _controller = new SubCategoriaGastoController(
                subCategoriaGastoValidator,
                insertUpdateSubCategoriaGastoValidator,
                subCategoriaGastoService);


            _validCat = new InsertUpdateCategoriaGastoDto { Descripcion = "Streeming Services" };
            _validCat2 = new InsertUpdateCategoriaGastoDto { Descripcion = "Alimentos" };


            _validSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Spotify", UsuarioId = 1, CategoriaGastoId = 1 };
            _validSubCat2 = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Netflix", UsuarioId = 1, CategoriaGastoId = 1 };
            _validSubCat3 = new  InsertUpdateSubCategoriaGastoDto { Descripcion = "Supermercado", UsuarioId = 1, CategoriaGastoId = 2 };
            _ivalidSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "", UsuarioId = -1, CategoriaGastoId = -2 };
            _repeatedSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Netflix", UsuarioId = 1, CategoriaGastoId = 1 };
            _createSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Verduleria", UsuarioId = 1, CategoriaGastoId = 2 };


        }

        private async Task<List<SubCategoriaGastoDto>> ArrangeAddStup()
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


            var servicioNecesario = _serviceProvider.GetRequiredService<ISubCategoriaGastoService>();
            var servicioCategoriaGasto = _serviceProvider.GetRequiredService<ICategoriaGastoService>();

            
            _ = await servicioCategoriaGasto.Add(_validCat);
            _ = await servicioCategoriaGasto.Add(_validCat2);


            var addedSubCategoria1 = await servicioNecesario.Add(_validSubCat);
            var addedSubCategoria2 = await servicioNecesario.Add(_validSubCat2);
            var addedSubCategoria3 = await servicioNecesario.Add(_validSubCat3);

            var expectedSubCategorias = new List<SubCategoriaGastoDto> {addedSubCategoria1, addedSubCategoria2, addedSubCategoria3 };
            return expectedSubCategorias;
        }

        [Fact]
        public async Task Get_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var expectedSubCategorias = await ArrangeAddStup();

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<SubCategoriaGastoDto>>(result);

            var resultList = result.ToList();
            Assert.Equal(resultList, expectedSubCategorias);
        }
        
        [Fact]
        public async Task Get_ReturnsEmpitySubCategoriaGastoDtoList()
        {
            // Arrange
            

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<SubCategoriaGastoDto>>(result);
            Assert.Empty(result);
        }

        

        [Fact]
        public async Task Get_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        
        [Fact]
        public async Task GetByCategoriaGasto_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedSubCategorias = await ArrangeAddStup();
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaGastoId == idCat).ToList();
            
            // Act
            var result = await _controller.GetByCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaGastoDto>>(objectResult.Value);

            Assert.NotNull(actualSubCategorias);
            Assert.Equal(expectedSubCategorias, actualSubCategorias);
            Assert.Equal(expectedSubCategorias.Count, actualSubCategorias.Count);
            
        }

        
        [Fact]
        public async Task GetByCategoriaGasto_ReturnsNull()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var idCat = 100L;
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaGastoId == idCat).ToList();

            // Act
            var result = await _controller.GetByCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        
        [Fact]
        public async Task GetByUsuario_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var expectedSubCategorias = await ArrangeAddStup();

            var idUser = expectedSubCategorias.ElementAt(0).UsuarioId;
            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaGastoDto>>(objectResult.Value);

            Assert.NotNull(actualSubCategorias);
            Assert.Equal(expectedSubCategorias.Count, actualSubCategorias.Count);
            Assert.Equal(expectedSubCategorias, actualSubCategorias);
        }

        
        [Fact]
        public async Task GetByUser_ReturnsNull()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var idUser = 200L;
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.UsuarioId == idUser).ToList();

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            Assert.Empty(expectedSubCategorias);
        }


        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();

            var idUser = insertedSubCategorias.ElementAt(0).UsuarioId;
            var idCat = insertedSubCategorias.ElementAt(0).CategoriaGastoId;
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaGastoId == idCat &&
                                                                         s.UsuarioId == idUser).ToList();


            // Act
            var result = await _controller.GetByUserAndCategoriaGasto(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaGastoDto>>(objectResult.Value);

            Assert.NotNull(actualSubCategorias);
            Assert.Equal(expectedSubCategorias, actualSubCategorias);
            Assert.Equal(expectedSubCategorias.Count, actualSubCategorias.Count);
        }

        
        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsNull()
        {
            // Arrange
            var _ = ArrangeAddStup();
            var idUser = 100L;
            var idCat = 100L;
            // Act
            var result = await _controller.GetByUserAndCategoriaGasto(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }



        
        [Fact]
        public async Task Get_ReturnsOk_WhenSubCategoriaGastoDtoFound()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var catSelected = categoriasAgregadas.ElementAt(0);
            // Act
            var result = await _controller.Get(catSelected.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<SubCategoriaGastoDto>(objectResult.Value);

            Assert.NotNull(actualValue);
            Assert.Equal(catSelected, actualValue);
            
        }
        
        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var _ = ArrangeAddStup();

            // Act
            var result = await _controller.Add(_ivalidSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(3, errors.Count);
        }
        
        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var _ = await ArrangeAddStup();

            // Act
            var result = await _controller.Add(_repeatedSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Single(errors);
        }
        
        [Fact]
        public async Task Add_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var insertedSubCats = await ArrangeAddStup();

            // Act
            var result = await _controller.Add(_createSubCat);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
            Assert.IsType<SubCategoriaGastoDto>(createdAtActionResult.Value);
        }
        
        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var insertedSunCategorias = await ArrangeAddStup();
            var selectedSunCategoria = insertedSunCategorias.FirstOrDefault();
            var invalidSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "", UsuarioId = 0, CategoriaGastoId = 0 };
            // Act
            var result = await _controller.Put(selectedSunCategoria.Id, invalidSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(errors.Count, 3);
        }
        
        [Fact]
        public async Task Put_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var selectedSubCategorias = insertedSubCategorias.FirstOrDefault();
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = selectedSubCategorias.Descripcion, CategoriaGastoId = 100, UsuarioId = 100 };

            // Act
            var result = await _controller.Put(selectedSubCategorias.Id, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<string>>(badRequestResult.Value);
            Assert.Equal(errors.Count, 3);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Disney +", CategoriaGastoId = 1, UsuarioId = 1 };
            var idSubCat = 100L;

            // Act
            var result = await _controller.Put(idSubCat, insertDto);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);

        }
        
        [Fact]
        public async Task Put_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var selectedSubCategoria = insertedSubCategorias.FirstOrDefault();
            var insertDto = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Disney +", CategoriaGastoId = selectedSubCategoria.CategoriaGastoId , UsuarioId = selectedSubCategoria.UsuarioId };
            var expectedSubCategoria = new SubCategoriaGastoDto
            {
                Id = selectedSubCategoria.Id,
                Descripcion = insertDto.Descripcion,
                UsuarioId = insertDto.UsuarioId,
                CategoriaGastoId = insertDto.CategoriaGastoId
            };
            // Act
            var result = await _controller.Put(selectedSubCategoria.Id, insertDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedSubCategoria, okResult.Value);
        }
        
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenSubCategoriaGastoDtoNotFound()
        {
            // Arrange
            var _ = await ArrangeAddStup();
            var idSubCat = 100L;

            // Act
            var result = await _controller.Delete(idSubCat);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        
        [Fact]
        public async Task Delete_ReturnsOk_WhenSubCategoriaGastoDtoDeleted()
        {
            // Arrange
            var insertedSubCategoriad = await ArrangeAddStup();
            var selectedSubCategoria = insertedSubCategoriad.FirstOrDefault();

            // Act
            var result = await _controller.Delete(selectedSubCategoria.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedSubCategoria, okResult.Value);
            Assert.Null(_context.GastosSubcategoriagastos.FirstOrDefault(s => s.Id == selectedSubCategoria.Id 
                                                                                && s.Baja == false));
        }
    }   
}
