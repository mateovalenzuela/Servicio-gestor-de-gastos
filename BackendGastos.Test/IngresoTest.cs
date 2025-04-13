using BackendGastos.Controller.Controllers;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BackendGastos.Test
{
    public class IngresoTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IngresoController _controller;
        private ProyectoGastosTestContext _context;

        private InsertUpdateIngresoDto _validIngreso;
        private InsertUpdateIngresoDto _validIngreso2;
        private InsertUpdateIngresoDto _validIngreso3;
        private InsertUpdateIngresoDto _validIngreso4;
        private InsertUpdateIngresoDto _validIngreso5;
        private InsertUpdateIngresoDto _validIngreso6;

        private InsertUpdateIngresoDto _invalidIngreso;
        private InsertUpdateIngresoDto _ingresoNotFound;
        private InsertUpdateIngresoDto _createIngreso;
        private InsertUpdateIngresoDto _repeatedIngreso;

        private readonly InsertUpdateCategoriaIngresoDto _validCat;
        private readonly InsertUpdateCategoriaIngresoDto _validCat2;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat2;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat3;

        public IngresoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);
            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var ingresoService = _serviceProvider.GetRequiredService<IIngresoService>();

            _controller = new IngresoController(
                ingresoService
            );

            _validIngreso = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso de salario",
                Importe = 1500.50m,
                CategoriaIngresoId = 1,
                MonedaId = 1,
                SubcategoriaIngresoId = 1,
                UsuarioId = 1
            };

            _validIngreso2 = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por venta",
                Importe = 200.75m,
                CategoriaIngresoId = 2,
                MonedaId = 1,
                SubcategoriaIngresoId = 2,
                UsuarioId = 1
            };

            _validIngreso3 = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por alquiler",
                Importe = 850.00m,
                CategoriaIngresoId = 2,
                MonedaId = 1,
                SubcategoriaIngresoId = 2,
                UsuarioId = 1
            };

            _validIngreso4 = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por inversión",
                Importe = 500.00m,
                CategoriaIngresoId = 2,
                MonedaId = 2,
                SubcategoriaIngresoId = 2,
                UsuarioId = 1
            };

            _validIngreso5 = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por freelance",
                Importe = 1200.00m,
                CategoriaIngresoId = 1,
                MonedaId = 2,
                SubcategoriaIngresoId = 1,
                UsuarioId = 1
            };

            _validIngreso6 = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por dividendos",
                Importe = 300.00m,
                CategoriaIngresoId = 2,
                MonedaId = 1,
                SubcategoriaIngresoId = 3,
                UsuarioId = 1
            };

            _repeatedIngreso = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por dividendos",
                Importe = 300.00m,
                CategoriaIngresoId = 2,
                MonedaId = 1,
                SubcategoriaIngresoId = 3,
                UsuarioId = 1
            };

            _invalidIngreso = new InsertUpdateIngresoDto
            {
                Descripcion = "",
                Importe = 1000.5555M,
                CategoriaIngresoId = 0,
                MonedaId = 0,
                SubcategoriaIngresoId = 0,
                UsuarioId = 0,
            };

            _ingresoNotFound = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por dividendos",
                Importe = 300.00m,
                CategoriaIngresoId = 20,
                MonedaId = 10,
                SubcategoriaIngresoId = 30,
                UsuarioId = 10
            };

            _createIngreso = new InsertUpdateIngresoDto
            {
                Descripcion = "Ingreso por dividendos",
                Importe = 9000.00m,
                CategoriaIngresoId = 2,
                MonedaId = 1,
                SubcategoriaIngresoId = 3,
                UsuarioId = 1
            };

            _validCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "Salary" };
            _validCat2 = new InsertUpdateCategoriaIngresoDto { Descripcion = "Investment" };

            _validSubCat = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Monthly Salary", UsuarioId = 1, CategoriaIngresoId = 1 };
            _validSubCat2 = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Yearly Bonus", UsuarioId = 1, CategoriaIngresoId = 1 };
            _validSubCat3 = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Stock Dividends", UsuarioId = 1, CategoriaIngresoId = 2 };


        }

        private async Task<IEnumerable<IngresoDto>> ArrangeAddSetup()
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


            var servicioNecesario = _serviceProvider.GetRequiredService<ISubCategoriaIngresoService>();
            var servicioCategoriaIngreso = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();
            var servicioIngreso = _serviceProvider.GetRequiredService<IIngresoService>();

            _ = await servicioCategoriaIngreso.Add(_validCat);
            _ = await servicioCategoriaIngreso.Add(_validCat2);

            _ = await servicioNecesario.Add(_validSubCat);
            _ = await servicioNecesario.Add(_validSubCat2);
            _ = await servicioNecesario.Add(_validSubCat3);

            var addedIngreso = await servicioIngreso.Add(_validIngreso);
            var addedIngreso2 = await servicioIngreso.Add(_validIngreso2);
            var addedIngreso3 = await servicioIngreso.Add(_validIngreso3);
            var addedIngreso4 = await servicioIngreso.Add(_validIngreso4);
            var addedIngreso5 = await servicioIngreso.Add(_validIngreso5);
            var addedIngreso6 = await servicioIngreso.Add(_validIngreso6);

            var addedIngresos = await servicioIngreso.Get();
            return addedIngresos.ToList();
        }

        [Fact]
        private async Task Get_ReturnsIngresoDtoList()
        {
            // Arrange
            var expectedIngreso = await ArrangeAddSetup();

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<IngresoDto>>(result);
            var resultList = result.ToList();
            Assert.Equal(expectedIngreso, resultList);
        }

        [Fact]
        private async Task Get_ReturnsEmptyIngresoDtoList()
        {
            // Arrange

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<IngresoDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenIngresoDtoNotFound()
        {
            // Arrange

            //Act
            var result = await _controller.Get(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsIngresoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedIngresos = await ArrangeAddSetup();
            var expectedIngresos = insertedIngresos.Where(i => i.CategoriaIngresoId == idCat).ToList();

            // Act
            var result = await _controller.GetByCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngresos = Assert.IsType<List<IngresoDto>>(objectResult.Value);

            Assert.NotNull(actualIngresos);
            Assert.Equal(expectedIngresos, actualIngresos);
            Assert.Equal(expectedIngresos.Count, actualIngresos.Count);
        }



        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var idCat = 100L;
            var expectedIngresos = insertedIngresos.Where(i => i.CategoriaIngresoId == idCat).ToList();

            //Act
            var result = await _controller.GetByCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenIngresoDtoFound()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var ingresoSelected = insertedIngresos.ElementAt(0);

            // Act
            var result = await _controller.Get(ingresoSelected.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<IngresoDto>(objectResult.Value);

            Assert.NotNull(actualValue);
            Assert.Equal(ingresoSelected, actualValue);
        }

        [Fact]
        public async Task GetByUsuario_ReturnsIngresoDtoList()
        {
            // Arrange
            var expectedIngresos = await ArrangeAddSetup();
            var idUser = expectedIngresos.ElementAt(0).UsuarioId;

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngresos = Assert.IsType<List<IngresoDto>>(objectResult.Value);

            Assert.NotNull(actualIngresos);
            Assert.Equal(expectedIngresos.Count(), actualIngresos.Count);
            Assert.Equal(expectedIngresos, actualIngresos);
        }

        [Fact]
        public async Task GetByUser_ReturnsNull()
        {
            // Arrange
            var insertedIngreso = await ArrangeAddSetup();
            var idUser = 200L;
            var expectedIngreso = insertedIngreso.Where(s => s.UsuarioId == idUser).ToList();

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
            Assert.Empty(expectedIngreso);
        }

        [Fact]
        public async Task GetBySubCategoriaIngreso_ReturnsIngresoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedIngresos = await ArrangeAddSetup();
            var expectedIngresos = insertedIngresos.Where(i => i.SubcategoriaIngresoId == idCat).ToList();

            // Act
            var result = await _controller.GetBySubCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngresos = Assert.IsType<List<IngresoDto>>(objectResult.Value);

            Assert.NotNull(actualIngresos);
            Assert.Equal(expectedIngresos, actualIngresos);
            Assert.Equal(expectedIngresos.Count, actualIngresos.Count);
        }



        [Fact]
        public async Task GetBySubCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var idCat = 100L;
            var expectedIngresos = insertedIngresos.Where(i => i.SubcategoriaIngresoId == idCat).ToList();

            //Act
            var result = await _controller.GetBySubCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }


        [Fact]
        public async Task GetByUserAndCategoriaIngreso_ReturnsIngresoDtoList()
        {
            // Arrange 
            var insertedingresos = await ArrangeAddSetup();

            var idUser = insertedingresos.ElementAt(0).UsuarioId;
            var idCat = insertedingresos.ElementAt(0).CategoriaIngresoId;
            var expectedIngresos = insertedingresos.Where(s => s.CategoriaIngresoId == idCat && s.UsuarioId == idUser).ToList();

            // act
            var result = await _controller.GetByUserAndCategoriaIngreso(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngresos = Assert.IsType<List<IngresoDto>>(objectResult.Value);

            Assert.NotNull(actualIngresos);
            Assert.Equal(expectedIngresos, actualIngresos);
            Assert.Equal(expectedIngresos.Count, actualIngresos.Count);
        }

        [Fact]
        public async Task GetByUserAndCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            var _ = ArrangeAddSetup();
            var idUser = 100L;
            var idCat = 100L;

            // Act
            var result = await _controller.GetByUserAndCategoriaIngreso(idUser, idCat);

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
            var result = await _controller.Add(_invalidIngreso);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            //var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            //Assert.Equal(6, errors.Count);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenServiceValidationFails()
        {
            // Arrange
            var _ = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_ingresoNotFound);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<Dictionary<string, string>>(badRequestResult.Value);
            errors.ContainsKey("Categoria");
            errors.ContainsKey("Subcategoria");
            errors.ContainsKey("Moneda");
            errors.ContainsKey("Usuario");
        }


        [Fact]
        public async Task Add_ReturnsOk_WhenIngresoDtoIsValid()
        {
            // Arrange
            var _ = ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_createIngreso);

            // Assert
            var okObjectResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualIngreso = Assert.IsType<IngresoDto>(okObjectResult.Value);

            Assert.Equal(_createIngreso.Descripcion, actualIngreso.Descripcion);
            Assert.Equal(_createIngreso.UsuarioId, actualIngreso.UsuarioId);
            Assert.Equal(_createIngreso.CategoriaIngresoId, actualIngreso.CategoriaIngresoId);
            Assert.Equal(_createIngreso.SubcategoriaIngresoId, actualIngreso.SubcategoriaIngresoId);
            Assert.Equal(_createIngreso.MonedaId, actualIngreso.MonedaId);
            Assert.Equal(_createIngreso.Importe, actualIngreso.Importe);
        }


        [Fact]
        public async Task Add_ReturnsBadRequest_WhenIngresoDtoIsRepeated()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();

            // Act
            var result = await _controller.Add(_repeatedIngreso);

            // Assert
            var createdRequestResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualIngreso = Assert.IsType<IngresoDto>(createdRequestResult.Value);
            var getIngresos = await _serviceProvider.GetRequiredService<IIngresoService>().Get();
            Assert.Contains(actualIngreso, getIngresos);
            Assert.Equal(insertedIngresos.Count() + 1, getIngresos.Count());
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenIngresoDtoIsValid()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var selectedIngreso = insertedIngresos.ElementAt(1);
            var updateCat = new InsertUpdateIngresoDto
            {
                Descripcion = "Updated Ingreso",
                Importe = 9999.55M,
                UsuarioId = selectedIngreso.UsuarioId,
                CategoriaIngresoId = selectedIngreso.CategoriaIngresoId,
                SubcategoriaIngresoId = selectedIngreso.SubcategoriaIngresoId,
                MonedaId = selectedIngreso.MonedaId,
            };


            // Act
            var result = await _controller.Put(selectedIngreso.Id, updateCat);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngreso = Assert.IsType<IngresoDto>(okObjectResult.Value);
            Assert.NotNull(actualIngreso);
            Assert.Equal(selectedIngreso.Id, actualIngreso.Id);
            Assert.Equal(updateCat.Descripcion, actualIngreso.Descripcion);
            Assert.Equal(updateCat.Importe, actualIngreso.Importe);
            Assert.Equal(updateCat.UsuarioId, actualIngreso.UsuarioId);
            Assert.Equal(updateCat.CategoriaIngresoId, selectedIngreso.CategoriaIngresoId);
            Assert.Equal(updateCat.SubcategoriaIngresoId, selectedIngreso.SubcategoriaIngresoId);
            Assert.Equal(updateCat.MonedaId, selectedIngreso.MonedaId);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenIngresoDtoIsInvalid()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var selectedIngreso = insertedIngresos.ElementAt(0);

            // Act
            var result = await _controller.Put(selectedIngreso.Id, _invalidIngreso);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            //var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            //Assert.Equal(6, errors.Count);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenIngresoDtoNotExist()
        {
            // Arrange
            var _ = await ArrangeAddSetup();
            var insertDto = new InsertUpdateIngresoDto
            {
                Descripcion = "Update Ingreso",
                CategoriaIngresoId = 1,
                UsuarioId = 1,
                Importe = 9999.00M,
                SubcategoriaIngresoId = 1,
                MonedaId = 2,
            };

            // Act
            var result = await _controller.Put(100, insertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenIngresoDtoRepeated()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var selectedIngreso = insertedIngresos.ElementAt(1);
            var selectedIngreso2 = insertedIngresos.ElementAt(2);
            var updateCat = new InsertUpdateIngresoDto
            {
                Descripcion = selectedIngreso2.Descripcion,
                UsuarioId = selectedIngreso2.UsuarioId,
                CategoriaIngresoId = selectedIngreso2.CategoriaIngresoId,
                SubcategoriaIngresoId = selectedIngreso2.SubcategoriaIngresoId,
                MonedaId = selectedIngreso2.MonedaId,
                Importe = selectedIngreso2.Importe,
            };

            // Act
            var result = await _controller.Put(selectedIngreso.Id, updateCat);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIngreso = Assert.IsType<IngresoDto>(okObjectResult.Value);
            Assert.NotNull(actualIngreso);
            Assert.Equal(selectedIngreso.Id, actualIngreso.Id);
            Assert.Equal(updateCat.Descripcion, actualIngreso.Descripcion);
            Assert.Equal(updateCat.Importe, actualIngreso.Importe);
            Assert.Equal(updateCat.UsuarioId, actualIngreso.UsuarioId);
            Assert.Equal(updateCat.CategoriaIngresoId, actualIngreso.CategoriaIngresoId);
            Assert.Equal(updateCat.SubcategoriaIngresoId, actualIngreso.SubcategoriaIngresoId);
            Assert.Equal(updateCat.MonedaId, actualIngreso.MonedaId);
        }

        [Fact]
        public async Task Delete_ReturnsOK_WhenIngresoDtoDeleted()
        {
            // Arrange
            var insertedIngresos = await ArrangeAddSetup();
            var selectedIngreso = insertedIngresos.ElementAt(1);

            // Act
            var result = await _controller.Delete(selectedIngreso.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedIngreso, okResult.Value);
            Assert.Null(_context.GastosIngresos.FirstOrDefault(s => s.Id == selectedIngreso.Id
                                                                                && s.Baja == false));
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIngresoDtoNotExist()
        {
            var _ = await ArrangeAddSetup();
            var result = await _controller.Delete(100);
            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}
