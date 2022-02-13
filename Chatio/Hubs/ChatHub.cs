using Chatio.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chatio.Hubs
{

    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, ChatUser> _connections;
        public static string BOT_NAME = "BOT";
        public static string METHOD_RECEIVE_MESSAGE = "ReceiveMessage";
        public static string METHOD_ROOM_MESSAGE = "UsersConnectedToRoom";

        public ChatHub(IDictionary<string, ChatUser> connections)
        {
            _botUser = BOT_NAME;
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _ = Disconnect();

            return base.OnDisconnectedAsync(exception);
        }

        public async Task Disconnect()
        {           
            if (_connections.TryGetValue(Context.ConnectionId, out ChatUser chatUser))
            {
                _connections.Remove(Context.ConnectionId);
                await Clients.Group(chatUser.Room).SendAsync(METHOD_RECEIVE_MESSAGE, _botUser, $"{chatUser.User} has left");
                await SendUsersConnectedToRoom(chatUser.Room);
            }
        }

        public string GetConnectionId() => Context.ConnectionId;

        public bool IsUsernameTaken(string room, string username)
        {
            if (_connections.Values.Any(
                c => c.Room.Equals(room, StringComparison.InvariantCultureIgnoreCase)
                    && c.User.Equals(username, StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        public async Task JoinToRoom(ChatUser chatUser)
        {

            if (!IsUsernameTaken(chatUser.Room, chatUser.User)){

                await Groups.AddToGroupAsync(Context.ConnectionId, chatUser.Room);

                _connections[Context.ConnectionId] = chatUser;

                await Clients.Group(chatUser.Room).SendAsync(METHOD_RECEIVE_MESSAGE, _botUser, $"{chatUser.User} has joined to the room: {chatUser.Room}");

                await SendUsersConnectedToRoom(chatUser.Room);

            }

        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out ChatUser chatUser))
            {
                await Clients.Group(chatUser.Room).SendAsync(METHOD_RECEIVE_MESSAGE, chatUser.User, message);
            }
        }

        public Task SendUsersConnectedToRoom(string room)
        {
            var users = _connections.Values
                .Where(c => c.Room == room)
                .Select(c => c.User);

            return Clients.Group(room).SendAsync(METHOD_ROOM_MESSAGE, users);
        }
    }

}
