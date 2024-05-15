using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaIngresoController : ControllerBase
    {
        /*
        private ProyectoGastosTestContext _context;

        public CategoriaIngresoController(ProyectoGastosTestContext context)
        {
            _context = context;
        }

        // GET: api/<CategoriaIngresoController>
        [HttpGet]
        public async Task<IEnumerable<ReaderCategoriaIngresoDto>> Get()
        {
            var categoriasIngreso = await _context.GastosCategoriaigresos.Select(c => new ReaderCategoriaIngresoDto
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                FechaCreacion = c.FechaCreacion,
                Baja = c.Baja,
            }).ToListAsync();

            return categoriasIngreso;
        }

        // GET api/<CategoriaIngresoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaIngresoDto>> Get(long id)
        {
            var categoriaIngreso = await _context.GastosCategoriaigresos.FindAsync(id);

            if (categoriaIngreso == null)
            {
                return NotFound();
            }

            var categoriaIngresoDto = new CategoriaIngresoDto
            {
                Id = categoriaIngreso.Id,
                Descripcion = categoriaIngreso.Descripcion,
            };
                
            return Ok(categoriaIngresoDto);
        }

        // POST api/<CategoriaIngresoController>
        [HttpPost]
        public async Task<ActionResult<CategoriaIngresoDto>> Add(InsertUpdateCategoriaIngresoDto insertCategoriaIngresoDto)
        {
            var categoriaIngreso = new GastosCategoriaigreso()
            {
                Descripcion = insertCategoriaIngresoDto.Descripcion
            };
            categoriaIngreso.FechaCreacion = DateTime.UtcNow;


            await _context.GastosCategoriaigresos.AddAsync(categoriaIngreso);
            await _context.SaveChangesAsync();

            var categoriaDto = new CategoriaIngresoDto()
            {
                Id = categoriaIngreso.Id,
                Descripcion = categoriaIngreso.Descripcion
            };

            return CreatedAtAction(nameof(Get), new {id = categoriaIngreso.Id}, categoriaDto);
        }

        // PUT api/<CategoriaIngresoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaIngresoDto>> Put(long id, InsertUpdateCategoriaIngresoDto categoriaIngresoDto)
        {
            var categoriaIngreso = await _context.GastosCategoriaigresos.FindAsync(id);

            if (categoriaIngreso == null)
            {
                return NotFound();
            }

            categoriaIngreso.Descripcion = categoriaIngresoDto.Descripcion;
            await _context.SaveChangesAsync();


            var categoriaDto = new CategoriaIngresoDto()
            {
                Id = categoriaIngreso.Id,
                Descripcion = categoriaIngreso.Descripcion
            };
            return Ok(categoriaDto);
        }

        // DELETE api/<CategoriaIngresoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var categoriaIngreso = await _context.GastosCategoriaigresos.FindAsync(id);

            if (categoriaIngreso == null)
            {
                return NotFound();
            }

            categoriaIngreso.Baja = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
    }
        
}
