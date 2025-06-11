using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mas_Project.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinNumberOfParticipants",
                table: "Quests",
                newName: "MaxNumberOfParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxNumberOfParticipants",
                table: "Quests",
                newName: "MinNumberOfParticipants");
        }
    }
}
