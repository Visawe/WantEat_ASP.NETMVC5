using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eat.Models
{
    //// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<IngredientType> IngredientTypes { get; set; }
        public virtual DbSet<NationalCuisine> NationalCuisines { get; set; }
        public virtual DbSet<OrderDishesDetail> OrderDishesDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersDish> OrdersDishes { get; set; }
        public virtual DbSet<OrderTable> OrderTables { get; set; }
        public virtual DbSet<Parking> Parkings { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<RestaurantPhoto> RestaurantPhotoes { get; set; }
        public virtual DbSet<RestaurantReview> RestaurantReviews { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TargetAudience> TargetAudiences { get; set; }
        public virtual DbSet<TypeFood> TypeFoods { get; set; }
        public virtual DbSet<MenuSection> MenuSections { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
        public virtual DbSet<GoogleRestaurantDetails> GoogleRestaurantsDetails { get; set; }
        public virtual DbSet<GoogleReview> GoogleReviews { get; set; }
        public virtual DbSet<GeoIPLocation> GeoIPLocations { get; set; }
        public virtual DbSet<GeoName> GeoNames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Addresses)
                .Map(m => m.ToTable("UserAddresses").MapLeftKey("AddressId").MapRightKey("UserId"));

            modelBuilder.Entity<Attribute>()
                .HasMany(e => e.Restaurants)
                .WithMany(e => e.Attributes)
                .Map(m => m.ToTable("RestaurantAttribute").MapLeftKey("AttributeId").MapRightKey("RestaurantId"));

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Dishes)
                .Map(m => m.ToTable("DishRecommendation").MapLeftKey("DishId").MapRightKey("UserId"));

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.IngredientTypes)
                .WithMany(e => e.Dishes)
                .Map(m => m.ToTable("Ingredients").MapLeftKey("DisheId").MapRightKey("IngredientTypeId"));

            modelBuilder.Entity<NationalCuisine>()
                .HasMany(e => e.Restaurants)
                .WithMany(e => e.NationalCuisines)
                .Map(m => m.ToTable("RestaurantNationalCuisines").MapLeftKey("NationalCuisineId").MapRightKey("RestaurantId"));

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrdersDishes)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OrdersDish>()
                .HasMany(e => e.OrderDishesDetails)
                .WithRequired(e => e.OrdersDish)
                .HasForeignKey(e => e.OrderDishesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.OrderTables)
                .WithOptional(e => e.PaymentType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Restaurants)
                .WithMany(e => e.PaymentTypes)
                .Map(m => m.ToTable("RestaurantPaymentTypes").MapLeftKey("PaymentTypeId").MapRightKey("RestaurantId"));

            modelBuilder.Entity<RestaurantReview>()
                .HasMany(e => e.RestaurantReviews1)
                .WithOptional(e => e.RestaurantReview1)
                .HasForeignKey(e => e.ParentReviewId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(e => e.TargetAudiences)
                .WithMany(e => e.Restaurants)
                .Map(m => m.ToTable("RestaurantTargetAudience").MapLeftKey("RestaurantId").MapRightKey("TargetAudienceId"));

            modelBuilder.Entity<Restaurant>()
                .HasMany(e => e.TypeFoods)
                .WithMany(e => e.Restaurants)
                .Map(m => m.ToTable("RestaurantTypeFood").MapLeftKey("RestaurantId").MapRightKey("TypeFoodId"));

            modelBuilder.Entity<Table>()
                .HasMany(e => e.OrderTables)
                .WithRequired(e => e.Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.LastChangedUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Orders1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.ClientUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Orders2)
                .WithOptional(e => e.User2)
                .HasForeignKey(e => e.ManagerUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.OrdersDishes)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.LastChangedUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.OrderTables)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.LastChangedUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Restaurant>()
                .HasOptional(e => e.GoogleRestaurantDetails)
                .WithRequired(e => e.Restaurant);
                

            modelBuilder.Entity<WorkSchedule>()
                .Property(e => e.TimeFrom)
                .HasPrecision(0);

            modelBuilder.Entity<WorkSchedule>()
                .Property(e => e.TimeTo)
                .HasPrecision(0);

            modelBuilder.Entity<Order>()
            .Property(f => f.TimeChanged)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<Order>()
            .Property(f => f.TimeCreated)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrderTable>()
            .Property(f => f.OrderTimeFrom)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrderTable>()
            .Property(f => f.OrderTimeTo)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrderTable>()
            .Property(f => f.TimeChanged)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrderTable>()
            .Property(f => f.TimeCreated)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrdersDish>()
            .Property(f => f.TimeChanged)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrdersDish>()
            .Property(f => f.TimeCreated)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            modelBuilder.Entity<OrdersDish>()
            .Property(f => f.OrderTime)
            .HasColumnType("datetime2")
            .HasPrecision(0);

            base.OnModelCreating(modelBuilder);
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
}