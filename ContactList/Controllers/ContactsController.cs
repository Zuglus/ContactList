using ContactList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            var req = _context
                .Contact
                .Include("Phones")
                .ToList();
            return View(req);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context
                .Contact.
                Include("Phones")
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
            "Organization,Position,Phones")] Contact contact)
        {
            if (button == "addPhoneField")
            {
                contact.Phones.Add(new Phone());
            }
            else if (button == "delPhoneField")
            {
                contact.Phones.Remove(contact.Phones.Last());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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
                .FirstOrDefaultAsync(i => i.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View("Create", contact);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId," +
            "Surname,Name,Patronymic,Birthday,Organization,Position," +
            "Phones")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

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
