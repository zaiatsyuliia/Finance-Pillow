using Financeillow.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Financeillow.Data
{
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

        public virtual DbSet<Income> Incomes { get; set; }

        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }

        public virtual DbSet<User> Users { get; set; }

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}