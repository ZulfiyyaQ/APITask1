namespace APITask1.Repositories.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context ):base(context)
        {
            //context.Categories.OrderBy(c=>c.Name).
        }
    }
}
