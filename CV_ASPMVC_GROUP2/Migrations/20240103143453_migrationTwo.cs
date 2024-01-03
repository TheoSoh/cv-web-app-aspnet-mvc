using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASPMVC_GROUP2.Migrations
{
    /// <inheritdoc />
    public partial class migrationTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromAnonymousName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAnonymousName",
                table: "Messages");
        }
    }
}
