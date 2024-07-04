using HRMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data
{
    public partial class HrmsContext :DbContext
    {
        public HrmsContext() 
        {

        }
        public HrmsContext(DbContextOptions<HrmsContext> options) : base(options) 
        {

        }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

        public virtual DbSet<AspNetUserLogin> AspNetUsersLogin { get; set; }

        public virtual DbSet<AspNetUserRole> AspNetUserRoleClaims { get; set; }

        public virtual DbSet<AspNetUserToken> AspNetUserTokenClaims { get; set; }

        public virtual DbSet<TblUserLogingDetail> TblUserLogingDetails { get; set; }

        public virtual DbSet<TblClientCredential> TblClientCredentials { get; set; }

        public virtual DbSet<TblCountry> TblCountries { get; set; }

        public virtual DbSet<TblClientList> TblClientLists { get; set; }

        public virtual DbSet<TblCustomer> TblCustomers { get; set; }

        public virtual DbSet<TblMenu> TblMenus { get; set; }

        public virtual DbSet<TblRoleMenuPermission> TblRoleMenuPermissions { get; set; }    

        public virtual DbSet<TblRole> TblRoles {  get; set; }   

        public virtual DbSet<TblUserRole> TblUserRoles { get; set;}  

        public virtual DbSet<TblGender> TblGenders {  get; set; }

        public virtual DbSet<TblUser> TblUsers {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-8FKF1RD;Database=HRMS_Test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=false;");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedNAme]) IS NOt NULL");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<TblClientCredential>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblClientList>(entity =>
            {
                entity.HasKey(e => e.CompanyCode)
                .HasName("");
                entity.Property(e => e.CreatedNepaliDate).IsUnicode(false);
                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
                entity.Property(e => e.UpdatedNepaliDate).IsUnicode(false);
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.Property(e => e.CountryCode).IsUnicode(false);
                entity.Property(e => e.LangCode).IsUnicode(false);
                entity.Property(e => e.Region).IsUnicode(false);
                entity.Property(e => e.Zone).IsUnicode(false);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                .HasName("");

                entity.Property(e => e.EmailConfirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ForceMpinchange)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength(true);

                entity.Property(e => e.ForcePasswordChange)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength(true);

                entity.Property(e => e.ForceTxnpinchange)
                .IsUnicode(false)
                .HasDefaultValueSql("(''Y)")
                .IsFixedLength (true);

                entity.Property(e => e.MobileConfirmed).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.IsSuperAdmin).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.GenderNavigation)
                .WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.Gender)
                .HasConstraintName("FK__tbl_users__Gende__79FD19BE");
            });

            modelBuilder.Entity<TblUserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__tbl_user___RoleI__1699586C");

                entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tbl_user___UserI__15A53433");
            });

            modelBuilder.Entity<TblRoleMenuPermission>(entity =>
            {
                entity.Property(e => e.CreatePer).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.CreatedNepaliDate).IsUnicode(false);

                entity.Property(e => e.DeletePer).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsActive).HasComputedColumnSql("(CONVERT([bit],(([ViewPer]|[CreatePer])|[UpdatePer])|[DeletePer]))", true);

                entity.Property(e => e.UpdatePer).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.UpdatedNepaliDate).IsUnicode(false);

                entity.Property(e => e.ViewPer).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblRoleMenuPermissions)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_role___MenuI__4830B400");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblRoleMenuPermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_role___RoleI__473C8FC7");
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
    
}
