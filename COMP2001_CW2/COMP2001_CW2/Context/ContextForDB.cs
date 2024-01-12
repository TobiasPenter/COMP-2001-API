using Microsoft.EntityFrameworkCore;
using COMP2001_CW2.Models.Tables;
using COMP2001_CW2.Models.Stored_Procedures;
using Microsoft.Data.SqlClient;
using COMP2001_CW2.Models.Stored_Procedures.GET;
using COMP2001_CW2.Models.Stored_Procedures.POST;
using COMP2001_CW2.Models.Stored_Procedures.DELETE;

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
        public DbSet<NewActivityName> newActivityName { get; set; }
        public DbSet<NewMeasurements> newMeasurements { get; set; }
        public DbSet<NewUser> newUser { get; set; }

        //DELETE
        public DbSet<DeleteAccount> deleteAccounts { get; set; }
        public DbSet<DeleteActivityFromAccount> deleteActivityFromAccounts { get; set; }
        public DbSet<DeleteActivityFromSystem> deleteActivityFromSystem { get; set; }


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

            modelBuilder.Entity<NewActivityName>()
                .HasNoKey();

            modelBuilder.Entity<NewMeasurements>()
                .HasNoKey();

            modelBuilder.Entity<NewUser>()
                .HasNoKey();

            //DELETE
            modelBuilder.Entity<DeleteAccount>()
                .HasNoKey();

            modelBuilder.Entity<DeleteActivityFromAccount>()
                .HasNoKey();

            modelBuilder.Entity<DeleteActivityFromSystem>()
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
        public async Task NewActivityLinkAsync(string email, int activityId)
        {
            await newActivityLink.FromSqlRaw("EXEC CW2.NewActivityLink @Email, @ActivityID", new SqlParameter("@Email", email), new SqlParameter("@ActivityID", activityId)).ToListAsync();
        }

        public async Task NewActivityNameAsync(string activityName)
        {
            await newActivityName.FromSqlRaw("EXEC CW2.NewActivityName @ActivityName", new SqlParameter("@ActivityName", activityName)).ToListAsync();
        }

        public async Task NewMeasurementsAsync(string email, bool units, double weight, double height)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.NewMeasurements @Email, @Units, @UserHeight, @UserWeight", 
                new SqlParameter("@Email", email),
                new SqlParameter("@Units", units),
                new SqlParameter("@UserHeight", height),
                new SqlParameter("@UserWeight", weight));
        }

        public async Task NewUserAsync(string email, string firstName, string lastName, string username, string password, byte[] profilePicture, string aboutMe, string memberLocation, bool activitySpeedPacePreference, string birthday)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.NewUser @Email, @FirstName, @LastName, @Username, @UserPassword, @ProfilePicture, @AboutMe, @MemberLocation, @ActivitySpeedPacePreference, @Birthday",
                new SqlParameter("@Email", email),
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@Username", username),
                new SqlParameter("@UserPassword", password),
                new SqlParameter("@ProfilePicture", profilePicture),
                new SqlParameter("@AboutMe", aboutMe),
                new SqlParameter("@MemberLocation", memberLocation),
                new SqlParameter("@ActivitySpeedPacePreference", activitySpeedPacePreference),
                new SqlParameter("@Birthday", birthday));
        }

        //DELETE
        public async Task DeleteAccountAsync(string email)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.DeleteAccount @Email", new SqlParameter("@Email", email));
        }

        public async Task DeleteActivityFromAccountAsync(string email, int activityId)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.DeleteActivityFromAccount @Email, @ActivityID", new SqlParameter("@Email", email), new SqlParameter("@ActivityID", activityId));
        }

        public async Task DeleteActivityFromSystemAsync(int activityId)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.DeleteActivityFromSystem @ActivityID", new SqlParameter("@ActivityID", activityId));
        }
    }
}
