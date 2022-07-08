using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryRepository = new CategoryRepository();

        [HttpGet("getCategories")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult GetCategories(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var categories = _categoryRepository.GetCategories(pageIndex, pageSize, keyword);

            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)).ToList(),
                Totalrecords = categories.Totalrecords
            };

            return Ok(listResponse);
        }

        [HttpGet("getCategory/{id}")]
        [ProducesResponseType(typeof(CategoryModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);

            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);
        }

        [HttpPost("addCategory")]
        [ProducesResponseType(typeof(CategoryModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Category category = new Category()
            {
                Id=model.Id,
                Name=model.Name
            };
            var entry= _categoryRepository.AddCategory(category);

            CategoryModel categoryModel = new CategoryModel(entry);

            return Ok(categoryModel);
        }

        [HttpPut("updateCategory")]
        [ProducesResponseType(typeof(CategoryModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var entry = _categoryRepository.UpdateCategory(category);

            CategoryModel categoryModel = new CategoryModel(entry);

            return Ok(categoryModel);
        }

        [HttpDelete("deleteCategory/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter valid Id");
            }
            var entry = _categoryRepository.DeleteCategory(id);
            return Ok(entry);
        }

    }
}
