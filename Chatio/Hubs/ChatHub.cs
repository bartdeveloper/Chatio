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
            if (_connections.TryGetValue(Context.ConnectionId, out ChatUser userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                await Clients.Group(userConnection.Room).SendAsync(METHOD_RECEIVE_MESSAGE, _botUser, $"{userConnection.User} has left");
                await SendUsersConnectedToRoom(userConnection.Room);
            }
        }

        public async Task JoinToRoom(ChatUser userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).SendAsync(METHOD_RECEIVE_MESSAGE, _botUser, $"{userConnection.User} has joined to the room: {userConnection.Room}");

            await SendUsersConnectedToRoom(userConnection.Room);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out ChatUser userConnection))
            {
                await Clients.Group(userConnection.Room).SendAsync(METHOD_RECEIVE_MESSAGE, userConnection.User, message);
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
