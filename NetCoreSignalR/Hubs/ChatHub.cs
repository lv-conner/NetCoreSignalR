using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreSignalR.Hubs
{
    //三个ChatHub的等效实现。
    public class ChatHub : Hub
    {
        public async Task Broadcast(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceivedBroadcast", Context.ConnectionId, message);
            await Clients.Caller.SendAsync("BroadcastSuccess");
        }
        public async Task GetCurrentUser()
        {
            var userName = this.Context.GetHttpContext().User.Identity.Name;
            await Clients.Caller.SendAsync("ReceivedBroadcast", Context.ConnectionId, userName);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }


    public class DynamicChatHub:DynamicHub
    {
        public async Task Broadcast(string message)
        {
            List<string> s = new List<string>();
            s.Add(Context.ConnectionId);
            await Clients.AllExcept(s).ReceivedBroadcast(Context.ConnectionId, message);
            await Clients.Caller.BroadcastSuccess();
        }
    }

    public class ClientChatHub:Hub<IChatHubClient>
    {
        public async Task Broadcast(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).ReceivedBroadcast(Context.ConnectionId, message);
            await Clients.Caller.BroadcastSuccess();
        }
    }


    public interface IChatHubClient
    {
        Task ReceivedBroadcast(string user, string message);
        Task BroadcastSuccess();
    }
}
