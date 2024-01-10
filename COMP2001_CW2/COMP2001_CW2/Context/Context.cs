using Microsoft.EntityFrameworkCore;
using COMP2001_CW2.Models;

namespace COMP2001_CW2.Context
{
    public class Context:DbContext
    {
        //Call my models
        public DbSet<Account_Details> Account_Details { get; set; }
        public DbSet <Archived_Accounts> Archived_Accounts { get; set; }
        public DbSet <Activity_Link_Table> Activity_Link_Table { get; set; }
        public DbSet <Activity_Name_Table> Activity_Name {  get; set; }
        public DbSet <Measurements> Measurements { get; set; }


        public Context(DbContextOptions<Context>options):base(options) {}


        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account_Details>()
                .HasKey(Account_Details => Account_Details.Email);

            modelBuilder.Entity<Archived_Accounts>()
                .HasKey(Archived_Accounts => Archived_Accounts.Email);

            modelBuilder.Entity<Account_Details>()
                .HasMany(Account_Details => Account_Details.measurementsLink)
                .WithOne(Measurements => Measurements.accountDetailsLink)
                .HasForeignKey(Measurements => Measurements.Email);

            modelBuilder.Entity<Activity_Link_Table>()
                .HasOne(Activity_Link_Table => Activity_Link_Table.accountDetailsLink)
                .WithMany(Account_Details => Account_Details.activityLinkTableLink)
                .HasForeignKey(Account_Details => Account_Details.Email);

            modelBuilder.Entity<Activity_Link_Table>()
                .HasOne(Activity_Link_Table => Activity_Link_Table.activityNameTableLink)
                .WithMany(Activity_Name_Table => Activity_Name_Table.activityLinkTableLink)
                .HasForeignKey(Activity_Name_Table => Activity_Name_Table.ActivityID);

            base.OnModelCreating(modelBuilder);
        }

    }
}
