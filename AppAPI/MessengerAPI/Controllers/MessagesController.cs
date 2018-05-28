using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace AppAPI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly DataContext _context;
        
        public MessagesController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index page
        /// </summary>
        [Route("Messages/Index")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all messages via Ajax
        /// </summary>
        [Route("Messages/GetMessages")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public IActionResult AjaxGetMessages(string senderID, string receiverID, int numPage = 0)
        {
			ViewData["Friend"] = receiverID;
            return View(_context.Message.Where(el => (el.senderID == senderID && el.receiverID == receiverID) || (el.senderID == receiverID && el.receiverID == senderID)).OrderBy(el => el.timeSent).ToList());
        }

        /// <summary>
        /// Creates a Message
        /// </summary>
        [Route("Messages/Create")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Create(string text, string senderID, string receiverID)
        {
            DateTime timeSent = DateTime.Now;
            Message message = new Message(text, timeSent, senderID, receiverID);
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Deletes a specific message
        /// </summary>
        [Route("Messages/Delete")]
        [EnableCors("AllowAll")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.SingleOrDefaultAsync(m => m.ID == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.ID == id);
        }
    }
}