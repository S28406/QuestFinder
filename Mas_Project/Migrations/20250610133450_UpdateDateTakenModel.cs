using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mas_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTakenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedEndDate",
                table: "Quests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedEndDate",
                table: "Quests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
