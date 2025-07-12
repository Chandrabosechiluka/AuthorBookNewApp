namespace AuthorBookNewApp.Exceptions.Book
{
    public class BookNotFoundException:Exception,INotFoundException
    {
        public BookNotFoundException(string message) : base(message) { }
    }
}
