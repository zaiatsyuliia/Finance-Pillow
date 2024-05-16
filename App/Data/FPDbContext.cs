using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class FPDbContext : IdentityDbContext<ApplicationUser>
{
    public FPDbContext()
    {
    }

    public FPDbContext(DbContextOptions<FPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Expense6MonthsMonthly> Expense6MonthsMonthlies { get; set; }

    public virtual DbSet<Expense6MonthsTotal> Expense6MonthsTotals { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<ExpenseMonthDaily> ExpenseMonthDailies { get; set; }

    public virtual DbSet<ExpenseMonthTotal> ExpenseMonthTotals { get; set; }

    public virtual DbSet<ExpenseYearMonthly> ExpenseYearMonthlies { get; set; }

    public virtual DbSet<ExpenseYearTotal> ExpenseYearTotals { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Income6MonthsMonthly> Income6MonthsMonthlies { get; set; }

    public virtual DbSet<Income6MonthsTotal> Income6MonthsTotals { get; set; }

    public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }

    public virtual DbSet<IncomeMonthDaily> IncomeMonthDailies { get; set; }

    public virtual DbSet<IncomeMonthTotal> IncomeMonthTotals { get; set; }

    public virtual DbSet<IncomeYearMonthly> IncomeYearMonthlies { get; set; }

    public virtual DbSet<IncomeYearTotal> IncomeYearTotals { get; set; }

    public virtual DbSet<Limit> Limits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=FinancePillow;User Id=postgres;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Budget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("budget");


            entity.Property(e => e.Budget1).HasColumnName("budget");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("expense_pkey");

            entity.ToTable("expense");

            entity.Property(e => e.ExpenseId).HasColumnName("expense_id");
            entity.Property(e => e.ExpenseCategoryId).HasColumnName("expense_category_id");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ExpenseCategory)
                .WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ExpenseCategoryId)
                .HasConstraintName("expense_expense_category_id_fkey");

            // Ensure you're using the correct type for IdentityUser here, replace 'IdentityUser' with 'ApplicationUser' if using a custom type.
            entity.HasOne<ApplicationUser>(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("expense_user_id_fkey")
                .OnDelete(DeleteBehavior.Cascade); // Assuming cascade delete is appropriate for your logic.
        });

        modelBuilder.Entity<Expense6MonthsMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_6_months_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Expense6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_6_months_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.ExpenseCategoryId).HasName("expense_category_pkey");

            entity.ToTable("expense_category");

            entity.Property(e => e.ExpenseCategoryId).HasColumnName("expense_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ExpenseMonthDaily>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_month_daily");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Day)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("day");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<ExpenseMonthTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_month_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<ExpenseYearMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_year_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<ExpenseYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_year_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("history");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("income_pkey");

            entity.ToTable("income");

            entity.Property(e => e.IncomeId).HasColumnName("income_id");
            entity.Property(e => e.IncomeCategoryId).HasColumnName("income_category_id");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.IncomeCategory).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IncomeCategoryId)
                .HasConstraintName("income_income_category_id_fkey");

            entity.HasOne<ApplicationUser>(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("income_user_id_fkey")
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Income6MonthsMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_6_months_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Income6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_6_months_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IncomeCategory>(entity =>
        {
            entity.HasKey(e => e.IncomeCategoryId).HasName("income_category_pkey");

            entity.ToTable("income_category");

            entity.Property(e => e.IncomeCategoryId).HasColumnName("income_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<IncomeMonthDaily>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_month_daily");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Day)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("day");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IncomeMonthTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_month_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IncomeYearMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_year_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IncomeYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_year_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Limit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("limits");

            entity.Property(e => e.ExpenseLimit).HasColumnName("expense_limit");
            entity.Property(e => e.ExpenseLimitExceeded).HasColumnName("expense_limit_exceeded");
            entity.Property(e => e.IncomeLimit).HasColumnName("income_limit");
            entity.Property(e => e.IncomeLimitExceeded).HasColumnName("income_limit_exceeded");
            entity.Property(e => e.TotalExpense).HasColumnName("total_expense");
            entity.Property(e => e.TotalIncome).HasColumnName("total_income");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}