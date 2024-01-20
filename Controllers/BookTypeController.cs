using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    public class BookTypeController : Controller
    {
		private readonly IBookTypeRepository _bookTypeRepository;
        public BookTypeController(IBookTypeRepository context)
        {
			_bookTypeRepository = context;
        }
        public IActionResult Index()
        {
            List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();
            return View(objBookTypeList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Add(BookType bookType)
		{
            if (ModelState.IsValid)
            {
				_bookTypeRepository.Add(bookType);
				_bookTypeRepository.Save();
				TempData["basarili"] = "Kitap Türü başarıyla eklendi";
                return RedirectToAction("Index");
            }

            return View();
        }

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }

            BookType? bookTypeDb = _bookTypeRepository.Get(u => u.Id == id);
            if (bookTypeDb == null)
            {
                return NotFound();
            }
			return View(bookTypeDb);
		}

		[HttpPost]
		public IActionResult Edit(BookType bookType)
		{
			if (ModelState.IsValid)
			{
				_bookTypeRepository.Update(bookType);
				_bookTypeRepository.Save();
				TempData["basarili"] = "Kitap Türü başarıyla güncellendi";
				return RedirectToAction("Index");
			}

			return View();
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			BookType? bookTypeDb = _bookTypeRepository.Get(u => u.Id == id);
			if (bookTypeDb == null)
			{
				return NotFound();
			}

			return View(bookTypeDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			BookType? bookTypeDb = _bookTypeRepository.Get(u => u.Id == id);
			if (bookTypeDb == null)
			{
				return NotFound();
			}

			_bookTypeRepository.Delete(bookTypeDb);
			_bookTypeRepository.Save();
			TempData["basarili"] = "Kitap Türü başarıyla silindi";
			return RedirectToAction("Index");
		}

	}
}
