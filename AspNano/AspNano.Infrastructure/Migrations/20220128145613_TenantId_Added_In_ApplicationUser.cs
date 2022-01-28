using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNano.Infrastructure.Migrations
{
    public partial class TenantId_Added_In_ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Key", "LastModifiedBy", "LastModifiedOn" },
                values: new object[] { new Guid("297af0a9-060d-4ac7-b014-e421588150a0"), new Guid("29faf0a9-060d-4ac7-b014-e421588150a0"), new DateTime(2022, 1, 28, 14, 56, 13, 351, DateTimeKind.Utc).AddTicks(4141), null, null, "root", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 1, 28, 14, 56, 13, 351, DateTimeKind.Utc).AddTicks(4142) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "22d61f2e-2364-4aec-b673-66cb8d31be31", "AQAAAAEAACcQAAAAEId1UB8Mfq8IMTsHvRQ56P1lgm+gBE4Ni3sJpinx3FSqGeGZETeL4Xe+VlhjLJkCnw==", "f03376fe-add5-41f1-8a83-065088e32db1", new Guid("297af0a9-060d-4ac7-b014-e421588150a0") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Tenant",
                keyColumn: "Id",
                keyValue: new Guid("297af0a9-060d-4ac7-b014-e421588150a0"));

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e336954-dab7-4f17-9e96-482328f8281c", "AQAAAAEAACcQAAAAEKscrFiRjwZJj2XHQG+JAuA2enHsLhwkwH6RmgVaUzM5qUqQ8XgWtvvwoFhWn6JTbQ==", "ce61a2e9-e418-49fe-8dd1-4a383f1cb180" });
        }
    }
}
