namespace Chatio.DataAccess.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Room { get; set; }
        public DateTimeOffset DateSent { get; set; }
    }
}
