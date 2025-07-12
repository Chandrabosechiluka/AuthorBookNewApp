namespace AuthorBookNewApp.Exceptions.Author
{
    public class AuthorNotFoundException:Exception,INotFoundException
    {
        public AuthorNotFoundException(string message) : base(message) { }
    }
}
