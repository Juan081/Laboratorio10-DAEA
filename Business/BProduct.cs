using Entity;
using Data;
using System.Runtime.Intrinsics.Arm;

namespace Business
{
    public class BProduct
    {
        public List<Product> Get()
        {
            DProduct data = new DProduct();
            var products = data.Get();
            return products;
        }

        public Product GetById(int id)
        {
            DProduct dProduct = new DProduct();
            List<Product> products = dProduct.Get();
            Product product = new Product();

            foreach (var item in products)
            {
                if (item.ProductId == id)
                {
                    product = item;
                }
            }
            return product;
        }

        public List<Product> GetByName(string Name)
        {
            List<Product> result = new List<Product>();
            DProduct data = new DProduct();
            var products = data.Get();

            foreach (var product in products)
            {
                if (product.Name == Name)
                {
                    result.Add(product);
                }
            }
            return result;
        }

        public bool Create(Product product)
        {
            DProduct data = new DProduct();
            data.Add(product);
            return true;
        }

        public bool Delete(int productId)
        {
            DProduct data = new DProduct();
            data.Delete(productId);
            return true;
        }

        public bool Edit(Product product)
        {
            DProduct data = new DProduct();
            data.Edit(product);
            return true;
        }
    }
}