using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
namespace WebApplication2.Context
{
		public class organizadorContext : DbContext
		{
			public organizadorContext(DbContextOptions<organizadorContext> options) : base(options)
			{

			}

			public DbSet<Tarefa> Tarefas { get; set; }
		}
}


