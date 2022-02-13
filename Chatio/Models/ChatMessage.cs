namespace Chatio.Models
{
    public class ChatMessage
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public bool CurrentUser { get; set; }
        public DateTimeOffset DateSent { get; set; }
    }
}
