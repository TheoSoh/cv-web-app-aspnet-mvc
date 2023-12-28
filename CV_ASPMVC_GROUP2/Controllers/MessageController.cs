using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class MessageController : Controller
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
        public IActionResult SendMessage(Message m)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(m);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return View("SendMessage"); 
        }


        public IActionResult Inbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomingMessages = _context.Messages.Where(m => m.ToUserId == userId).ToList();

            return View(incomingMessages);
        }


       // [HttpGet]
       // public IActionResult GetUnreadCount()
       // {
            // Hämta antalet olästa meddelanden för den inloggade användaren
           // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           // var unreadCount = _context.Messages.Count(m => m.ToUserId == userId && !m.Read);

           // return Json(new { unreadCount });
       // }
    }
}
