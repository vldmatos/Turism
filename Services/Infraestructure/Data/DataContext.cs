using Microsoft.EntityFrameworkCore;
using Shared.Entities.Contexts;
using Shared.Entities.Units;

namespace Services.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        #region Contexts

        public DbSet<Booking> Bookings { get; set; }

        #endregion Contexts

        #region Units

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ExhibitionCenter> ExhibitionCenters { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion Units

        #region Constructors

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        #endregion Constructors

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Booking>(model => model.ToContainer(nameof(Bookings)))
                .Entity<Accommodation>(model => model.ToContainer(nameof(Accommodations)))
                .Entity<Airport>(model => model.ToContainer(nameof(Airports)))
                .Entity<Contact>(model => model.ToContainer(nameof(Contacts)))
                .Entity<Employee>(model => model.ToContainer(nameof(Informations)))
                .Entity<Event>(model => model.ToContainer(nameof(Events)))
                .Entity<ExhibitionCenter>(model => model.ToContainer(nameof(ExhibitionCenters)))
                .Entity<Hotel>(model => model.ToContainer(nameof(Hotels)))
                .Entity<Information>(model => model.ToContainer(nameof(Informations)))
                .Entity<Organizer>(model => model.ToContainer(nameof(Organizers)))
                .Entity<Partner>(model => model.ToContainer(nameof(Partners)))
                .Entity<Policy>(model => model.ToContainer(nameof(Policies)))
                .Entity<Service>(model => model.ToContainer(nameof(Services)))
                .Entity<User>(model => model.ToContainer(nameof(Users)));
        }

        #endregion Methods
    }
}