using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    public class BookHireController : Controller
    {
		private readonly IBookHireRepository _bookHireRepository;
		private readonly IBookRepository _bookRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;
        public BookHireController(IBookHireRepository bookHireRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
			_bookHireRepository = bookHireRepository;
			_bookRepository = bookRepository;
			_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Book> objBookList = _bookRepository.GetAll().ToList();
			List<BookHire> objBookHireList = _bookHireRepository.GetAll(includeProps:"Book").ToList();
			return View(objBookHireList);
        }

        public IActionResult AddOrEdit(int? id)
        {
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll().
				Select(k => new SelectListItem
				{
					Text = k.BookName,
					Value = k.Id.ToString(),
				});
            ViewBag.bookList = bookList;

			if (id == null || id == 0)
			{
				return View();
			}
			else 
			{
				BookHire? bookHireDb = _bookHireRepository.Get(u => u.Id == id);
				if (bookHireDb == null)
				{
					return NotFound();
				}
				return View(bookHireDb);
			}
        }

        [HttpPost]
		public IActionResult AddOrEdit(BookHire bookHire)
		{
            if (ModelState.IsValid)
            {	
				if(bookHire.Id == 0)
				{
					_bookHireRepository.Add(bookHire);
					TempData["basarili"] = "Kiralama kaydı başarıyla oluşturuldu.";
				}
				else
				{
					_bookHireRepository.Update(bookHire);
					TempData["basarili"] = "Kiralama işlemi başarıyla güncellendi.";
				}				
				_bookHireRepository.Save();
                return RedirectToAction("Index", "BookHire");
            }

            return View();
        }

		public IActionResult Delete(int? id)
		{
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll().
				Select(k => new SelectListItem
					{
						Text = k.BookName,
						Value = k.Id.ToString(),
					});
            ViewBag.bookList = bookList;

            if (id == null || id == 0)
			{
				return NotFound();
			}

			BookHire? bookHireDb = _bookHireRepository.Get(u => u.Id == id);
			if (bookHireDb == null)
			{
				return NotFound();
			}

			return View(bookHireDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			BookHire? bookHireDb = _bookHireRepository.Get(u => u.Id == id);
			if (bookHireDb == null)
			{
				return NotFound();
			}

			_bookHireRepository.Delete(bookHireDb);
			_bookHireRepository.Save();
			TempData["basarili"] = "Kiralama başarıyla silindi";
			return RedirectToAction("Index", "BookHire");
		}

	}
}
