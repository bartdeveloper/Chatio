namespace Chatio.Models
{
    public class ChatMessageDTO
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public bool CurrentUser { get; set; }
        public DateTimeOffset DateSent { get; set; }
    }
}
