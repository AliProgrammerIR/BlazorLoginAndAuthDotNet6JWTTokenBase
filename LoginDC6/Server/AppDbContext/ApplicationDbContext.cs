using LoginDC6.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LoginDC6.Server.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        /// <summary>
        /// Definition All Base Info . . .
        /// </summary>
        public virtual DbSet<TblBaseInformation> TblBaseInformations { get; set; }
        public virtual DbSet<TblFiscalPeriodYear> TblFiscalPeriodYears { get; set; }
        public virtual DbSet<TblPageNames> TblPageNames { get; set; }
        public virtual DbSet<TblPagesTexts> TblPagesTexts { get; set; }
        public virtual DbSet<TblPagesTextsEditHistory> TblPagesTextsEditHistories { get; set; }
        public virtual DbSet<TblUsersPermissions> TblUsersPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region PageNames
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 1, MnuTitle = "مدیریت کاربران", MnuLinkTitle = "UsersAdministration", PageParentID = 0, MnuOrderNumber = 10001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 2, MnuTitle = "مدیریت مشتریان", MnuLinkTitle = "CustomersAdministration", PageParentID = 0, MnuOrderNumber = 20001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 3, MnuTitle = "سوابق بیمه مشتری", MnuLinkTitle = "CustomersInsuranceHistoryAdministration", PageParentID = 0, MnuOrderNumber = 30001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 4, MnuTitle = "مدیریت اطلاعات پایه", MnuLinkTitle = "BaseInfoAdministration", PageParentID = 0, MnuOrderNumber = 40001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 5, MnuTitle = "مدیریت فیلد های اضافی", MnuLinkTitle = "BaseInfoExtraFieldsAdministration", PageParentID = 0, MnuOrderNumber = 50001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 32, MnuTitle = "جستجوی سوابق بیمه", MnuLinkTitle = "BaseInfoExtraFieldsAdministration", PageParentID = 0, MnuOrderNumber = 50001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 33, MnuTitle = "جستجو", MnuLinkTitle = "BaseInfoExtraFieldsAdministration", PageParentID = 32, MnuOrderNumber = 50001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 34, MnuTitle = "خروجی اکسل", MnuLinkTitle = "BaseInfoExtraFieldsAdministration", PageParentID = 32, MnuOrderNumber = 50001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 6, MnuTitle = "ورود به سیستم", PageParentID = 1, MnuOrderNumber = 60001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 7, MnuTitle = "مشاهده لیست کاربران", PageParentID = 1, MnuOrderNumber = 70001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 8, MnuTitle = "ویرایش کاربر", PageParentID = 1, MnuOrderNumber = 80001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 9, MnuTitle = "حذف کاربر", PageParentID = 1, MnuOrderNumber = 90001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 10, MnuTitle = "دسترسی کاربران", PageParentID = 1, MnuOrderNumber = 100001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 11, MnuTitle = "مدیریت رول های کاربران", PageParentID = 1, MnuOrderNumber = 110001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 12, MnuTitle = "مشاهده لیست مشتریان", PageParentID = 2, MnuOrderNumber = 120001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 13, MnuTitle = "مشاهده جزئیات اطلاعات مشتری", PageParentID = 2, MnuOrderNumber = 130001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 14, MnuTitle = "ثبت مشتری جدید", PageParentID = 2, MnuOrderNumber = 140001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 15, MnuTitle = "ویرایش اطلاعات مشتری", PageParentID = 2, MnuOrderNumber = 150001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 16, MnuTitle = "حذف مشتری", PageParentID = 2, MnuOrderNumber = 160001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 17, MnuTitle = "مشاهده لیست سوابق بیمه", PageParentID = 3, MnuOrderNumber = 170001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 18, MnuTitle = "ثبت سابقه بیمه", PageParentID = 3, MnuOrderNumber = 180001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 19, MnuTitle = "ویرایش سابقه بیمه", PageParentID = 3, MnuOrderNumber = 190001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 20, MnuTitle = "حذف سابقه بیمه", PageParentID = 3, MnuOrderNumber = 200001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 21, MnuTitle = "مشاهده جزئیات بیشتر از بیمه نامه", PageParentID = 3, MnuOrderNumber = 210001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 22, MnuTitle = "آپلود فایل", PageParentID = 3, MnuOrderNumber = 220001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 23, MnuTitle = "حذف فایل ", PageParentID = 3, MnuOrderNumber = 230001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 24, MnuTitle = "مشاهده لیست اطلاعات پایه", PageParentID = 4, MnuOrderNumber = 240001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 25, MnuTitle = "ویرایش اطلاعات پایه", PageParentID = 4, MnuOrderNumber = 250001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 26, MnuTitle = "حذف اطلاعات پایه", PageParentID = 4, MnuOrderNumber = 260001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 27, MnuTitle = "ثبت اطلاعات پایه", PageParentID = 4, MnuOrderNumber = 265001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 28, MnuTitle = "مشاهده فیلد های اضافی", PageParentID = 5, MnuOrderNumber = 270001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 29, MnuTitle = "ثبت فیلد اضافی", PageParentID = 5, MnuOrderNumber = 280001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 30, MnuTitle = "ویرایش فیلد اضافی", PageParentID = 5, MnuOrderNumber = 290001, IsVisible = true });
            builder.Entity<TblPageNames>().HasData(new TblPageNames() { PageID = 31, MnuTitle = "حذف فیلد اضافی", PageParentID = 5, MnuOrderNumber = 300001, IsVisible = true });
            #endregion


            builder.Entity<TblUsersPermissions>()
             .HasOne(e => e.PageNames)
             .WithMany()
             .HasForeignKey(e => e.PageId);

            builder.Entity<TblUsersPermissions>()
                .HasOne(e => e.AppUsers)
                .WithMany()
                .HasForeignKey(e => e.PUserID);

            builder.Entity<TblPagesTexts>()
        .HasMany(pt => pt.EditHistory)
        .WithOne(e => e.PagesTexts)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TblUsersPermissions>(entity =>
            {
                entity.HasKey(e => e.PageAccessPermId);
                entity.Property(e => e.PageAccessPermId)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();
                entity.HasIndex(e => new { e.PageId, e.PUserID }, "IX_UsersPermissions")
                .IsUnique();
            });
            base.OnModelCreating(builder);
        }
    }
}