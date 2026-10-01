using Microsoft.EntityFrameworkCore;

namespace Asp.netcore_with_angular.Model
{
    public class ProductRepository
    {

        public readonly AppDbcontext db;
        public ProductRepository(AppDbcontext dbcontext)
        {
            this.db = dbcontext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await db.Products.ToListAsync();
        }
        public async Task SaveProduct(Product vm)
        {
            // db.Products.Add(vm);
            await db.Products.AddAsync(vm);
        }


    }
}