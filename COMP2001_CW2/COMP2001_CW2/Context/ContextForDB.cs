using Microsoft.EntityFrameworkCore;
using COMP2001_CW2.Models.Tables;
using COMP2001_CW2.Models.Stored_Procedures;
using Microsoft.Data.SqlClient;
using COMP2001_CW2.Models.Stored_Procedures.GET;
using COMP2001_CW2.Models.Stored_Procedures.POST;

namespace COMP2001_CW2.Context
{
    public class ContextForDB:DbContext
    {
        //Call my models for tables
        public DbSet <Account_Details> Account_Details { get; set; }
        public DbSet <Activity_Link_Table> Activity_Link_Table { get; set; }
        public DbSet <Activity_Name_Table> Activity_Name_Table {  get; set; }
        public DbSet <Measurements> Measurements { get; set; }

        //Calling models for Stored Procedure
        //GET
        public DbSet<ReadAccountDetailsResult> readAccountDetailsResult { get; set; }
        public DbSet<AllAccountDetailsAdmin> allAccountDetailsAdmin { get; set; }
        public DbSet<ReadAccountDetailsAdmin> readAccountDetailsAdmin {  get; set; }
        public DbSet<ReadAllActivities> readAllActivities { get; set; }
        public DbSet<ReadMeasurements> readMeasurements { get; set; }
        public DbSet<ReadAccountActivities> ReadAccountActivities { get; set; }

        //POST
        public DbSet<NewActivityLink> newActivityLink { get; set; }


        public ContextForDB(DbContextOptions<ContextForDB>options):base(options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account_Details>()
                .HasKey(Account_Details => Account_Details.Email);

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
            //GET
            modelBuilder.Entity<ReadAccountDetailsResult>()
                .HasNoKey();

            modelBuilder.Entity<AllAccountDetailsAdmin>()
                .HasNoKey();

            modelBuilder.Entity<ReadAccountDetailsAdmin>()
                .HasNoKey();

            modelBuilder.Entity<ReadAllActivities>()
                .HasNoKey();

            modelBuilder.Entity<ReadMeasurements>()
                .HasNoKey();

            modelBuilder.Entity<ReadAccountActivities>()
                .HasNoKey();

            //POST
            modelBuilder.Entity<NewActivityLink>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }

        //Code for get procedures
        //GET
        public async Task<IEnumerable<ReadAccountDetailsResult>> ReadAccountDetailsAsync(string email)
        {
            return await readAccountDetailsResult.FromSqlRaw("EXEC CW2.ReadAccountDetails @Email", new SqlParameter("@Email", email)).ToListAsync();
        }

        public async Task<IEnumerable<AllAccountDetailsAdmin>> AllAccountDetailsAsync()
        {
            return await allAccountDetailsAdmin.FromSqlRaw("EXEC CW2.AllAccountDetailsAdmin").ToListAsync();
        }

        public async Task<IEnumerable<ReadAccountDetailsAdmin>> ReadAccountDetailsAdminAsync(string email)
        {
            return await readAccountDetailsAdmin.FromSqlRaw("EXEC CW2.ReadAccountDetailsAdmin @Email", new SqlParameter("@Email", email)).ToListAsync();
        }

        public async Task<IEnumerable<ReadAllActivities>> ReadAllActivitiesAsync()
        {
            return await readAllActivities.FromSqlRaw("EXEC CW2.ReadAllActivities").ToListAsync();
        }

        public async Task<IEnumerable<ReadMeasurements>> ReadMeasurementsAsync(string email)
        {
            return await readMeasurements.FromSqlRaw("EXEC CW2.ReadMeasurements @Email", new SqlParameter("@Email", email)).ToListAsync();
        }

        public async Task<IEnumerable<ReadAccountActivities>> ReadAccountActivitiesAsync( string email)
        {
            return await ReadAccountActivities.FromSqlRaw("EXEC CW2.ReadAccountActivities @Email", new SqlParameter("@Email", email)).ToListAsync();
        }

        //POST
        public async void NewActivityLinkAsync(string email, int activityId)
        {
            await newActivityLink.FromSqlRaw("EXEC CW2.NewActivityLink @Email, @ActivityID",new SqlParameter("@Email", email),new SqlParameter("@ActivityID", activityId)).ToListAsync();
        }
    }
}
