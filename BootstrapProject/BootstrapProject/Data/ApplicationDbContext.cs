using BootstrapProject.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task = BootstrapProject.Models.Task;

namespace BootstrapProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<AccountEntries> AccountEntries { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CashFlow> CashFlow { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<CustomerTask> CustomerTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder){

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID);

            entity.Property(e => e.UserID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(e => e.UserType)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Address)
                .HasMaxLength(255);

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.ContactNo)
                .HasMaxLength(15);

            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Employee>(entity =>
        {   
            // // PRIMARY KEY
            entity.HasKey(e => e.EmployeeID);

            entity.Property(e => e.EmployeeID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserID);


            // FOREIGN KEYS 
            entity.HasOne(e => e.SystemRole)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.SystemRoleID);
            

            entity.HasOne(e => e.EmployeeType)
                .WithMany()
                .HasForeignKey(e => e.EmployeeTypeID);

            // Others            
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.CustomerID);

            entity.Property(e => e.CustomerID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Customer>(c => c.UserID);

            entity.Property(c => c.CreditCardInfo)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<SystemRole>(entity =>
        {
            entity.HasKey(sr => sr.SystemRoleID);

            entity.Property(e => e.SystemRoleID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(sr => sr.RoleName)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(sr => sr.AbsenceID);

            entity.Property(e => e.AbsenceID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(e => e.ReasonOfAbsence)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(p => p.PayrollID);

            entity.Property(e => e.PayrollID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeID);

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(et => et.EmployeeTypeID);

            entity.Property(e => e.EmployeeTypeID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(et => et.EmployeeTitle)
                .HasMaxLength(100);

            entity.Property(e => e.BaseSalary)
                .HasColumnType("decimal(10,2)");
        });

        // Configure TPH for Account and its derived types
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.AccountID);
            entity.HasIndex(a => a.isCompanyOrEmployee); // Ensure disjointness

            entity.Property(e => e.AccountID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(e => e.Employee)
                .WithOne(e => e.Account)
                .HasForeignKey<Employee>(e => e.AccountID)
                .OnDelete(DeleteBehavior.Cascade);

             entity.HasOne(c => c.Company)
                .WithOne(c => c.Account)
                .HasForeignKey<Company>(c => c.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(a => a.Balance)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.CompanyID);

            entity.Property(e => e.CompanyID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(c=>c.Account)
                .WithOne(a => a.Company)
                .HasForeignKey<Company>(c => c.AccountID);
        });
        
        // Configure TPH for Account and its derived types
        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(at => at.AccountTypeID);

            entity.Property(e => e.AccountTypeID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(at => at.AccountTypeName)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.HasKey(od => od.OrderDetailsID);

            entity.Property(e => e.OrderDetailsID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(od => od.Company)
                .WithMany()
                .HasForeignKey(od => od.CompanyID);

            entity.HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            entity.HasOne(od => od.ProductWarehouse)
                .WithMany()
                .HasForeignKey(od => od.ProductWarehouseID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            entity.Property(od => od.UnitPrice)
                .HasColumnType("decimal(10,2)");

            entity.Property(od => od.TotalPrice)
                .HasColumnType("decimal(10,2)");
        });


        modelBuilder.Entity<ProductWarehouse>(entity =>
        {
            entity.HasKey(pw => pw.ProductWarehouseID);

            entity.Property(e => e.ProductWarehouseID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(pw => pw.Product)
                .WithMany()
                .HasForeignKey(pw => pw.ProductID);

            entity.HasOne(pw => pw.Warehouse)
                .WithMany()
                .HasForeignKey(pw => pw.WarehouseID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            entity.Property(pw => pw.UnitPrice)
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(w => w.WarehouseID);

            entity.Property(e => e.WarehouseID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(w => w.WarehouseName)
                .HasMaxLength(255);

            entity.Property(w => w.Address)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductID);

            entity.Property(e => e.ProductID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            entity.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION


            entity.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierID);


            entity.Property(p => p.ProductName)
                .HasMaxLength(255);

            entity.Property(p => p.SKU)
                .HasMaxLength(100);

            entity.Property(p => p.imageUrl)
                .HasMaxLength(255);

            entity.Property(p => p.UnitPrice)
                .HasColumnType("decimal(10,2)");

            entity.Property(p => p.CostPrice)
                .HasColumnType("decimal(10,2)");

            entity.Property(p => p.Condition)
                .HasMaxLength(25);

            entity.Property(p => p.BatchNo)
                .HasMaxLength(100);

            entity.Property(p => p.Notes)
                .HasMaxLength(255);
        });


        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(s => s.SupplierID);

            entity.Property(e => e.SupplierID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(s => s.SupplierName)
                .HasMaxLength(255);

            entity.Property(s => s.Address)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.BrandID);

            entity.Property(e => e.BrandID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(b => b.BrandName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(b => b.Description)
                .HasMaxLength(255);
        });


        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.CategoryID);

            entity.Property(e => e.CategoryID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(c => c.Description)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.ReviewID);

            entity.Property(e => e.ReviewID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION
;

            entity.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductID);

            entity.Property(r => r.Rating)
                .IsRequired();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderID);

            entity.Property(e => e.OrderID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerID);

            entity.Property(p => p.DeliveryPrice)
                .HasColumnType("decimal(10,2)");

            entity.Property(o => o.PaymentType)
                .HasMaxLength(20);

            entity.Property(o => o.ShippingAddress)
                .HasMaxLength(255);
        });

        modelBuilder
        .Entity<Review>()
        .ToTable(b => b.HasCheckConstraint("Ck_ReviewRating", "[Rating] >= 1 AND [Rating] <= 5"));

        modelBuilder.Entity<CustomerTask>(entity =>
        {
            entity.HasKey(c => c.CustomerTaskID);

            entity.Property(e => e.CustomerTaskID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerID);

            entity.HasOne(c => c.Task)
                .WithMany()
                .HasForeignKey(c => c.TaskID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION
;

            entity.HasOne(c => c.Employee)
                .WithMany()
                .HasForeignKey(c => c.AssignEmployeeID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION
;

            entity.Property(c => c.Status)
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(t => t.TaskID);

            entity.Property(e => e.TaskID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.Property(t => t.TaskDescription)
                .HasMaxLength(255);

            entity.Property(t => t.TaskType)
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Interaction>(entity =>
        {
            entity.HasKey(i => i.InteractionID);

            entity.Property(e => e.InteractionID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            entity.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerID)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            entity.HasOne(i => i.Employee)
                .WithMany()
                .HasForeignKey(i => i.EmployeeID);

            entity.Property(i => i.Subject)
                .HasMaxLength(100);

            entity.Property(i => i.Details)
                .HasMaxLength(255);
        
            entity.Property(i => i.Outcome)
                .HasMaxLength(255);
        });


        }
    }
}