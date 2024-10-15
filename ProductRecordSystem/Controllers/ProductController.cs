using Microsoft.AspNetCore.Mvc;
using ProductRecordSystem.Data;
using ProductRecordSystem.Models;

namespace ProductRecordSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        
        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET: ProductController
        public ActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            string partNumber = GenerateUniquePartNumber();
            Product newProduct = new Product()
            {
                Name = product.Name,
                Color = product.Color,
                Size = product.Size,
                PartNumber = partNumber
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private string GenerateUniquePartNumber()
        {
            Random random = new Random();
            int length = random.Next(6, 11);

            string partNumber = Guid.NewGuid().ToString("N").Substring(0, length);
            return partNumber;
        }

        public IActionResult Edit(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.Update(product);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
