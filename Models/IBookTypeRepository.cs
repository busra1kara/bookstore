namespace WebApplication1.Models
{
	public interface IBookTypeRepository : IRepository<BookType>
	{
		public void Update(BookType bookType);
		public void Save();
	}
}
