using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LedgerLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "Clients",
                newName: "Rfc");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Clients",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Rfc",
                table: "Clients",
                newName: "TaxId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "CompanyName");
        }
    }
}
