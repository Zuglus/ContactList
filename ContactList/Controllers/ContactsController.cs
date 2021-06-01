using ContactList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(DateTime SearchDateStart,
            DateTime SearchDateEnd, string SearchString)
        {
            ViewData["SearchStr"] = SearchString;

            var res = _context
                .Contact
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

            return View(await res.AsNoTracking().ToListAsync());
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
            switch (button)
            {
                case "addPhoneField":
                    contact.Phones.Add(new Phone());
                    break;
                case "delPhoneField":
                    contact.Phones.Remove(contact.Phones.Last());
                    break;
                case "addEmailField":
                    contact.Emails.Add(new Email());
                    break;
                case "delEmailField":
                    contact.Emails.Remove(contact.Emails.Last());
                    break;
                case "addSkypeField":
                    contact.Skypes.Add(new Skype());
                    break;
                case "delSkypeField":
                    contact.Skypes.Remove(contact.Skypes.Last());
                    break;
                case "addOtherField":
                    contact.Others.Add(new Other());
                    break;
                case "delOtherField":
                    contact.Others.Remove(contact.Others.Last());
                    break;
                default:
                    if (ModelState.IsValid)
                    {
                        _context.Add(contact);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    break;
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
                .FirstOrDefaultAsync(i => i.ContactId == id);

            return contact == null ? NotFound() : View(contact);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string button,
            [Bind("ContactId,Surname,Name,Patronymic,Birthday,Organization," +
            "Position,Phones,Emails,Skypes,Others")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }
            switch (button)
            {
                case "addPhoneField":
                    contact.Phones.Add(new Phone());
                    break;
                case "delPhoneField":
                    contact.Phones.Remove(contact.Phones.Last());
                    break;
                case "addEmailField":
                    contact.Emails.Add(new Email());
                    break;
                case "delEmailField":
                    contact.Emails.Remove(contact.Emails.Last());
                    break;
                case "addSkypeField":
                    contact.Skypes.Add(new Skype());
                    break;
                case "delSkypeField":
                    contact.Skypes.Remove(contact.Skypes.Last());
                    break;
                case "addOtherField":
                    contact.Others.Add(new Other());
                    break;
                case "delOtherField":
                    contact.Others.Remove(contact.Others.Last());
                    break;
                default:
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(contact);
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
                    break;
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
