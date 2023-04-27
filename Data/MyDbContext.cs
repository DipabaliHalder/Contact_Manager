using ContactList.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Data{
    public class MyDbContext: DbContext{
        public MyDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}