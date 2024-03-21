using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;
public partial class MyContext : DbContext
{
    public MyContext()
    {
    }
    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<ExpenseCurrentMonth> ExpenseCurrentMonths { get; set; }

    public virtual DbSet<ExpenseCurrentMonthDaily> ExpenseCurrentMonthDailies { get; set; }

    public virtual DbSet<ExpenseLast6Month> ExpenseLast6Months { get; set; }

    public virtual DbSet<ExpenseLast6MonthsTotal> ExpenseLast6MonthsTotals { get; set; }

    public virtual DbSet<ExpenseLastYear> ExpenseLastYears { get; set; }

    public virtual DbSet<ExpenseLastYearTotal> ExpenseLastYearTotals { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }

    public virtual DbSet<IncomeCurrentMonth> IncomeCurrentMonths { get; set; }

    public virtual DbSet<IncomeCurrentMonthDaily> IncomeCurrentMonthDailies { get; set; }

    public virtual DbSet<IncomeLast6Month> IncomeLast6Months { get; set; }

    public virtual DbSet<IncomeLast6MonthsTotal> IncomeLast6MonthsTotals { get; set; }

    public virtual DbSet<IncomeLastYear> IncomeLastYears { get; set; }

    public virtual DbSet<IncomeLastYearTotal> IncomeLastYearTotals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBudget> UserBudgets { get; set; }

    public DbSet<HistoryEntry> History { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=FinancePillow;User Id=postgres;Password=postgres;");

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

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategoryExpense).HasName("expense_category_pkey");

            entity.ToTable("expense_category");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ExpenseCurrentMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_current_month");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseCurrentMonthDaily>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_current_month_daily");

            entity.Property(e => e.ExpenseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expense_date");
            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseLast6Month>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_last_6_months");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseLast6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_last_6_months_total");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseLastYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_last_year");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<ExpenseLastYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("expense_last_year_total");

            entity.Property(e => e.IdCategoryExpense).HasColumnName("id_category_expense");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
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

        modelBuilder.Entity<IncomeCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategoryIncome).HasName("income_category_pkey");

            entity.ToTable("income_category");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<IncomeCurrentMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_current_month");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeCurrentMonthDaily>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_current_month_daily");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IncomeDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("income_date");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeLast6Month>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_last_6_months");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeLast6MonthsTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_last_6_months_total");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeLastYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_last_year");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Month)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("month");
            entity.Property(e => e.TotalSum).HasColumnName("total_sum");
        });

        modelBuilder.Entity<IncomeLastYearTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("income_last_year_total");

            entity.Property(e => e.IdCategoryIncome).HasColumnName("id_category_income");
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
        });

        modelBuilder.Entity<UserBudget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("user_budget");

            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
        });

        modelBuilder.Entity<HistoryEntry>(entity =>
        {
            entity.HasNoKey(); // As the view doesn't have a primary key

            entity.ToView("history"); // Specify the view name

            // Map properties to columns
            entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("id_user");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Sum).HasColumnName("sum");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
