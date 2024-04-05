using System;
using System.Collections.Generic;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseMonthLimitComparison> ExpenseMonthLimitComparisons { get; set; }

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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBudget> UserBudgets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=FinancePillow;User Id=postgres;Password=postgres;");
#pragma warning restore CS1030 // #warning directive

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.IdExpense).HasName("expense_pkey");

            entity.ToTable("expense");

            entity.Property(e => e.IdExpense).HasColumnName("id_expense");
            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");

            entity.HasOne(d => d.IdCategoryExpenseNavigation).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.IdCategoryExpense)
                .HasConstraintName("expense_id_category_expense_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("expense_id_user_fkey");
        });

        modelBuilder.Entity<Expense6MonthsMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_6_months_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<Expense6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_6_months_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("expense_category_pkey");

            entity.ToTable("expense_category");

            entity.Property(e => e.IdCategory).HasColumnName("id_category_expense");
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
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseMonthTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_month_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseYearMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_year_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_year_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("history");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IdIncome).HasName("income_pkey");

            entity.ToTable("income");

            entity.Property(e => e.IdIncome).HasColumnName("id_income");
            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Sum).HasColumnName("sum");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");

            entity.HasOne(d => d.IdCategoryIncomeNavigation).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IdCategoryIncome)
                .HasConstraintName("income_id_category_income_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("income_id_user_fkey");
        });

        modelBuilder.Entity<Income6MonthsMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_6_months_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<Income6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_6_months_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("income_category_pkey");

            entity.ToTable("income_category");

            entity.Property(e => e.IdCategory).HasColumnName("id_category_income");
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
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeMonthTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_month_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeYearMonthly>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_year_monthly");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_year_total");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Limit)
            .HasColumnName("limit");
        });

        modelBuilder.Entity<UserBudget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("user_budget");

            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
        });

        modelBuilder.Entity<ExpenseMonthLimitComparison>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_month_limit_comparison");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
            entity.Property(e => e.UserLimit).HasColumnName("user_limit");
            entity.Property(e => e.LimitStatus).HasColumnName("limit_status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
