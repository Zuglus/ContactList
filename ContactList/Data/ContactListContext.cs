using Microsoft.EntityFrameworkCore;

namespace ContactList.Data
{
    public class ContactListContext : DbContext
    {

        public ContactListContext (DbContextOptions<ContactListContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Phone> Phone { get; set; }
        public DbSet<Models.Email> Email { get; set; }
        public DbSet<Models.Skype> Skype { get; set; }
        public DbSet<Models.Other> Other { get; set; }
        public DbSet<Models.Contact> Contact { get; set; }
    }
}
