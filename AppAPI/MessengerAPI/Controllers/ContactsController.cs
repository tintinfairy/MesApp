using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;

namespace AppAPI.Controllers
{
    public class ContactsController : Controller
    {

        private readonly DataContext _context;

        public ContactsController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index page
        /// </summary>
        [Route("Contacts/Index")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all contacts via ajax
        /// </summary>
        [Route("Contacts/GetContacts")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public IActionResult AjaxGetContacts(string owner)
        {
            var model = _context.UserContacts.Where(el => (el.owner == owner)).SingleOrDefault();
            if (model == null)
            {
                UserContacts userContacts = new UserContacts(owner, new JObject());
                _context.Add(userContacts);
                _context.SaveChanges();
                model = _context.UserContacts.SingleOrDefault(el => (el.owner == owner));
            }
            return View(model);
        }

        /// <summary>
        /// Create contacts
        /// </summary>
        [Route("Contacts/Create")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Create(string owner)
        {
            UserContacts userContacts = new UserContacts(owner, new JObject());
            if (ModelState.IsValid)
            {
                _context.Add(userContacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Add contacts
        /// </summary>
        [Route("Contacts/Add")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Add(string owner, string username)
        {
            UserContacts userContacts = _context.UserContacts.SingleOrDefault(el => (el.owner == owner));

            JObject currentContacts = userContacts.contacts;
            if (currentContacts["username"] == null)
            {
                currentContacts.Add(username, username);
            }

            userContacts.contacts = currentContacts;

            _context.Attach(userContacts);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Deletes user from contacts
        /// </summary>
        [Route("Contacts/Delete")]
        [EnableCors("AllowAll")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string owner, string username)
        {
            UserContacts userContacts = await _context.UserContacts.SingleOrDefaultAsync(m => m.owner == owner);

            JObject currentContacts = userContacts.contacts;
            currentContacts.Remove(username);

            userContacts.contacts = currentContacts;


            _context.Attach(userContacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
            return _context.Message.Any(e => e.ID == id);
        }
    }
}