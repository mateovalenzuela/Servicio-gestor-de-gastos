using BackendGastos.Controller.Controllers;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Test
{
    public class GastoTest
    {

        private readonly ServiceProvider _serviceProvider;
        private readonly GastoController _controller;
        private readonly ProyectoGastosTestContext _context;

        private readonly InsertUpdateGastoDto _validGasto;
        private readonly InsertUpdateGastoDto _validGasto1;
        private readonly InsertUpdateGastoDto _validGasto2;
        private readonly InsertUpdateGastoDto _validGasto3;
        private readonly InsertUpdateGastoDto _validGasto4;
        private readonly InsertUpdateGastoDto _validGasto5;
        private readonly InsertUpdateGastoDto _validGasto6;

        private readonly InsertUpdateGastoDto _invalidGasto;
        private readonly InsertUpdateGastoDto _gastoNotFound;
        private readonly InsertUpdateGastoDto _createGasto;
        private readonly InsertUpdateGastoDto _repeatedGasto;


        private readonly InsertUpdateCategoriaGastoDto _validCat;
        private readonly InsertUpdateCategoriaGastoDto _validCat2;
        private readonly InsertUpdateCategoriaGastoDto _validCat3;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat2;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat3;
        private readonly InsertUpdateSubCategoriaGastoDto _validSubCat4;

        public GastoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);
            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var gastoService = _serviceProvider.GetRequiredService<IGastoService>();
            var gastoValidator = _serviceProvider.GetRequiredService<IValidator<GastoDto>>();
            var insertUpdateGastoValidator = _serviceProvider.GetRequiredService<IValidator<InsertUpdateGastoDto>>();

            _controller = new GastoController(
                gastoValidator,
                insertUpdateGastoValidator,
                gastoService
            );

            _validGasto = new InsertUpdateGastoDto
            {
                Descripcion = "Comida",
                Importe = 100000.00M,
                CategoriaGastoId = 1,
                MonedaId = 1,
                SubcategoriaGastoId = 1,
                UsuarioId = 1,
            };

            _validGasto2 = new InsertUpdateGastoDto
            {
                Descripcion = "Carniceria",
                Importe = 10000.00M,
                CategoriaGastoId = 1,
                MonedaId = 1,
                SubcategoriaGastoId = 1,
                UsuarioId = 1,
            };

            _validGasto3 = new InsertUpdateGastoDto
            {
                Descripcion = "Supermercado",
                Importe = 1000.00M,
                CategoriaGastoId = 1,
                MonedaId = 1,
                SubcategoriaGastoId = 2,
                UsuarioId = 1,
            };

            _validGasto4 = new InsertUpdateGastoDto
            {
                Descripcion = "Gym",
                Importe = 10000.00M,
                CategoriaGastoId = 2,
                MonedaId = 1,
                SubcategoriaGastoId = 3,
                UsuarioId = 1,
            };

            _validGasto5 = new InsertUpdateGastoDto
            {
                Descripcion = "Cuota Natación",
                Importe = 1000.00M,
                CategoriaGastoId = 2,
                MonedaId = 1,
                SubcategoriaGastoId = 3,
                UsuarioId = 1,
            };

            _validGasto6 = new InsertUpdateGastoDto
            {
                Descripcion = "Ropa Addidas",
                Importe = 100000.00M,
                CategoriaGastoId = 3,
                MonedaId = 1,
                SubcategoriaGastoId = 4,
                UsuarioId = 1,
            };


            _repeatedGasto = new InsertUpdateGastoDto
            {
                Descripcion = "Comida",
                Importe = 100000.00M,
                CategoriaGastoId = 1,
                MonedaId = 1,
                SubcategoriaGastoId = 1,
                UsuarioId = 1,
            };

            _invalidGasto = new InsertUpdateGastoDto
            {
                Descripcion = "",
                Importe = 100000.000005453M,
                CategoriaGastoId = 0,
                MonedaId = 0,
                SubcategoriaGastoId = 0,
                UsuarioId = 0,
            };


            _gastoNotFound = new InsertUpdateGastoDto
            {
                Descripcion = "Comida",
                Importe = 100000.00M,
                CategoriaGastoId = 10,
                MonedaId = 10,
                SubcategoriaGastoId = 10,
                UsuarioId = 10,
            };

            _createGasto = new InsertUpdateGastoDto
            {
                Descripcion = "Conjunto Nike",
                Importe = 10000.00M,
                CategoriaGastoId = 3,
                MonedaId = 2,
                SubcategoriaGastoId = 4,
                UsuarioId = 1,
            };

            _validCat = new InsertUpdateCategoriaGastoDto { Descripcion = "Comida" };
            _validCat2 = new InsertUpdateCategoriaGastoDto { Descripcion = "Deportes" };
            _validCat3 = new InsertUpdateCategoriaGastoDto { Descripcion = "Ropa" };

            _validSubCat = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Kiosco", UsuarioId = 1, CategoriaGastoId = 1 };
            _validSubCat2 = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Supermercado LA", UsuarioId = 1, CategoriaGastoId = 1 };
            _validSubCat3 = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Cuotas de Servicios", UsuarioId = 1, CategoriaGastoId = 2 };
            _validSubCat4 = new InsertUpdateSubCategoriaGastoDto { Descripcion = "Ropa de Marca", UsuarioId = 1, CategoriaGastoId = 3 };

        }

        private async Task<List<GastoDto>> ArrangeAddSetup()
        {
            var usuarios = new List<AuthenticationUsuario>
            {
                new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "password1",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
                new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "password2",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            };
            await _context.AuthenticationUsuarios.AddRangeAsync(usuarios);
            await _context.SaveChangesAsync();

            var monedas = new List<GastosMonedum>
            {
                new() {Id = 1, Descripcion = "ARS", FechaCreacion = DateTime.UtcNow, Baja = false},
                new() {Id = 2, Descripcion = "USD", FechaCreacion = DateTime.UtcNow, Baja = false}
            };
            await _context.GastosMoneda.AddRangeAsync(monedas);
            await _context.SaveChangesAsync();


            var servicioNecesario = _serviceProvider.GetRequiredService<ISubCategoriaGastoService>();
            var servicioCategoriaIngreso = _serviceProvider.GetRequiredService<ICategoriaGastoService>();
            var servicioIngreso = _serviceProvider.GetRequiredService<IGastoService>();

            _ = await servicioCategoriaIngreso.Add(_validCat);
            _ = await servicioCategoriaIngreso.Add(_validCat2);
            _ = await servicioCategoriaIngreso.Add(_validCat3);

            _ = await servicioNecesario.Add(_validSubCat);
            _ = await servicioNecesario.Add(_validSubCat2);
            _ = await servicioNecesario.Add(_validSubCat3);
            _ = await servicioNecesario.Add(_validSubCat4);

            var addedGasto = await servicioIngreso.Add(_validGasto);
            var addedGasto2 = await servicioIngreso.Add(_validGasto2);
            var addedGasto3 = await servicioIngreso.Add(_validGasto3);
            var addedGasto4 = await servicioIngreso.Add(_validGasto4);
            var addedGasto5 = await servicioIngreso.Add(_validGasto5);
            var addedGasto6 = await servicioIngreso.Add(_validGasto6);

            var addedGastos = new List<GastoDto> {addedGasto, addedGasto2, addedGasto3,
                                                    addedGasto4, addedGasto5, addedGasto6};
            return addedGastos;
        }


        [Fact]
        private async Task Get_ReturnsGastoDtoList()
        {
            // Arrange
            var expectedGastos = await ArrangeAddSetup();

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<GastoDto>>(result);
            var resultList = result.ToList();
            Assert.Equal(expectedGastos, resultList);
        }

        [Fact]
        private async Task Get_ReturnsEmptyGastoDtoList()
        {
            // Arrange

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<GastoDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenGastoDtoNotFound()
        {
            // Arrange

            //Act
            var result = await _controller.Get(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsGastoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedGastos = await ArrangeAddSetup();
            var expectedGastos = insertedGastos.Where(i => i.CategoriaGastoId == idCat).ToList();

            // Act
            var result = await _controller.GetByCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGastos = Assert.IsType<List<GastoDto>>(objectResult.Value);

            Assert.NotNull(actualGastos);
            Assert.Equal(expectedGastos, actualGastos);
            Assert.Equal(expectedGastos.Count, actualGastos.Count);
        }


        [Fact]
        public async Task GetByCategoriaGasto_ReturnsNull()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var idCat = 100L;
            var expectedGastos = insertedGastos.Where(i => i.CategoriaGastoId == idCat).ToList();

            //Act
            var result = await _controller.GetByCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenGastoDtoFound()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var gastoSelected = insertedGastos.ElementAt(0);

            // Act
            var result = await _controller.Get(gastoSelected.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<GastoDto>(objectResult.Value);

            Assert.NotNull(actualValue);
            Assert.Equal(gastoSelected, actualValue);
        }


        [Fact]
        public async Task GetByUsuario_ReturnsSubCategoriaGastoDtoList()
        {
            // Arrange
            var expectedGastos = await ArrangeAddSetup();
            var idUser = expectedGastos.ElementAt(0).UsuarioId;

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGastos = Assert.IsType<List<GastoDto>>(objectResult.Value);

            Assert.NotNull(actualGastos);
            Assert.Equal(expectedGastos.Count, actualGastos.Count);
            Assert.Equal(expectedGastos, actualGastos);
        }

        [Fact]
        public async Task GetByUser_ReturnsNull()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var idUser = 200L;
            var expectedGasto = insertedGastos.Where(s => s.UsuarioId == idUser).ToList();

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            Assert.Empty(expectedGasto);
        }


        [Fact]
        public async Task GetBySubCategoriaGasto_ReturnsGastoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedGastos = await ArrangeAddSetup();
            var expectedGastos = insertedGastos.Where(i => i.SubcategoriaGastoId == idCat).ToList();

            // Act
            var result = await _controller.GetBySubCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGastos = Assert.IsType<List<GastoDto>>(objectResult.Value);

            Assert.NotNull(actualGastos);
            Assert.Equal(expectedGastos, actualGastos);
            Assert.Equal(expectedGastos.Count, actualGastos.Count);
        }



        [Fact]
        public async Task GetBySubCategoriaGasto_ReturnsNull()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var idCat = 100L;
            var expectedGastos = insertedGastos.Where(i => i.SubcategoriaGastoId == idCat).ToList();

            //Act
            var result = await _controller.GetBySubCategoriaGasto(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }


        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsGastoDtoList()
        {
            // Arrange 
            var insertedGastos = await ArrangeAddSetup();

            var idUser = insertedGastos.ElementAt(0).UsuarioId;
            var idCat = insertedGastos.ElementAt(0).CategoriaGastoId;
            var expectedGastos = insertedGastos.Where(s => s.CategoriaGastoId == idCat && s.UsuarioId == idUser).ToList();

            // act
            var result = await _controller.GetByUserAndCategoriaGasto(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGastos = Assert.IsType<List<GastoDto>>(objectResult.Value);

            Assert.NotNull(actualGastos);
            Assert.Equal(expectedGastos, actualGastos);
            Assert.Equal(expectedGastos.Count, actualGastos.Count);
        }

        [Fact]
        public async Task GetByUserAndCategoriaGasto_ReturnsNull()
        {
            // Arrange
            var _ = ArrangeAddSetup();
            var idUser = 100L;
            var idCat = 100L;

            // Act
            var result = await _controller.GetByUserAndCategoriaGasto(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }



        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var _ = ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_invalidGasto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(6, errors.Count);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var _ = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_gastoNotFound);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<Dictionary<string, string>>(badRequestResult.Value);
            errors.ContainsKey("Categoria");
            errors.ContainsKey("Subcategoria");
            errors.ContainsKey("Moneda");
            errors.ContainsKey("Usuario");
        }

        [Fact]
        public async Task Add_ReturnsOk_WhenSubCategoriaGastoDtoIsValid()
        {
            // Arrange
            var _ = ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_createGasto);

            // Assert
            var okObjectResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualGasto = Assert.IsType<GastoDto>(okObjectResult.Value);

            Assert.Equal(_createGasto.Descripcion, actualGasto.Descripcion);
            Assert.Equal(_createGasto.UsuarioId, actualGasto.UsuarioId);
            Assert.Equal(_createGasto.CategoriaGastoId, actualGasto.CategoriaGastoId);
            Assert.Equal(_createGasto.SubcategoriaGastoId, actualGasto.SubcategoriaGastoId);
            Assert.Equal(_createGasto.MonedaId, actualGasto.MonedaId);
            Assert.Equal(_createGasto.Importe, actualGasto.Importe);
        }


        [Fact]
        public async Task Add_ReturnsBadRequest_WhenIngresoDtoIsRepeated()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_repeatedGasto);

            // Assert
            var createdRequestResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualGasto = Assert.IsType<GastoDto>(createdRequestResult.Value);
            var getGasto = await _serviceProvider.GetRequiredService<IGastoService>().Get();
            Assert.Contains(actualGasto, getGasto);
            Assert.Equal(insertedGastos.Count() + 1, getGasto.Count());
        }


        [Fact]
        public async Task Put_ReturnsOk_WhenSubCategoriaGastoDtoIsValid()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var selectedGasto = insertedGastos.ElementAt(1);
            var updateCat = new InsertUpdateGastoDto
            {
                Descripcion = "Updated Gasto",
                Importe = 9999.55M,
                UsuarioId = selectedGasto.UsuarioId,
                CategoriaGastoId = selectedGasto.CategoriaGastoId,
                SubcategoriaGastoId = selectedGasto.SubcategoriaGastoId,
                MonedaId = selectedGasto.MonedaId,
            };


            // Act
            var result = await _controller.Put(selectedGasto.Id, updateCat);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGasto = Assert.IsType<GastoDto>(okObjectResult.Value);
            Assert.NotNull(actualGasto);
            Assert.Equal(selectedGasto.Id, actualGasto.Id);
            Assert.Equal(updateCat.Descripcion, actualGasto.Descripcion);
            Assert.Equal(updateCat.Importe, actualGasto.Importe);
            Assert.Equal(updateCat.UsuarioId, actualGasto.UsuarioId);
            Assert.Equal(updateCat.CategoriaGastoId, actualGasto.CategoriaGastoId);
            Assert.Equal(updateCat.SubcategoriaGastoId, actualGasto.SubcategoriaGastoId);
            Assert.Equal(updateCat.MonedaId, actualGasto.MonedaId);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenSubCategoriaGastoDtoIsInvalid()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var selectedGasto = insertedGastos.ElementAt(0);

            // Act
            var result = await _controller.Put(selectedGasto.Id, _invalidGasto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(6, errors.Count);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenGastoDtoNotExist()
        {
            // Arrange
            var _ = await ArrangeAddSetup();
            var insertDto = new InsertUpdateGastoDto
            {
                Descripcion = "Update Gasto",
                CategoriaGastoId = 1,
                UsuarioId = 1,
                Importe = 9999.00M,
                SubcategoriaGastoId = 1,
                MonedaId = 2,
            };

            // Act
            var result = await _controller.Put(100, insertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenGastoDtoRepeated()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var selectedGasto = insertedGastos.ElementAt(1);
            var selectedGasto2 = insertedGastos.ElementAt(2);
            var updateCat = new InsertUpdateGastoDto
            {
                Descripcion = selectedGasto2.Descripcion,
                UsuarioId = selectedGasto2.UsuarioId,
                CategoriaGastoId = selectedGasto2.CategoriaGastoId,
                SubcategoriaGastoId = selectedGasto2.SubcategoriaGastoId,
                MonedaId =  selectedGasto2.MonedaId,
                Importe = selectedGasto2.Importe,
            };

            // Act
            var result = await _controller.Put(selectedGasto.Id, updateCat);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGasto = Assert.IsType<GastoDto>(okObjectResult.Value);
            Assert.NotNull(actualGasto);
            Assert.Equal(selectedGasto.Id, actualGasto.Id);
            Assert.Equal(updateCat.Descripcion, actualGasto.Descripcion);
            Assert.Equal(updateCat.Importe, actualGasto.Importe);
            Assert.Equal(updateCat.UsuarioId, actualGasto.UsuarioId);
            Assert.Equal(updateCat.CategoriaGastoId, actualGasto.CategoriaGastoId);
            Assert.Equal(updateCat.SubcategoriaGastoId, actualGasto.SubcategoriaGastoId);
            Assert.Equal(updateCat.MonedaId, actualGasto.MonedaId);
        }

        [Fact]
        public async Task Delete_ReturnsOK_WhenGastoDtoDeleted()
        {
            // Arrange
            var insertedGastos = await ArrangeAddSetup();
            var selectedGastos = insertedGastos.ElementAt(1);

            // Act
            var result = await _controller.Delete(selectedGastos.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedGastos, okResult.Value);
            Assert.Null(_context.GastosGastos.FirstOrDefault(s => s.Id == selectedGastos.Id
                                                                                && s.Baja == false));
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenGastoDtoNotExist()
        {
            var _ = await ArrangeAddSetup();
            var result = await _controller.Delete(100);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
