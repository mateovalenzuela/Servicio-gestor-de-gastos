using BackendGastos.Controller.Controllers;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BackendGastos.Test
{
    public class SubCategoriaIngresoTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly SubCategoriaIngresoController _controller;
        private readonly ProyectoGastosTestContext _context;
        private readonly InsertUpdateCategoriaIngresoDto _validCat;
        private readonly InsertUpdateCategoriaIngresoDto _validCat2;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat2;
        private readonly InsertUpdateSubCategoriaIngresoDto _validSubCat3;
        private readonly InsertUpdateSubCategoriaIngresoDto _invalidSubCat;
        private readonly InsertUpdateSubCategoriaIngresoDto _repeatedSubCat;
        private readonly InsertUpdateSubCategoriaIngresoDto _createSubCat;

        public SubCategoriaIngresoTest()
        {
            _serviceProvider = ProgramTest.GetServices(true);
            _context = _serviceProvider.GetRequiredService<ProyectoGastosTestContext>();

            var subCategoriaIngresoService = _serviceProvider.GetRequiredService<ISubCategoriaIngresoService>();

            _controller = new SubCategoriaIngresoController(
                subCategoriaIngresoService);

            _validCat = new InsertUpdateCategoriaIngresoDto { Descripcion = "Salary" };
            _validCat2 = new InsertUpdateCategoriaIngresoDto { Descripcion = "Investment" };

            _validSubCat = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Monthly Salary", UsuarioId = 1, CategoriaIngresoId = 1 };
            _validSubCat2 = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Yearly Bonus", UsuarioId = 1, CategoriaIngresoId = 1 };
            _validSubCat3 = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Stock Dividends", UsuarioId = 1, CategoriaIngresoId = 2 };
            _invalidSubCat = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "", UsuarioId = -1, CategoriaIngresoId = -2 };
            _repeatedSubCat = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Yearly Bonus", UsuarioId = 1, CategoriaIngresoId = 1 };
            _createSubCat = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Freelance Income", UsuarioId = 1, CategoriaIngresoId = 2 };
        }

        private async Task<List<SubCategoriaIngresoDto>> ArrangeAddStup()
        {
            var usuarios = new List<AuthenticationUsuario>
            {
                new() { Id = 1, Username = "Test 1", Email = "usuario_test1@example.com", Password = "password1",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
                new() { Id = 2, Username = "Test 2", Email = "usuario_test2@example.com", Password = "password2",
                    IsActive = true, IsSuperuser = false,  EmailConfirmado = true, IsStaff = false, },
            };

            _context.AuthenticationUsuarios.AddRange(usuarios);
            _context.SaveChanges();

            var servicioNecesario = _serviceProvider.GetRequiredService<ISubCategoriaIngresoService>();
            var servicioCategoriaIngreso = _serviceProvider.GetRequiredService<ICategoriaIngresoService>();

            _ = await servicioCategoriaIngreso.Add(_validCat);
            _ = await servicioCategoriaIngreso.Add(_validCat2);

            var addedSubCategoria1 = await servicioNecesario.Add(_validSubCat);
            var addedSubCategoria2 = await servicioNecesario.Add(_validSubCat2);
            var addedSubCategoria3 = await servicioNecesario.Add(_validSubCat3);

            var expectedSubCategorias = new List<SubCategoriaIngresoDto> { addedSubCategoria1, addedSubCategoria2, addedSubCategoria3 };
            return expectedSubCategorias;
        }

        [Fact]
        public async Task Get_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var expectedSubCategorias = await ArrangeAddStup();

            // Act
            var result = await _controller.Get();

            //Assert
            Assert.IsType<List<SubCategoriaIngresoDto>>(result);
            var resultList = result.ToList();
            Assert.Equal(resultList, expectedSubCategorias);
        }

        [Fact]
        public async Task Get_ReturnsEmptySubCategoriaIngresoDtoList()
        {
            // Arrange

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<List<SubCategoriaIngresoDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotFound()
        {
            // Arrange

            //Act
            var result = await _controller.Get(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var idCat = 1L;
            var insertedSubCategorias = await ArrangeAddStup();
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaIngresoId == idCat).ToList();

            // Act
            var result = await _controller.GetByCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaIngresoDto>>(objectResult.Value);

            Assert.NotNull(actualSubCategorias);
            Assert.Equal(expectedSubCategorias, actualSubCategorias);
            Assert.Equal(expectedSubCategorias.Count, actualSubCategorias.Count);
        }

        [Fact]
        public async Task GetByCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            var insertedSubCategorias = await ArrangeAddStup();
            var idCat = 100L;
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaIngresoId == idCat).ToList();

            //Act
            var result = await _controller.GetByCategoriaIngreso(idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetByUsuario_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange
            var expectedSubCategorias = await ArrangeAddStup();
            var idUser = expectedSubCategorias.ElementAt(0).UsuarioId;

            // Act
            var result = await _controller.GetByUser(idUser);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaIngresoDto>>(objectResult.Value);

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
        public async Task GetByUserAndCategoriaIngreso_ReturnsSubCategoriaIngresoDtoList()
        {
            // Arrange 
            var insertedSubCategorias = await ArrangeAddStup();

            var idUser = insertedSubCategorias.ElementAt(0).UsuarioId;
            var idCat = insertedSubCategorias.ElementAt(0).CategoriaIngresoId;
            var expectedSubCategorias = insertedSubCategorias.Where(s => s.CategoriaIngresoId == idCat && s.UsuarioId == idUser).ToList();

            // act
            var result = await _controller.GetByUserAndCategoriaIngreso(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategorias = Assert.IsType<List<SubCategoriaIngresoDto>>(objectResult.Value);

            Assert.NotNull(actualSubCategorias);
            Assert.Equal(expectedSubCategorias, actualSubCategorias);
            Assert.Equal(expectedSubCategorias.Count, actualSubCategorias.Count);
        }

        [Fact]
        public async Task GetByUserAndCategoriaIngreso_ReturnsNull()
        {
            // Arrange
            var _ = ArrangeAddStup();
            var idUser = 100L;
            var idCat = 100L;

            // Act
            var result = await _controller.GetByUserAndCategoriaIngreso(idUser, idCat);

            // Assert
            var objectResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenSubCategoriaIngresoDtoFound()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var catSelected = categoriasAgregadas.ElementAt(0);

            // Act
            var result = await _controller.Get(catSelected.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<SubCategoriaIngresoDto>(objectResult.Value);

            Assert.NotNull(actualValue);
            Assert.Equal(catSelected, actualValue);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var _ = ArrangeAddStup();

            // Act
            var result = await _controller.Add(_invalidSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            //var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            //Assert.Equal(3, errors.Count);
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
            var errors = Assert.IsAssignableFrom<Dictionary<string, string>>(badRequestResult.Value);
            Assert.True(errors.ContainsKey("Descripcion"));

        }


        [Fact]
        public async Task Add_ReturnsOk_WhenSubCategoriaIngresoDtoIsValid()
        {
            // Arrange
            var _ = ArrangeAddStup();

            // Act
            var result = await _controller.Add(_createSubCat);

            // Assert
            var okObjectResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualSubCategoria = Assert.IsType<SubCategoriaIngresoDto>(okObjectResult.Value);

            Assert.Equal(_createSubCat.Descripcion, actualSubCategoria.Descripcion);
            Assert.Equal(_createSubCat.UsuarioId, actualSubCategoria.UsuarioId);
            Assert.Equal(_createSubCat.CategoriaIngresoId, actualSubCategoria.CategoriaIngresoId);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenSubCategoriaIngresoDtoIsRepeated()
        {
            // Arrange
            var _ = await ArrangeAddStup();

            // Act
            var result = await _controller.Add(_repeatedSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<Dictionary<string, string>>(badRequestResult.Value);
            Assert.True(errors.ContainsKey("Descripcion"));
            Assert.Single(errors);
        }

        [Fact]
        public async Task Put_ReturnsOk_WhenSubCategoriaIngresoDtoIsValid()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var selectedCat = categoriasAgregadas.ElementAt(1);
            var updateCat = new InsertUpdateSubCategoriaIngresoDto
            {
                Descripcion = "Updated",
                UsuarioId = selectedCat.UsuarioId,
                CategoriaIngresoId = selectedCat.CategoriaIngresoId,
            };

            // Act
            var result = await _controller.Put(selectedCat.Id, updateCat);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualSubCategoria = Assert.IsType<SubCategoriaIngresoDto>(okObjectResult.Value);
            Assert.NotNull(actualSubCategoria);
            Assert.Equal(selectedCat.Id, actualSubCategoria.Id);
            Assert.Equal(updateCat.Descripcion, actualSubCategoria.Descripcion);
            Assert.Equal(updateCat.UsuarioId, actualSubCategoria.UsuarioId);
            Assert.Equal(updateCat.CategoriaIngresoId, actualSubCategoria.CategoriaIngresoId);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenSubCategoriaIngresoDtoIsInvalid()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var selectedCat = categoriasAgregadas.ElementAt(0);

            // Act
            var result = await _controller.Put(selectedCat.Id, _invalidSubCat);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            //var errors = Assert.IsAssignableFrom<List<ValidationFailure>>(badRequestResult.Value);
            //Assert.Equal(3, errors.Count);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotExist()
        {
            // Arrange
            var _ = await ArrangeAddStup();
            var insertDto = new InsertUpdateSubCategoriaIngresoDto { Descripcion = "Update", CategoriaIngresoId = 1, UsuarioId = 1 };

            // Act
            var result = await _controller.Put(100, insertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenSubCategoriaIngresoDtoRepeated()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var selectedCat = categoriasAgregadas.ElementAt(1);
            var updateCat = new InsertUpdateSubCategoriaIngresoDto
            {
                Descripcion = selectedCat.Descripcion,
                UsuarioId = selectedCat.UsuarioId,
                CategoriaIngresoId = selectedCat.CategoriaIngresoId,
            };

            // Act
            var result = await _controller.Put(selectedCat.Id, updateCat);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var errors = Assert.IsAssignableFrom<Dictionary<string, string>>(badRequestResult.Value);
            Assert.True(errors.ContainsKey("Descripcion"));
            Assert.Single(errors);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSubCategoriaIngresoDtoDeleted()
        {
            // Arrange
            var categoriasAgregadas = await ArrangeAddStup();
            var selectedCat = categoriasAgregadas.ElementAt(1);

            // Act
            var result = await _controller.Delete(selectedCat.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(selectedCat, okResult.Value);
            Assert.Null(_context.GastosSubcategoriaingresos.FirstOrDefault(s => s.Id == selectedCat.Id
                                                                                && s.Baja == false));
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenSubCategoriaIngresoDtoNotExist()
        {
            var _ = await ArrangeAddStup();
            var result = await _controller.Delete(100);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }

}
