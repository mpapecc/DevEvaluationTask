using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Api.Migrations
{
    public partial class altercontacttagtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTags",
                table: "ContactTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTags",
                table: "ContactTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTags_ContactId",
                table: "ContactTags",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTags",
                table: "ContactTags");

            migrationBuilder.DropIndex(
                name: "IX_ContactTags_ContactId",
                table: "ContactTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTags",
                table: "ContactTags",
                columns: new[] { "ContactId", "TagId" });
        }
    }
}
