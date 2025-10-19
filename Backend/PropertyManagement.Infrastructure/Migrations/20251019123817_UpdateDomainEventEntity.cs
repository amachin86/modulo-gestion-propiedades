using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDomainEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventData",
                table: "DomainEvents");

            migrationBuilder.DropColumn(
                name: "OccurredAt",
                table: "DomainEvents");

            migrationBuilder.AddColumn<string>(
                name: "PayloadJSON",
                table: "DomainEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayloadJSON",
                table: "DomainEvents");

            migrationBuilder.AddColumn<string>(
                name: "EventData",
                table: "DomainEvents",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OccurredAt",
                table: "DomainEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
