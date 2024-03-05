using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginDC6.Server.Migrations
{
    public partial class M1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Bse");

            migrationBuilder.EnsureSchema(
                name: "Inf");

            migrationBuilder.EnsureSchema(
                name: "Usr");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LandLinePhoneNumber = table.Column<string>(type: "nvarchar(3900)", maxLength: 3900, nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(3900)", maxLength: 3900, nullable: false),
                    CanRememberMe = table.Column<bool>(type: "bit", nullable: false),
                    IsWoman = table.Column<bool>(type: "bit", nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(3900)", maxLength: 3900, nullable: false),
                    WorkAddress = table.Column<string>(type: "nvarchar(3900)", maxLength: 3900, nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: true),
                    PictureOfUser = table.Column<byte[]>(type: "varbinary(1500)", maxLength: 1500, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblBaseInformation",
                schema: "Bse",
                columns: table => new
                {
                    BsInfoID = table.Column<long>(type: "bigint", nullable: false),
                    BsInfoValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BsInfoDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BsInfoFurtherInfo = table.Column<string>(type: "nvarchar(3500)", maxLength: 3500, nullable: false),
                    BlnIsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateOfInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BsInfoTypeID = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBaseInformation", x => x.BsInfoID);
                });

            migrationBuilder.CreateTable(
                name: "TblPageNames",
                schema: "Inf",
                columns: table => new
                {
                    PageID = table.Column<int>(type: "int", nullable: false),
                    MnuTitle = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    MnuLinkTitle = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    MnuOrderNumber = table.Column<int>(type: "int", nullable: false),
                    PageParentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPageNames", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPagesTexts",
                schema: "Inf",
                columns: table => new
                {
                    TextId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortText = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LongText = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPagesTexts", x => x.TextId);
                    table.ForeignKey(
                        name: "FK_TblPagesTexts_AspNetUsers_InsertedById",
                        column: x => x.InsertedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUsersPermissions",
                schema: "Usr",
                columns: table => new
                {
                    PageAccessPermId = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    PUserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PageCrcPerm = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUsersPermissions", x => x.PageAccessPermId);
                    table.ForeignKey(
                        name: "FK_TblUsersPermissions_AspNetUsers_PUserID",
                        column: x => x.PUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblUsersPermissions_TblPageNames_PageId",
                        column: x => x.PageId,
                        principalSchema: "Inf",
                        principalTable: "TblPageNames",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPagesTextsEditHistory",
                schema: "Inf",
                columns: table => new
                {
                    TextHistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextId = table.Column<int>(type: "int", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ShortTextPre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LongTextPre = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ShortTextNew = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LongTextNew = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPagesTextsEditHistory", x => x.TextHistId);
                    table.ForeignKey(
                        name: "FK_TblPagesTextsEditHistory_TblPagesTexts_TextId",
                        column: x => x.TextId,
                        principalSchema: "Inf",
                        principalTable: "TblPagesTexts",
                        principalColumn: "TextId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Inf",
                table: "TblPageNames",
                columns: new[] { "PageID", "IsVisible", "MnuLinkTitle", "MnuOrderNumber", "MnuTitle", "PageParentID" },
                values: new object[,]
                {
                    { 1, true, "UsersAdministration", 10001, "مدیریت کاربران", 0 },
                    { 2, true, "CustomersAdministration", 20001, "مدیریت مشتریان", 0 },
                    { 3, true, "CustomersInsuranceHistoryAdministration", 30001, "سوابق بیمه مشتری", 0 },
                    { 4, true, "BaseInfoAdministration", 40001, "مدیریت اطلاعات پایه", 0 },
                    { 5, true, "BaseInfoExtraFieldsAdministration", 50001, "مدیریت فیلد های اضافی", 0 },
                    { 6, true, null, 60001, "ورود به سیستم", 1 },
                    { 7, true, null, 70001, "مشاهده لیست کاربران", 1 },
                    { 8, true, null, 80001, "ویرایش کاربر", 1 },
                    { 9, true, null, 90001, "حذف کاربر", 1 },
                    { 10, true, null, 100001, "دسترسی کاربران", 1 },
                    { 11, true, null, 110001, "مدیریت رول های کاربران", 1 },
                    { 12, true, null, 120001, "مشاهده لیست مشتریان", 2 },
                    { 13, true, null, 130001, "مشاهده جزئیات اطلاعات مشتری", 2 },
                    { 14, true, null, 140001, "ثبت مشتری جدید", 2 },
                    { 15, true, null, 150001, "ویرایش اطلاعات مشتری", 2 },
                    { 16, true, null, 160001, "حذف مشتری", 2 },
                    { 17, true, null, 170001, "مشاهده لیست سوابق بیمه", 3 },
                    { 18, true, null, 180001, "ثبت سابقه بیمه", 3 },
                    { 19, true, null, 190001, "ویرایش سابقه بیمه", 3 },
                    { 20, true, null, 200001, "حذف سابقه بیمه", 3 },
                    { 21, true, null, 210001, "مشاهده جزئیات بیشتر از بیمه نامه", 3 },
                    { 22, true, null, 220001, "آپلود فایل", 3 },
                    { 23, true, null, 230001, "حذف فایل ", 3 },
                    { 24, true, null, 240001, "مشاهده لیست اطلاعات پایه", 4 },
                    { 25, true, null, 250001, "ویرایش اطلاعات پایه", 4 },
                    { 26, true, null, 260001, "حذف اطلاعات پایه", 4 },
                    { 27, true, null, 265001, "ثبت اطلاعات پایه", 4 },
                    { 28, true, null, 270001, "مشاهده فیلد های اضافی", 5 },
                    { 29, true, null, 280001, "ثبت فیلد اضافی", 5 },
                    { 30, true, null, 290001, "ویرایش فیلد اضافی", 5 },
                    { 31, true, null, 300001, "حذف فیلد اضافی", 5 },
                    { 32, true, "BaseInfoExtraFieldsAdministration", 50001, "جستجوی سوابق بیمه", 0 },
                    { 33, true, "BaseInfoExtraFieldsAdministration", 50001, "جستجو", 32 },
                    { 34, true, "BaseInfoExtraFieldsAdministration", 50001, "خروجی اکسل", 32 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TblPagesTexts_InsertedById",
                schema: "Inf",
                table: "TblPagesTexts",
                column: "InsertedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblPagesTextsEditHistory_TextId",
                schema: "Inf",
                table: "TblPagesTextsEditHistory",
                column: "TextId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsersPermissions_PUserID",
                schema: "Usr",
                table: "TblUsersPermissions",
                column: "PUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPermissions",
                schema: "Usr",
                table: "TblUsersPermissions",
                columns: new[] { "PageId", "PUserID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TblBaseInformation",
                schema: "Bse");

            migrationBuilder.DropTable(
                name: "TblPagesTextsEditHistory",
                schema: "Inf");

            migrationBuilder.DropTable(
                name: "TblUsersPermissions",
                schema: "Usr");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TblPagesTexts",
                schema: "Inf");

            migrationBuilder.DropTable(
                name: "TblPageNames",
                schema: "Inf");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
