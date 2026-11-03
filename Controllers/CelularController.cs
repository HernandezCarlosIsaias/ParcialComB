using Microsoft.AspNetCore.Mvc;
using Parcial.Entities;

namespace Parcial.Controllers;

[ApiController]
[Route("[controller]")]
public class CelularController : ControllerBase
{
    private static readonly List<Celular> Celulares = new()
    {
        new Celular { Id = 1, Marca = "Samsung", Modelo = "Galaxy A55", Precio = 399000 },
        new Celular { Id = 2, Marca = "Apple", Modelo = "iPhone 15", Precio = 799000 },
        new Celular { Id = 3, Marca = "Motorola", Modelo = "Edge 50", Precio = 499000 }
    };

    private readonly ILogger<CelularController> _logger;

    public CelularController(ILogger<CelularController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCelularById")]
    public IActionResult GetById(int id)
    {
        try
        {
            var celular = Celulares.FirstOrDefault(c => c.Id == id);
            if (celular == null)
            {
                return NotFound();
            }

            return Ok(celular);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el celular por ID");
            return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
        }
    }

    [HttpPost]
    public IActionResult Create(Celular celular)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(celular.Marca) ||
                string.IsNullOrWhiteSpace(celular.Modelo) ||
                celular.Precio <= 0)
            {
                return BadRequest("La marca, el modelo y un precio mayor que cero son obligatorios.");
            }

            Celulares.Add(celular);
            return Ok("Celular creado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el celular");
            return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("El ID del celular debe ser mayor que cero.");
            }

            var celular = Celulares.FirstOrDefault(c => c.Id == id);
            if (celular == null)
            {
                return NotFound();
            }

            Celulares.Remove(celular);
            return Ok("Celular eliminado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el celular");
            return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
        }
    }
}
