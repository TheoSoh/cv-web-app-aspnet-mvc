using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class MessageController : BaseController
    {

        private readonly TestDbContext _context;
        private readonly UserManager<User> _userManager;


        public MessageController(TestDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersSelectList = new SelectList(users, "Id", "UserName");
            ViewData["ToUserId"] = usersSelectList;

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var message = new Message
            {
                SentTime = DateTime.Now,
                Read = false,
                FromUserId = loggedInUserId
            };

            return View("SendMessage", message);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message, string selectedUsername)
        {
            if (ModelState.IsValid)
            {
                var toUserId = selectedUsername;

                if (toUserId != null)
                {
                    var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    message.FromUserId = loggedInUserId;
                    message.ToUserId = toUserId;
                    message.Read = false;

                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }

            var users = await _userManager.Users.ToListAsync();
            var usersSelectList = new SelectList(users, "UserName", "UserName");
            ViewData["ToUserId"] = usersSelectList;

            return View("SendMessage", message);
        }

        public async Task<string?> GetUserIdByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user?.Id;
        }




        public IActionResult Inbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomingUnreadMessages = _context.Messages
                   .Where(m => m.ToUserId == userId && (m.Read == null || m.Read == false)).ToList();
                   
            return View("Inbox");
        }


        public IActionResult MarkAsRead(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                message.Read = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Inbox");
        }
    }
}
