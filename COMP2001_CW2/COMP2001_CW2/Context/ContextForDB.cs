using Microsoft.EntityFrameworkCore;
using COMP2001_CW2.Models.Tables;
using COMP2001_CW2.Models.Stored_Procedures;
using Microsoft.Data.SqlClient;

namespace COMP2001_CW2.Context
{
    public class ContextForDB:DbContext
    {
        //Call my models for tables
        public DbSet <Account_Details> Account_Details { get; set; }
        public DbSet <Archived_Accounts> Archived_Accounts { get; set; }
        public DbSet <Activity_Link_Table> Activity_Link_Table { get; set; }
        public DbSet <Activity_Name_Table> Activity_Name {  get; set; }
        public DbSet <Measurements> Measurements { get; set; }

        //Calling models for Stored Procedure
        public DbSet<ReadAccountDetailsResult> readAccountDetailsResult { get; set; }


        public ContextForDB(DbContextOptions<ContextForDB>options):base(options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            //Stored Procedures
            modelBuilder.Entity<ReadAccountDetailsResult>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }


        public async Task<IEnumerable<ReadAccountDetailsResult>> ReadAccountDetailsAsync(string email)
        {
            return await readAccountDetailsResult.FromSqlRaw("EXEC CW2.ReadAccountDetails @Email", new SqlParameter("@Email", email)).ToListAsync();
        }

    }
}
