using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
		private readonly IBookRepository _bookRepository;
		private readonly IBookTypeRepository _bookTypeRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
			_bookRepository = bookRepository;
			_bookTypeRepository = bookTypeRepository;
			_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Book> objBookList = _bookRepository.GetAll().ToList();
			List<Book> objBookList = _bookRepository.GetAll(includeProps:"BookType").ToList();
			return View(objBookList);
        }

        public IActionResult AddOrEdit(int? id)
        {
            IEnumerable<SelectListItem> bookTypeList = _bookTypeRepository.GetAll().
				Select(k => new SelectListItem
				{
					Text = k.Name,
					Value = k.Id.ToString(),
				});
            ViewBag.bookTypeList = bookTypeList;

			if (id == null || id == 0)
			{
				return View();
			}
			else 
			{
				Book? bookDb = _bookRepository.Get(u => u.Id == id);
				if (bookDb == null)
				{
					return NotFound();
				}
				return View(bookDb);
			}
        }

        [HttpPost]
		public IActionResult AddOrEdit(Book book, IFormFile file)
		{
            if (ModelState.IsValid)
            {
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				string bookPath = Path.Combine(wwwRootPath, @"img");
				
				if(file != null)
				{
					using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					book.ImageUrl = @"\img\" + file.FileName;
				}	

				if(book.Id == 0)
				{
					_bookRepository.Add(book);
					TempData["basarili"] = "Kitap başarıyla eklendi.";
				}
				else
				{
					_bookRepository.Update(book);
					TempData["basarili"] = "Kitap başarıyla güncellendi.";
				}				
				_bookRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

		//public IActionResult Edit(int? id)
		//{
  //          if(id == null || id == 0)
  //          {
  //              return NotFound();
  //          }

  //          Book? bookDb = _bookRepository.Get(u => u.Id == id);
  //          if (bookDb == null)
  //          {
  //              return NotFound();
  //          }
		//	return View(bookDb);
		//}

		//[HttpPost]
		//public IActionResult Edit(Book book)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		_bookRepository.Update(book);
		//		_bookRepository.Save();
		//		TempData["basarili"] = "Kitap başarıyla güncellendi";
		//		return RedirectToAction("Index");
		//	}

		//	return View();
		//}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Book? bookDb = _bookRepository.Get(u => u.Id == id);
			if (bookDb == null)
			{
				return NotFound();
			}

			return View(bookDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Book? bookDb = _bookRepository.Get(u => u.Id == id);
			if (bookDb == null)
			{
				return NotFound();
			}

			_bookRepository.Delete(bookDb);
			_bookRepository.Save();
			TempData["basarili"] = "Kitap başarıyla silindi";
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Deneme(string BookName, string BookDefine, string Author, int Price)
		{
			return Json(new { foo = "bar", baz = "Blech" });
		}

	}
}
