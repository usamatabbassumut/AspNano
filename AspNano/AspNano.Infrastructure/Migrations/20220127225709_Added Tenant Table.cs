using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNano.Infrastructure.Migrations
{
    public partial class AddedTenantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50b9ab82-7f5b-4756-ac77-97ae912e996c", "AQAAAAEAACcQAAAAEP/FZizStRdWyyrEAU2+3itNAXlR984wDzcqV96rIbvxgdUlu6hp6A2wSv1K6ISOmA==", "86247920-09e3-4be3-b637-534bba8d03d3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10bbdeaf-430b-495d-ad1a-69eda0b99ddc", "AQAAAAEAACcQAAAAEEzhljls2N7+n3gyrJZ1RFzjK0r93qE54+Gts9UqvfeVzNu+3rFoG65vW5/bKVcFRQ==", "e797140c-037b-489a-8d63-f263d63547b1" });
        }
    }
}
