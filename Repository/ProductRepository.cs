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
            vm.Id = 0;
            await db.Products.AddAsync(vm);
            await db.SaveChangesAsync();
        }
        public async Task UpdateProduct(int id, Product obj)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                product.ProductName = obj.ProductName;
                product.Price = obj.Price;
                product.Description = obj.Description;
                product.Status = obj.Status;
                product.Rating = obj.Rating;
                product.Status = obj.Status;
                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteProduct(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }


    }
}