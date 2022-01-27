using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNano.Infrastructure.Migrations
{
    public partial class AddedTableVenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueType = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venue_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e336954-dab7-4f17-9e96-482328f8281c", "AQAAAAEAACcQAAAAEKscrFiRjwZJj2XHQG+JAuA2enHsLhwkwH6RmgVaUzM5qUqQ8XgWtvvwoFhWn6JTbQ==", "ce61a2e9-e418-49fe-8dd1-4a383f1cb180" });

            migrationBuilder.CreateIndex(
                name: "IX_Venue_TenantId",
                table: "Venue",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50b9ab82-7f5b-4756-ac77-97ae912e996c", "AQAAAAEAACcQAAAAEP/FZizStRdWyyrEAU2+3itNAXlR984wDzcqV96rIbvxgdUlu6hp6A2wSv1K6ISOmA==", "86247920-09e3-4be3-b637-534bba8d03d3" });
        }
    }
}
