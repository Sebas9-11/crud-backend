using Microsoft.EntityFrameworkCore;
using crud.Models;

namespace crud.Data
{
	public class DirectoryDb : DbContext
	{
		public DirectoryDb(DbContextOptions<DirectoryDb> options) : base(options)
		{

		}

        public DbSet<Contact> Contacts => Set<Contact>();
	}
}

