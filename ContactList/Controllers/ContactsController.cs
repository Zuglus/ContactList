using ContactList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class ContactsController : Controller
    {
        private readonly ContactListContext _context;

        public ContactsController(ContactListContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {

            var res = _context
                .Contact
                    .Include("Phones")
                    .Include("Emails")
                    .Include("Skypes")
                    .Include("Others");

            return View(await res.ToListAsync());
        }

        // POST: Contacts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateTime SearchDateStart,
            DateTime SearchDateEnd, string SearchString)
        {
            SearchDateEnd = SearchDateEnd > SearchDateStart
                ? SearchDateEnd : DateTime.Today;

            ViewData["SearchStr"] = SearchString;
            ViewData["SearchDateStart"] = SearchDateStart.ToShortDateString();
            ViewData["SearchDateEnd"] = SearchDateEnd.ToShortDateString();

            var res = _context
                .Contact.Where(d => d.Birthday >= SearchDateStart
                && d.Birthday <= SearchDateEnd)
                    .Include("Phones")
                    .Include("Emails")
                    .Include("Skypes")
                    .Include("Others");

            if (!String.IsNullOrEmpty(SearchString))
            {
                res = res.Where(s => s.Surname.Contains(SearchString)
                || s.Name.Contains(SearchString)
                || s.Patronymic.Contains(SearchString)
                || s.Organization.Contains(SearchString)
                || s.Position.Contains(SearchString)
                || s.Phones.Any(p => p.PhoneNumber.Contains(SearchString))
                || s.Emails.Any(e => e.EmailAdress.Contains(SearchString))
                || s.Skypes.Any(s => s.SkypeNumber.Contains(SearchString))
                || s.Others.Any(o => o.OtherField.Contains(SearchString)));
            }

            return View(await res.ToListAsync());
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context
                .Contact
                .Include("Phones")
                .Include("Emails")
                .Include("Skypes")
                .Include("Others")
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string button,
            [Bind("ContactId,Surname,Name,Patronymic,Birthday," +
            "Organization,Position,Phones,Emails,Skypes,Others")] Contact contact)
        {
            if (button.Contains("addPhoneField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Phones.Count())
                    contact.Phones.Add(new Phone());
                else
                {
                    contact.Phones.Insert(index + 1, new Phone());
                }
            }
            else if (button.Contains("delPhoneField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Phones.RemoveAt(index);
            }
            else if (button.Contains("addEmailField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Emails.Count())
                    contact.Emails.Add(new Email());
                else
                {
                    contact.Emails.Insert(index + 1, new Email());
                }
            }
            else if (button.Contains("delEmailField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Emails.RemoveAt(index);
            }
            else if (button.Contains("addSkypeField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Skypes.Count())
                    contact.Skypes.Add(new Skype());
                else
                {
                    contact.Skypes.Insert(index + 1, new Skype());
                }
            }
            else if (button.Contains("delSkypeField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Skypes.RemoveAt(index);
            }
            else if (button.Contains("addOtherField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Others.Count())
                    contact.Others.Add(new Other());
                else
                {
                    contact.Others.Insert(index + 1, new Other());
                }
            }
            else if (button.Contains("delOtherField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Others.RemoveAt(index);
            }
            else if (button == "create" && ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context
                .Contact
                .Include("Phones")
                .Include("Emails")
                .Include("Skypes")
                .Include("Others")
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ContactId == id);

            return contact == null ? NotFound() : View(contact);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<object> Edit(int id, string button,
            [Bind("ContactId,Surname,Name,Patronymic,Birthday,Organization," +
            "Position,Phones,Emails,Skypes,Others")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (button.Contains("addPhoneField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Phones.Count())
                    contact.Phones.Add(new Phone());
                else
                {
                    contact.Phones.Insert(index + 1, new Phone());
                }
            }
            else if (button.Contains("delPhoneField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Phones.RemoveAt(index);
            }
            else if (button.Contains("addEmailField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Emails.Count())
                    contact.Emails.Add(new Email());
                else
                {
                    contact.Emails.Insert(index + 1, new Email());
                }
            }
            else if (button.Contains("delEmailField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Emails.RemoveAt(index);
            }
            else if (button.Contains("addSkypeField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Skypes.Count())
                    contact.Skypes.Add(new Skype());
                else
                {
                    contact.Skypes.Insert(index + 1, new Skype());
                }
            }
            else if (button.Contains("delSkypeField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Skypes.RemoveAt(index);
            }
            if (button.Contains("addOtherField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                if (index == contact.Others.Count())
                    contact.Others.Add(new Other());
                else
                {
                    contact.Others.Insert(index + 1, new Other());
                }
            }
            else if (button.Contains("delOtherField"))
            {
                int index = int.Parse(button.Remove(0, 13));
                contact.Others.RemoveAt(index);
            }
            else if (button == "save" && ModelState.IsValid)
            {
                try
                {
                    var oldContact = await _context
                .Contact
                .Include("Phones")
                .Include("Emails")
                .Include("Skypes")
                .Include("Others")
                .FirstOrDefaultAsync(i => i.ContactId == id);

                    oldContact.ContactId = contact.ContactId;
                    oldContact.Surname = contact.Surname;
                    oldContact.Name = contact.Name;
                    oldContact.Patronymic = contact.Patronymic;
                    oldContact.Birthday = contact.Birthday;
                    oldContact.Organization = contact.Organization;
                    oldContact.Position = contact.Position;
                    oldContact.Phones = contact.Phones;
                    oldContact.Emails = contact.Emails;
                    oldContact.Skypes = contact.Skypes;
                    oldContact.Others = contact.Others;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }


        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context
                .Contact
                .FirstOrDefaultAsync(m => m.ContactId == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context
                .Contact
                .FindAsync(id);

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
