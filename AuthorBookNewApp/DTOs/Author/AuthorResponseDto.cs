namespace AuthorBookNewApp.DTOs.Author
{
    public class AuthorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public int TotalBooks { get; set; }
    }
}
