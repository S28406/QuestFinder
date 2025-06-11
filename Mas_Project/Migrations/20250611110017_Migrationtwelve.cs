using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mas_Project.Migrations
{
    /// <inheritdoc />
    public partial class Migrationtwelve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberRole",
                table: "GuildMembers");

            migrationBuilder.AddColumn<string>(
                name: "MemberRoles",
                table: "GuildMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberRoles",
                table: "GuildMembers");

            migrationBuilder.AddColumn<int>(
                name: "MemberRole",
                table: "GuildMembers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
