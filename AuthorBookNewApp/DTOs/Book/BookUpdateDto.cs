namespace AuthorBookNewApp.DTOs.Book
{
    public class BookUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int AuthorId { get; set; }
    }
}
