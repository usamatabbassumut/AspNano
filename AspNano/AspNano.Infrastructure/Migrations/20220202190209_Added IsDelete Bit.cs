using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNano.Infrastructure.Migrations
{
    public partial class AddedIsDeleteBit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Venue",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tenant",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6466930a-f562-43e0-82fb-1ec315d8f3a2", "AQAAAAEAACcQAAAAEN0LksEVMOPwc4OCs5Tr/cQnM4ECS6ICLaOeaiHW5lSVkWsQOia/kZ61EtY/fJ696A==", "09a07058-0165-438c-9157-98589c695093" });

            migrationBuilder.UpdateData(
                table: "Tenant",
                keyColumn: "Id",
                keyValue: new Guid("297af0a9-060d-4ac7-b014-e421588150a0"),
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2022, 2, 2, 19, 2, 8, 830, DateTimeKind.Utc).AddTicks(9677), new DateTime(2022, 2, 2, 19, 2, 8, 830, DateTimeKind.Utc).AddTicks(9679) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tenant");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "297af0a9-060d-4ac7-b014-e421588150a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22d61f2e-2364-4aec-b673-66cb8d31be31", "AQAAAAEAACcQAAAAEId1UB8Mfq8IMTsHvRQ56P1lgm+gBE4Ni3sJpinx3FSqGeGZETeL4Xe+VlhjLJkCnw==", "f03376fe-add5-41f1-8a83-065088e32db1" });

            migrationBuilder.UpdateData(
                table: "Tenant",
                keyColumn: "Id",
                keyValue: new Guid("297af0a9-060d-4ac7-b014-e421588150a0"),
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2022, 1, 28, 14, 56, 13, 351, DateTimeKind.Utc).AddTicks(4141), new DateTime(2022, 1, 28, 14, 56, 13, 351, DateTimeKind.Utc).AddTicks(4142) });
        }
    }
}
