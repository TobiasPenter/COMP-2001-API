using Microsoft.EntityFrameworkCore;
using COMP2001_CW2.Models.Tables;
using COMP2001_CW2.Models.Stored_Procedures;
using Microsoft.Data.SqlClient;
using COMP2001_CW2.Models.Stored_Procedures.GET;
using COMP2001_CW2.Models.Stored_Procedures.POST;
using COMP2001_CW2.Models.Stored_Procedures.DELETE;
using COMP2001_CW2.Models.Stored_Procedures.PUT;

namespace COMP2001_CW2.Context
{
    public class ContextForDB:DbContext
    {
        private IConfiguration _configuration;
        public ContextForDB(IConfiguration configration) { 
            _configuration = configration;
        }

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

        //PUT
        public DbSet<UpdateAboutMe> updateAboutMe { get; set; }
        public DbSet<UpdateActivitySpeedPacePreference> updateActivitySpeedPacePreferences { get; set; }
        public DbSet<UpdateBirthday> updateBirthday { get; set; }
        public DbSet<UpdateEmail> updateEmail { get; set; }
        public DbSet<UpdateFirstName> updateFirstName { get; set; }
        public DbSet<UpdateHeight> updateHeights { get; set; }
        public DbSet<UpdateLastName> updateLastNames { get; set; }
        public DbSet<UpdateMemberLocation> updateMemberLocation { get; set; }
        public DbSet<UpdateProfilePicture> updateProfilePictures { get; set; }
        public DbSet<UpdateUnits> updateUnits { get; set; }
        public DbSet<UpdateUsername> updateUsername { get; set; }
        public DbSet<UpdateUserPassword> updateUserPassword { get; set; }
        public DbSet<UpdateWeight> updateWeight { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyConnection"));
            }
        }

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

            //PUT
            modelBuilder.Entity<UpdateAboutMe>()
                .HasNoKey();

            modelBuilder.Entity<UpdateActivitySpeedPacePreference>()
                .HasNoKey();

            modelBuilder.Entity<UpdateBirthday>()
                .HasNoKey();

            modelBuilder.Entity<UpdateEmail>()
                .HasNoKey();

            modelBuilder.Entity<UpdateFirstName>()
                .HasNoKey();

            modelBuilder.Entity<UpdateHeight>()
                .HasNoKey();

            modelBuilder.Entity<UpdateLastName>()
                .HasNoKey();

            modelBuilder.Entity<UpdateMemberLocation>()
                .HasNoKey();

            modelBuilder.Entity<UpdateProfilePicture>()
                .HasNoKey();

            modelBuilder.Entity<UpdateUnits>()
                .HasNoKey();

            modelBuilder.Entity<UpdateUsername>()
                .HasNoKey();

            modelBuilder.Entity<UpdateUserPassword>()
                .HasNoKey();

            modelBuilder.Entity<UpdateWeight>()
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

        //PUT
        public async Task UpdateAboutMeAsync(string email, string newAboutMe)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateAboutMe @Email, @NewAboutMe", new SqlParameter("@Email", email), new SqlParameter("@NewAboutMe", newAboutMe));
        }

        public async Task UpdateActivitySpeedPacePreferenceAsync(string email, bool newActivitySpeedPacePreference)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateActivitySpeedPacePreference @Email, @NewActivitySpeedPacePreference", new SqlParameter("@Email", email), new SqlParameter("@NewActivitySpeedPacePreference", newActivitySpeedPacePreference));
        }

        public async Task UpdateBirthdayAsync(string email, string newBirthday)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateBirthday @Email, @NewBirthday", new SqlParameter("@Email", email), new SqlParameter("@NewBirthday", newBirthday));
        }

        public async Task UpdateEmailAsync(string email, string newEmail)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateEmail @Email, @NewEmail", new SqlParameter("@Email", email), new SqlParameter("@NewEmail", newEmail));
        }

        public async Task UpdateFirstNameAsync(string email, string newFirstName)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateFirstName @Email, @NewFirstName", new SqlParameter("@Email", email), new SqlParameter("@NewFirstName", newFirstName));
        }

        public async Task UpdateHeightAsync(string email, double newHeight)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateHeight @Email, @NewUserHeight", new SqlParameter("@Email", email), new SqlParameter("@NewUserHeight", newHeight));
        }

        public async Task UpdateLastNameAsync(string email, string newLastName)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateLastName @Email, @NewLastName", new SqlParameter("@Email", email), new SqlParameter("@NewLastName", newLastName));
        }

        public async Task UpdateMemberLocationAsync(string email, string newMemberLocation)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateMemberLocation @Email, @NewMemberLocation", new SqlParameter("@Email", email), new SqlParameter("@NewMemberLocation", newMemberLocation));
        }

        public async Task UpdateProfilePictureAsync(string email, byte[] newProfilePicture)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateProfilePicture @Email, @NewProfilePicture", new SqlParameter("@Email", email), new SqlParameter("@NewProfilePicture", newProfilePicture));
        }

        public async Task UpdateUnitsAsync(string email, bool newUnits)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateUnits @Email, @NewUnits", new SqlParameter("@Email", email), new SqlParameter("@NewUnits", newUnits));
        }

        public async Task UpdateUsernameAsync(string email, string newUsername)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateUsername @Email, @NewUsername", new SqlParameter("@Email", email), new SqlParameter("@NewUsername", newUsername));
        }

        public async Task UpdateUserPasswordAsync(string email, string newUserPassword)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateUserPassword @Email, @NewUserPassword", new SqlParameter("@Email", email), new SqlParameter("@NewUserPassword", newUserPassword));
        }

        public async Task UpdateWeightAsync(string email, double newWeight)
        {
            await Database.ExecuteSqlRawAsync("EXEC CW2.UpdateWeight @Email, @NewUserWeight", new SqlParameter("@Email", email), new SqlParameter("@NewUserWeight", newWeight));
        }
    }
}
