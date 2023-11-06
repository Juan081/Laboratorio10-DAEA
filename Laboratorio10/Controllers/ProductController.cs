using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Business;
using Entity;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            BProduct bProduct = new BProduct();
            List<Product> products = bProduct.Get();
            List<ProductModel> productsModel = products.Select(x => new ProductModel
            {
                Id = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
            }).ToList();

            return View(productsModel);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            BProduct bProduct = new BProduct();
            Product product = bProduct.GetById(id);
            ProductModel productModel = new ProductModel
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
            };
            return View(productModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {

            Product product1 = new Product
            {

                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Stock = model.Stock,
            };

            try
            {
                BProduct product = new BProduct();
                product.Create(product1);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            BProduct bProduct = new BProduct();
            Product product = bProduct.GetById(id);
            ProductModel productModel = new ProductModel
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
            };
            return View(productModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductModel model)
        {
            Product product = new Product
            {
                ProductId = model.Id,
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock,
                Price = model.Price
            };

            try
            {
                BProduct bProduct = new BProduct();
                bProduct.Edit(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            BProduct bProduct = new BProduct();
            Product product = bProduct.GetById(id);
            Console.WriteLine(product);
            ProductModel productModel = new ProductModel
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
            };

            return View(productModel);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductModel model)
        {

            try
            {
                BProduct bProduct = new BProduct();
                bProduct.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}