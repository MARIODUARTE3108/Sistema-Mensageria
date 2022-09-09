namespace Projeto05.Services.Api.Models
{
    public class MessageViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;


    }
}
