using WebApplication1.Utility;

namespace WebApplication1.Models
{
	public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;


		public BookTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(BookType bookType)
		{
			_applicationDbContext.Update(bookType);
		}
	}
}
