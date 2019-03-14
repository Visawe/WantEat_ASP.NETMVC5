namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBnewAdmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false, maxLength: 100),
                        Region = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        Street = c.String(nullable: false, maxLength: 100),
                        House = c.String(nullable: false, maxLength: 10),
                        Apartment = c.Int(),
                        Entrance = c.Int(),
                        Floor = c.Int(),
                        Description = c.String(maxLength: 300),
                        Map = c.Geography(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        PathMainFoto = c.String(maxLength: 247),
                        Description = c.String(nullable: false, maxLength: 1000),
                        DressCode = c.String(maxLength: 500),
                        AverageCheckRestaurant = c.Double(),
                        AverageCheckOrder = c.Double(),
                        Rating = c.Double(),
                        VerificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        PathDishFoto = c.String(maxLength: 247),
                        WeightAll = c.Int(),
                        WeightPart = c.String(maxLength: 100),
                        Price = c.Double(),
                        Description = c.String(maxLength: 300),
                        Recommendation = c.Int(),
                        Availability = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.IngredientTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDishesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDishesId = c.Int(nullable: false),
                        DishId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.OrdersDishes", t => t.OrderDishesId)
                .Index(t => t.OrderDishesId)
                .Index(t => t.DishId);
            
            CreateTable(
                "dbo.OrdersDishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        DeliveryType = c.String(nullable: false, maxLength: 30),
                        NeedPrepayment = c.Double(),
                        ReceivedPrepayment = c.Double(),
                        PaymentTypeId = c.Int(),
                        Discount = c.Double(),
                        TotalCost = c.Double(),
                        TotalCostAfterDiscount = c.Double(),
                        Description = c.String(maxLength: 250),
                        AnswerManager = c.String(maxLength: 250),
                        OrderTime = c.DateTime(nullable: false),
                        NumberPeople = c.Int(),
                        LadingNumber = c.String(maxLength: 30),
                        Status = c.String(nullable: false, maxLength: 30),
                        StatusPayment = c.String(nullable: false, maxLength: 30),
                        LastChangedUserId = c.String(nullable: false, maxLength: 128),
                        TimeCreated = c.DateTime(nullable: false),
                        TimeChanged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastChangedUserId)
                .Index(t => t.OrderId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.LastChangedUserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        ClientUserId = c.String(nullable: false, maxLength: 128),
                        ManagerUserId = c.String(maxLength: 128),
                        LastChangedUserId = c.String(nullable: false, maxLength: 128),
                        TimeCreated = c.DateTime(nullable: false),
                        TimeChanged = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastChangedUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerUserId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.ClientUserId)
                .Index(t => t.ManagerUserId)
                .Index(t => t.LastChangedUserId);
            
            CreateTable(
                "dbo.OrderTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        Description = c.String(maxLength: 250),
                        AnswerManager = c.String(maxLength: 250),
                        NumberPeople = c.Int(),
                        OrderTime = c.DateTime(nullable: false),
                        NumberHours = c.DateTime(),
                        NeedPrepayment = c.Double(),
                        ReceivedPrepayment = c.Double(),
                        StatusPayment = c.String(nullable: false, maxLength: 30),
                        PaymentTypeId = c.Int(),
                        LastChangedUserId = c.String(nullable: false, maxLength: 128),
                        TimeCreated = c.DateTime(nullable: false),
                        TimeChanged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastChangedUserId)
                .Index(t => t.OrderId)
                .Index(t => t.TableId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.LastChangedUserId);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        PathTableFoto = c.String(maxLength: 247),
                        Name = c.String(nullable: false, maxLength: 150),
                        MaxNumberPeople = c.Int(),
                        MinNumberPeople = c.Int(),
                        Description = c.String(maxLength: 300),
                        Prepayment = c.Double(nullable: false),
                        Availability = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Avatar = c.String(maxLength: 247),
                        RestaurantId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RestaurantReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentReviewId = c.Int(),
                        RestaurantId = c.Int(nullable: false),
                        Review = c.String(nullable: false, maxLength: 1000),
                        Stars = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .ForeignKey("dbo.RestaurantReviews", t => t.ParentReviewId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ParentReviewId)
                .Index(t => t.RestaurantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NationalCuisines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        OwnParking = c.Boolean(nullable: false),
                        OwnParkingPlaces = c.Int(nullable: false),
                        NotOwnParking = c.Boolean(nullable: false),
                        NotOwnParkingPlaces = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.RestaurantPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 247),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.TargetAudiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeFoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.CurrencyRate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RestaurantAttribute",
                c => new
                    {
                        AttributeId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AttributeId, t.RestaurantId })
                .ForeignKey("dbo.Attributes", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.AttributeId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        DisheId = c.Int(nullable: false),
                        IngredientTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DisheId, t.IngredientTypeId })
                .ForeignKey("dbo.Dishes", t => t.DisheId, cascadeDelete: true)
                .ForeignKey("dbo.IngredientTypes", t => t.IngredientTypeId, cascadeDelete: true)
                .Index(t => t.DisheId)
                .Index(t => t.IngredientTypeId);
            
            CreateTable(
                "dbo.RestaurantPaymentTypes",
                c => new
                    {
                        PaymentTypeId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PaymentTypeId, t.RestaurantId })
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.DishRecommendation",
                c => new
                    {
                        DishId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DishId, t.UserId })
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.DishId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RestaurantNationalCuisines",
                c => new
                    {
                        NationalCuisineId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NationalCuisineId, t.RestaurantId })
                .ForeignKey("dbo.NationalCuisines", t => t.NationalCuisineId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.NationalCuisineId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.RestaurantTargetAudience",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false),
                        TargetAudienceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RestaurantId, t.TargetAudienceId })
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .ForeignKey("dbo.TargetAudiences", t => t.TargetAudienceId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.TargetAudienceId);
            
            CreateTable(
                "dbo.RestaurantTypeFood",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false),
                        TypeFoodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RestaurantId, t.TypeFoodId })
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .ForeignKey("dbo.TypeFoods", t => t.TypeFoodId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.TypeFoodId);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AddressId, t.UserId })
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAddresses", "AddressId", "dbo.Address");
            DropForeignKey("dbo.WorkSchedules", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantTypeFood", "TypeFoodId", "dbo.TypeFoods");
            DropForeignKey("dbo.RestaurantTypeFood", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantTargetAudience", "TargetAudienceId", "dbo.TargetAudiences");
            DropForeignKey("dbo.RestaurantTargetAudience", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantPhoto", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Parking", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantNationalCuisines", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantNationalCuisines", "NationalCuisineId", "dbo.NationalCuisines");
            DropForeignKey("dbo.DishRecommendation", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DishRecommendation", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.Dishes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.OrderDishesDetails", "OrderDishesId", "dbo.OrdersDishes");
            DropForeignKey("dbo.Orders", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RestaurantReviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RestaurantReviews", "ParentReviewId", "dbo.RestaurantReviews");
            DropForeignKey("dbo.RestaurantReviews", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.AspNetUsers", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.OrderTable", "LastChangedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrdersDishes", "LastChangedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ManagerUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ClientUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "LastChangedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tables", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.OrderTable", "TableId", "dbo.Tables");
            DropForeignKey("dbo.RestaurantPaymentTypes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantPaymentTypes", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.OrderTable", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.OrdersDishes", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.OrderTable", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrdersDishes", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDishesDetails", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.Ingredients", "IngredientTypeId", "dbo.IngredientTypes");
            DropForeignKey("dbo.Ingredients", "DisheId", "dbo.Dishes");
            DropForeignKey("dbo.Contacts", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Contacts", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.RestaurantAttribute", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantAttribute", "AttributeId", "dbo.Attributes");
            DropForeignKey("dbo.Restaurants", "AddressId", "dbo.Address");
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.UserAddresses", new[] { "AddressId" });
            DropIndex("dbo.RestaurantTypeFood", new[] { "TypeFoodId" });
            DropIndex("dbo.RestaurantTypeFood", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantTargetAudience", new[] { "TargetAudienceId" });
            DropIndex("dbo.RestaurantTargetAudience", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantNationalCuisines", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantNationalCuisines", new[] { "NationalCuisineId" });
            DropIndex("dbo.DishRecommendation", new[] { "UserId" });
            DropIndex("dbo.DishRecommendation", new[] { "DishId" });
            DropIndex("dbo.RestaurantPaymentTypes", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantPaymentTypes", new[] { "PaymentTypeId" });
            DropIndex("dbo.Ingredients", new[] { "IngredientTypeId" });
            DropIndex("dbo.Ingredients", new[] { "DisheId" });
            DropIndex("dbo.RestaurantAttribute", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantAttribute", new[] { "AttributeId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WorkSchedules", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantPhoto", new[] { "RestaurantId" });
            DropIndex("dbo.Parking", new[] { "RestaurantId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.RestaurantReviews", new[] { "UserId" });
            DropIndex("dbo.RestaurantReviews", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantReviews", new[] { "ParentReviewId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "RestaurantId" });
            DropIndex("dbo.Tables", new[] { "RestaurantId" });
            DropIndex("dbo.OrderTable", new[] { "LastChangedUserId" });
            DropIndex("dbo.OrderTable", new[] { "PaymentTypeId" });
            DropIndex("dbo.OrderTable", new[] { "TableId" });
            DropIndex("dbo.OrderTable", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "LastChangedUserId" });
            DropIndex("dbo.Orders", new[] { "ManagerUserId" });
            DropIndex("dbo.Orders", new[] { "ClientUserId" });
            DropIndex("dbo.Orders", new[] { "RestaurantId" });
            DropIndex("dbo.OrdersDishes", new[] { "LastChangedUserId" });
            DropIndex("dbo.OrdersDishes", new[] { "PaymentTypeId" });
            DropIndex("dbo.OrdersDishes", new[] { "OrderId" });
            DropIndex("dbo.OrderDishesDetails", new[] { "DishId" });
            DropIndex("dbo.OrderDishesDetails", new[] { "OrderDishesId" });
            DropIndex("dbo.Dishes", new[] { "RestaurantId" });
            DropIndex("dbo.Contacts", new[] { "ContactTypeId" });
            DropIndex("dbo.Contacts", new[] { "RestaurantId" });
            DropIndex("dbo.Restaurants", new[] { "AddressId" });
            DropTable("dbo.UserAddresses");
            DropTable("dbo.RestaurantTypeFood");
            DropTable("dbo.RestaurantTargetAudience");
            DropTable("dbo.RestaurantNationalCuisines");
            DropTable("dbo.DishRecommendation");
            DropTable("dbo.RestaurantPaymentTypes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.RestaurantAttribute");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CurrencyRate");
            DropTable("dbo.WorkSchedules");
            DropTable("dbo.TypeFoods");
            DropTable("dbo.TargetAudiences");
            DropTable("dbo.RestaurantPhoto");
            DropTable("dbo.Parking");
            DropTable("dbo.NationalCuisines");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.RestaurantReviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tables");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.OrderTable");
            DropTable("dbo.Orders");
            DropTable("dbo.OrdersDishes");
            DropTable("dbo.OrderDishesDetails");
            DropTable("dbo.IngredientTypes");
            DropTable("dbo.Dishes");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Contacts");
            DropTable("dbo.Attributes");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Address");
        }
    }
}
