using ChatAppServer.WebAPI.Context;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppServer.WebAPI.Hubs
{
    public sealed class ChatHub(ApplicationDbContext context) :Hub
    {
        public static Dictionary<string, Guid> Users = new();
         public async Task Connect(Guid UserId)
        {
            Users.Add(Context.ConnectionId, UserId);
            User? user = await context.Users.FindAsync(UserId);
            if (user is not null)
            {
                user.Status = "online";
                context.SaveChangesAsync();
                await Clients.All.SendAsync("Users", user);

            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Guid userId;
           Users.TryGetValue(Context.ConnectionId,out  userId);

            User? user = await context.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Status = "online";
                context.SaveChangesAsync();
                await Clients.All.SendAsync("Users", user);
            }

            
        }
    }
   
}
