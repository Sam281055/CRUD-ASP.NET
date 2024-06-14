using Microsoft.AspNetCore.Mvc;
using PandStore_Back.Model;
using PandStore_Back.Service;

namespace PandStore_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;
        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            List<Producto> prd = await _productoService.ObtenerProductos();
            return StatusCode(StatusCodes.Status200OK, prd);
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody]Producto producto)
        {
            bool respuesta = await _productoService.AgregarProducto(producto);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta});
        }
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody]Producto producto)
        {
            bool respuesta = await _productoService.Edit(producto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _productoService.EliminarProducto(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

    }
}
