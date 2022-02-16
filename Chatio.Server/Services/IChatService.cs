using Chatio.DataAccess.Models;

namespace Chatio.Services
{
    public interface IChatService
    {
        public Task Add(ChatMessage message);
        public List<ChatMessage> List(string room);
    }
}
