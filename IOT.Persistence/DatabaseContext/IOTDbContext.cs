using IOT.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Persistence.DatabaseContext
{
	public class IOTDbContext : DbContext
	{

		public IOTDbContext(DbContextOptions<IOTDbContext> options) : base(options)
		{

		}
		public DbSet<Detail> Detail { get; set; }
		public DbSet<DetailPicture> DetailPicture { get; set; }
		public DbSet<Machine> Machine { get; set; }	
		public DbSet<Oder> Oder { get; set; }
		public DbSet<OEE> OEE { get; set; }
		public DbSet<Project> Project { get; set; }
		public DbSet<Worker> Worker { get; set; }
		public DbSet<WorkerPicture> WorkerPicture { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(IOTDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
			// Xác định các thuộc tính làm key
			modelBuilder.Entity<Oder>().HasKey(p => p.OderId);
			modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);
			modelBuilder.Entity<Detail>().HasKey(p => p.DetailId);

			modelBuilder.Entity<DetailPicture>().HasKey(e => new { e.DetailPictureId, e.DetailId });
			modelBuilder.Entity<DetailPicture>().HasOne(e => e.Detail).WithMany(e => e.DetailPictures).HasForeignKey(e => e.DetailId);


			modelBuilder.Entity<Worker>().HasKey(e => e.WorkerId);

			modelBuilder.Entity<WorkerPicture>().HasKey(e => new { e.WorkerPictureId, e.WorkerId });
			modelBuilder.Entity<WorkerPicture>().HasOne(e => e.Worker).WithMany(e => e.WorkerPictures).HasForeignKey(e => e.WorkerId);

			modelBuilder.Entity<Machine>().HasKey(e => e.MachineId);
			modelBuilder.Entity<OEE>().HasKey(e => new { e.TimeStamp, e.MachineId });

		}
	}
	
}
