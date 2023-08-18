using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DB_Service.Hubs
{
    //[Authorize]

    public class NotificationHub : Hub
    {
        private readonly static ConnectionMapping<int> _connections = 
            new ConnectionMapping<int>();

        //public void SendNotifications(int toUserId, string message)
        //{
            
        //    //var token = Context.GetHttpContext().Request.Headers["Authorization"].ToString().Split(' ')[1];

        //    //var handler = new JwtSecurityTokenHandler();
        //    //var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

        //    //int UserId = int.Parse(parsedToken.Claims
        //    //    .Where(c => c.Type == ClaimTypes.Sid)
        //    //    .FirstOrDefault()?.ToString().Split(" ")[1]);

        //    foreach (var connectionId in _connections.GetConnections(toUserId))
        //    {
        //        Clients.Client(connectionId).SendAsync(message);
        //    }
        //}

        //public async Task Send(string message, string userName)
        //{
        //    await Clients.All.SendAsync("Receive", message, userName);
        //}

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected enstablished " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("RecieveConnectionId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public void SetUserConnection(string data)
        {
            var route = JsonConvert.DeserializeObject<dynamic>(data);
            string userIdUnparsed = route.UserId;
            int userId = int.Parse(userIdUnparsed);
            _connections.Add(userId, Context.ConnectionId);
            Console.WriteLine($"{userId} connected as {Context.ConnectionId}; {_connections.Count}");
        }

        public async Task SendMessageAsync(string message)
        {
            var route = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = route.To;
            Console.WriteLine("Message received on " + Context.ConnectionId);

            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _connections.Find(Context.ConnectionId);
            _connections.Remove(userId, Context.ConnectionId);
            Console.WriteLine($"{userId} disconnected {_connections.Count}");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
