namespace WebApplication1.Models
{
	public interface IBookRepository : IRepository<Book>
	{
		public void Update(Book book);
		public void Save();
	}
}
