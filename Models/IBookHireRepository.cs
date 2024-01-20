namespace WebApplication1.Models
{
	public interface IBookHireRepository : IRepository<BookHire>
	{
		public void Update(BookHire bookHire);
		public void Save();
	}
}
