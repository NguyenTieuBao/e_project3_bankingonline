using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Models;

namespace BankingSystem.Data;

public partial class EBankingonlineContext : DbContext
{
    public EBankingonlineContext()
    {
    }

    public EBankingonlineContext(DbContextOptions<EBankingonlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Adminprofile> Adminprofiles { get; set; }

    public virtual DbSet<Adminrole> Adminroles { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Tokenblacklist> Tokenblacklists { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transfertransaction> Transfertransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }
    public virtual DbSet<Notification> Notifications{ get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.HasIndex(e => e.AccountNumber, "UQ_AccountNumber").IsUnique();

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Balance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_accounts_users");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("admins");

            entity.HasIndex(e => e.Username, "UQ_Username").IsUnique();

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__admins__RoleId__3F466844");
        });

        modelBuilder.Entity<Adminprofile>(entity =>
        {
            entity.ToTable("adminprofile");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Admin).WithMany(p => p.Adminprofiles)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__adminprof__Admin__4222D4EF");
        });

        modelBuilder.Entity<Adminrole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("adminroles");

            entity.HasIndex(e => e.RoleName, "UQ_RoleName").IsUnique();

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.ToTable("otp");

            entity.Property(e => e.Otpid).HasColumnName("OTPId");
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OTPCode");

            entity.HasOne(d => d.User).WithMany(p => p.Otps)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__otp__UserId__52593CB8");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("requests");

            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__requests__UserId__5629CD9C");
        });

        modelBuilder.Entity<Tokenblacklist>(entity =>
        {
            entity.HasKey(e => e.TokenId);

            entity.ToTable("tokenblacklist");

            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");

            entity.Property(e => e.Amount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__transacti__Accou__60A75C0F");

            entity.HasOne(d => d.Admin).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__transacti__Admin__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__transacti__UserI__5FB337D6");
        });

        modelBuilder.Entity<Transfertransaction>(entity =>
        {
            entity.HasKey(e => e.TransferId);

            entity.ToTable("transfertransactions");

            entity.Property(e => e.Amount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransferDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransfertransactionFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .HasConstraintName("FK__transfert__FromA__6477ECF3");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransfertransactionToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .HasConstraintName("FK__transfert__ToAcc__656C112C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ_Username_New").IsUnique();

            entity.Property(e => e.AccountLocked).HasDefaultValue(false);
            entity.Property(e => e.FailedLoginAttempts).HasDefaultValue(0);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordResetExpiry).HasColumnType("datetime");
            entity.Property(e => e.PasswordResetToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserToke__1788CC4C5A0172AC");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithOne(p => p.UserToken)
                .HasForeignKey<UserToken>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserToken__UserI__5AEE82B9");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.ToTable("userprofile");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Userprofiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__userprofi__UserI__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
