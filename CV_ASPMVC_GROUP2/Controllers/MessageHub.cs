using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CV_ASPMVC_GROUP2.Controllers
{

    public class UnreadMessagesHub : Hub
    {
        private readonly TestDbContext _context;

        public UnreadMessagesHub(TestDbContext context)
        {
            _context = context;
        }


        public async Task<int> GetUnreadMessageCountForUser(string userId)
        {
            var unreadCount = await _context.Messages
                .Where(m => m.ToUserId == userId && m.Read == false)
                .CountAsync();

            return unreadCount;
        }

        public async Task GetUnreadMessageCount(string userId)
        {
            var unreadCount = await GetUnreadMessageCountForUser(userId);
            await Clients.User(userId).SendAsync("ReceiveUnreadMessageCount", unreadCount);
        }
    }
}