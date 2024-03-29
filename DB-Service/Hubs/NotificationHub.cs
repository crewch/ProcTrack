﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Sockets;
using System.Security.Claims;

namespace DB_Service.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly static ConnectionMapping<int> _connections = 
            new ConnectionMapping<int>();

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected enstablished " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("RecieveConnectionId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public static IEnumerable<string> GetConnections(int connectionId)
        {
            return _connections.GetConnections(connectionId);
        }

        public void SetUserConnection(string data)
        {
            var route = JsonConvert.DeserializeObject<dynamic>(data);
            string userIdUnparsed = route.UserId;
            int userId = int.Parse(userIdUnparsed);
            _connections.Add(userId, Context.ConnectionId);
            Console.WriteLine($"{userId} connected as {Context.ConnectionId}; {_connections.Count}");
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
