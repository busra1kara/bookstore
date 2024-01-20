using WebApplication1.Utility;

namespace WebApplication1.Models
{
	public class BookHireRepository : Repository<BookHire>, IBookHireRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public BookHireRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(BookHire bookHire)
		{
			_applicationDbContext.Update(bookHire);
		}
	}
}
