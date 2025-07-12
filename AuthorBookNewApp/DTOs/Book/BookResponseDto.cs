namespace AuthorBookNewApp.DTOs.Book
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
