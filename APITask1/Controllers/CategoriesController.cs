
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
           
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(orderExpression:c=>c.Name,isDescenting:true,skip:(page-1)*take,take:take);
            //List<Category> categories = await _context.Categories.AsNoTracking().Skip((page - 1) * take).Take(take).ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category = await _repository.GetByIdAsync(id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categorydto)
        {
            Category category = new Category
            {
                Name=categorydto.Name
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id) ;

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            //bool result = _context.Categories.Any(t => t.Name.ToLower().Trim() == name.ToLower().Trim());
            //if (result)
            //{
            //    return StatusCode(StatusCodes.Status409Conflict);
            //}

            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
