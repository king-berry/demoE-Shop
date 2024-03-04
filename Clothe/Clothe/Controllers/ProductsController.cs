using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clothe.Data;
using Clothe.Models;

namespace Clothe.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ClotheContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductsController(ClotheContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> IndexUser()
        {
            var product = await _context.Product.ToListAsync();
            return View("IndexUser",product);
        }

		public async Task<IActionResult> products()
		{
			var product = await _context.Product.ToListAsync();
			return View("products", product);
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

		public async Task<IActionResult> DetailsUser(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Product
				.FirstOrDefaultAsync(m => m.ProductID == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProName,Description,ProImg,Genre,Brand,Qty,Price,ReleaseDate")] Product product, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                //Check if the file has a valid extension
                var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file extension. Allowed extensions are: " + string.Join(",", allowedExtensions));
                }

                if (formFile.Length > 0)
                {
                    //change the folder path to where you want to store the upload files
                    var uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadFolderPath);

                    var fileName = Path.GetRandomFileName() + fileExtension;
                    var filePath = Path.Combine(uploadFolderPath, fileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }


            try
            {
                if (true)
                {
                    if (filePaths.Count > 0)
                    {
                        product.ProImg = "/uploads/" + Path.GetFileName(filePaths[0]);
                    }

                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                   "Try again, and if the problem persists " +
                   "see your system administrator.");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProName,Description,ProImg,Genre,Brand,Qty,Price,ReleaseDate")] Product product, List<IFormFile> files)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Product
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (productToUpdate == null)
            {
                if (files != null && files.Count > 0)
                {
                    long size = files.Sum(f => f.Length);

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                    var filePaths = new List<string>();
                    foreach (var formFile in files)
                    {
                        //Check if the file has a valid extension
                        var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                        if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest("Invalid file extension. Allowed extensions are: " + string.Join(",", allowedExtensions));
                        }

                        if (formFile.Length > 0)
                        {
                            //change the folder path to where you want to store the upload files
                            var uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                            Directory.CreateDirectory(uploadFolderPath);

                            var fileName = Path.GetRandomFileName() + fileExtension;
                            var filePath = Path.Combine(uploadFolderPath, fileName);
                            filePaths.Add(filePath);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }
                    }
                    productToUpdate.ProImg = "/uploads/" + Path.GetFileName(filePaths[0]);
                    
                }
                if (await TryUpdateModelAsync<Product>(productToUpdate,
                "",
                p => p.ProductID, p => p.ProName, p => p.Description, p => p.ProImg, p => p.Genre, p => p.Brand, p => p.Qty, p => p.Price, p => p.ReleaseDate))
                {
                }
                try
                {
                    //_context.Update(studentToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
               

            }
            return View(productToUpdate);

        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }
    }
}
