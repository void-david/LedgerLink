using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LedgerLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnrichClientProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressZip",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxRegime",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressZip",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TaxRegime",
                table: "Clients");
        }
    }
}
