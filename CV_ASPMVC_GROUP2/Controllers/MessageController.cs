using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Claims;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class MessageController : Controller
    {

        private readonly TestDbContext _context;

        public MessageController(TestDbContext context)
        {
            _context = context;
        }

        public IActionResult SendMessage(string receiverId)
        {
            var message = new Message
            {
                ToUserId = receiverId 
            };

            return View("SendMessage");
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
    }
}
