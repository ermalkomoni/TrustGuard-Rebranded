using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrustGuardAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    PersonalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
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
                name: "OrderHeaders",
                columns: table => new
                {
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderTotal = table.Column<double>(type: "float", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripePaymentIntentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalItems = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.OrderHeaderId);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_InsuranceTypes_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_InsuranceTypes_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "OrderHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InsuranceTypes",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[,]
                {
                    { 1, "Car", "With TrustGuard's CASCO insurance, you can safeguard your vehicle from potential financial losses due to damage, regardless of who is at fault. Protect yourself anywhere/anytime.", "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", "Casco", 8.9900000000000002, "Best Seller" },
                    { 2, "Property", "With property insurance at TrustGuard you can protect your business space, as well as its contents, restoring the business to its proper condition without the need for investment.", "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", "House", 11.99, null },
                    { 3, "Car", "Compulsory auto liability insurance is a legal requirement for vehicle owners, shielding you from financial responsibility for any damages you cause to a third party with your vehicle.", "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", "TPL", 14.99, "Special Price" },
                    { 4, "Property", "TrustGuard's personal property insurance ensures that your personal space remains protected and comfortable by covering the cost of replacing any lost or damaged items.", "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", "Apartment", 11.99, null },
                    { 5, "Car", "TrustGuard's health insurance offers comprehensive coverage for unexpected medical expenses, providing financial support for your health needs. Protect yourself anywhere/anytime.", "iVBORw0KGgoAAAANSUhEUgAAAJUAAACVCAYAAABRorhPAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyFpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNS1jMDIxIDc5LjE1NDkxMSwgMjAxMy8xMC8yOS0xMTo0NzoxNiAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpCQkExNjJDQzdGMDAxMUU2OTQ0OUYzNThCRjg1QjU5MiIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpCQkExNjJDRDdGMDAxMUU2OTQ0OUYzNThCRjg1QjU5MiI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkJCQTE2MkNBN0YwMDExRTY5NDQ5RjM1OEJGODVCNTkyIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkJCQTE2MkNCN0YwMDExRTY5NDQ5RjM1OEJGODVCNTkyIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+uDun7gAAE11JREFUeNrsXQt0VdWZ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", "Health", 8.9900000000000002, null },
                    { 6, "Individual", "Prepare for unexpected accidents by purchasing personal accident insurance, which ensures direct compensation in the event of injury resulting from an accident.", "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", "Accident", 11.99, null }
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
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_InsuranceTypeId",
                table: "CartItems",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_InsuranceTypeId",
                table: "OrderDetails",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
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
                name: "Books");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "InsuranceTypes");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
