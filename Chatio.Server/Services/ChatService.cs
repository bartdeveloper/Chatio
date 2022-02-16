using Chatio.DataAccess.Data;
using Chatio.DataAccess.Models;

namespace Chatio.Services
{
    public class ChatService : IChatService
    {

        private readonly ChatDbContext _context;

        public ChatService(ChatDbContext chatDbContext) { 
            _context = chatDbContext;
        }

        public async Task Add(ChatMessage message)
        {
            message.Message = message.Message;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public List<ChatMessage> List(string room)
        {
            return _context.Messages
                .AsEnumerable()
                .Where(x => x.Room == room && x.DateSent.Date == DateTime.Today)
                .TakeLast(15)
                .ToList();
        }

    }
}
