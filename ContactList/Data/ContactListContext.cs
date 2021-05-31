using Microsoft.EntityFrameworkCore;

namespace ContactList.Data
{
    public class ContactListContext : DbContext
    {
        public ContactListContext (DbContextOptions<ContactListContext> options)
            : base(options)
        {
        }

        public DbSet<ContactList.Models.Phone> Phone { get; set; }
        public DbSet<ContactList.Models.Contact> Contact { get; set; }
    }
}
