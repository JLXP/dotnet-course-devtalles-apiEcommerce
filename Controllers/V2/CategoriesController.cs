using ApiEcommerce.Constants;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiVersion("2.0")]
    //[EnableCors(PolicyNames.AllowSpecificOrigin)]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /*

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        👉 Esto es documentación para indicar qué respuestas puede devolver este método:

        403 Forbidden: significa que el usuario no tiene permiso para acceder.

        200 OK: significa que todo salió bien.


        */
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [MapToApiVersion("2.0")]
        public IActionResult GetCategoriesOrderById()
        {

            var categories = _categoryRepository.GetCategories().OrderBy(cat => cat.Id);
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(category.Adapt<CategoryDto>());
            }
            return Ok(categoriesDto);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetCategory")]
        [ResponseCache(CacheProfileName = CacheProfiles.Default10)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategory(int id)
        {
            Console.WriteLine($"Categoria con el ID: {id} a las {DateTime.Now}");
            var category = _categoryRepository.GetCategory(id);
            Console.WriteLine($"Respuesta con el ID:{id}");
            if (category == null)
            {
                return NotFound($"La categoria con el Id {id} no existe");
            }


            var categoryDto = category.Adapt<CategoryDto>();

            return Ok(categoryDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            // si createCategoryDto es null se refiere a que no se esta enviando la informacion requerida
            if (createCategoryDto == null)
            {
                return BadRequest(ModelState);
            }

            //Verificar si ya existe la categoria
            if (_categoryRepository.CategoryExists(createCategoryDto.Name))
            {
                ModelState.AddModelError("CustomError", "La categoria ya existe");
                return BadRequest(ModelState);
            }

            var category = createCategoryDto.Adapt<Category>();

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al guardar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            //Ese fragmento es típico en controladores de ASP.NET Core Web API cuando devuelves un recurso recién creado.
            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);



        }


        [HttpPatch("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CreateCategoryDto updateCategoryDto)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoria con el Id {id} no existe");
            }

            // si createCategoryDto es null se refiere a que no se esta enviando la informacion requerida
            if (updateCategoryDto == null)
            {
                return BadRequest(ModelState);
            }

            //Verificar si ya existe la categoria
            if (_categoryRepository.CategoryExists(updateCategoryDto.Name))
            {
                ModelState.AddModelError("CustomError", "La categoria ya existe");
                return BadRequest(ModelState);
            }

            var category = updateCategoryDto.Adapt<Category>();
            category.Id = id;

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al actualizar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            //Ese fragmento es típico en controladores de ASP.NET Core Web API cuando devuelves un recurso recién creado.
            return NoContent();

        }


        [HttpDelete("{id:int}", Name = "DeletCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int id)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoria con el Id {id} no existe");
            }

            var category = _categoryRepository.GetCategory(id);


            if (category == null)
            {
                return NotFound($"La categoria con el Id {id} no existe");
            }

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al eliminar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            //Ese fragmento es típico en controladores de ASP.NET Core Web API cuando devuelves un recurso recién creado.
            return NoContent();

        }


    }
}
