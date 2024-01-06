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
            //Hämtar alla användare från databasen och skapar en SelectList som kan användas i vyn
            var users = await _userManager.Users.ToListAsync();
                var usersSelectList = new SelectList(users, "Id", "UserName");
                ViewData["ToUserId"] = usersSelectList;

            //Hämtar den inloggade användarens ID från Claims och skapar ett nytt meddelandeobjekt
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var message = new Message
                {
                    SentTime = DateTime.Now,
                    Read = false,
                    FromUserId = loggedInUserId
                };

            //Returnerar vyn för att skicka meddelanden med det nya meddelandeobjektet
            return View("SendMessage", message);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message, string selectedUsername)
        {
            if (ModelState.IsValid)
            {
                //Hämtar mottagarens ID från det valda användarnamnet i vyn
                var toUserId = selectedUsername;

                if (toUserId != null)
                {
                    //Hämtar den inloggade användarens ID och tilldelar meddelandeobjektet rätt information
                    var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    message.FromUserId = loggedInUserId;
                    message.ToUserId = toUserId;
                    message.Read = false;

                    //Lägger till det nya meddelandeobjektet i databasen och sparar ändringarna
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }

            //Om ModelState inte är giltig, återgår till vyn för att skicka meddelanden
            var users = await _userManager.Users.ToListAsync();
            var usersSelectList = new SelectList(users, "UserName", "UserName");
            ViewData["ToUserId"] = usersSelectList;

            return View("SendMessage", message);
        }

        // Hämtar användar-ID med angivet användarna och returnerar ID om användaren finns, annars returneras null.
        public async Task<string?> GetUserIdByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user?.Id;
        }


        //Hämtar användarnamn med angivet användar-Id och returnerar namnet om användaren finns, annars returneras null.
        public async Task<string?> GetUsernameById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.UserName;
        }


        public async Task<IActionResult> Inbox()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Hämtar inkommande meddelanden för den inloggade användaren
            var incomingMessages = _context.Messages
                .Where(m => m.ToUserId == userId)
                .ToList();

            var messagesWithUsername = new List<(Message message, string username)>();

            //Associerar varje meddelande med avsändarens användarnamn
            foreach (var message in incomingMessages)
            {
                var senderUsername = await GetUsernameById(message.FromUserId);
                messagesWithUsername.Add((message, senderUsername));
            }

            //Returnerar en vy med listan av meddelanden tillsammans med avsändarens användarnamn.
            return View(messagesWithUsername);
        }


        //Get-metod som hämtar antalet olästa meddelanden, alltså meddelane där Read är false
        [HttpGet]
        public IActionResult GetUnreadMessages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var unreadMessagesCount = _context.Messages.Count(m => m.ToUserId == userId && m.Read == false);

            return Json(new { count = unreadMessagesCount });
        }


        //Post-metod som markerar ett meddelande (det meddelande-id vi anger) som läst om meddelandet inte är null
        //Sätter Read till true
        [HttpPost]
        public IActionResult MarkAsRead(int messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                message.Read = true;
                _context.SaveChanges();
                return RedirectToAction("Inbox");
            }
            return NotFound(); 
        }

    }
}
