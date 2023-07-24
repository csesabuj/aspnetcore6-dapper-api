using Demo.API.DBContext;
using Demo.API.Model;
using Dapper;

namespace Demo.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext db;
        public ProductRepository(DapperContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var query = "select Id,ProductName,BarCode from Product";
            //using (var connection = db.CreateConnection())
            //{
            //    var products = await connection.QueryAsync<Product>(query);
            //    return products.ToList();
            //}


            var connection = db.CreateConnection();
            var products = await connection.QueryAsync<Product>(query);
            return products.ToList();
        }

        public async Task<Product> GetProduct(int id)
        {
            var query = "select Id,ProductName,BarCode from Product where Id =@id";
            var connection = db.CreateConnection();
            var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new { id });
            return product;
        }


        public async Task<bool> CreateProduct(Product product)
        {
            var query = "INSERT INTO Product( ProductName,BarCode) VALUES (@ProductName,@BarCode)";
            var connection = db.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new {  ProductName = product.ProductName, BarCode = product.BarCode });
            if (affected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var query = "DELETE FROM Product where Id=@Id";
            var connection = db.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            if (affected > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> UpdateProduct(Product product)
        {
            var query = "UPDATE Product set ProductName=@ProductName,BarCode=@BarCode where Id=@Id";
            var connection = db.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { Id = product.Id, ProductName = product.ProductName, BarCode = product.BarCode });
            if (affected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
