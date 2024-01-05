using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASPMVC_GROUP2.Migrations
{
    /// <inheritdoc />
    public partial class MigrationFour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_AspNetUsers_User_ID",
                table: "Cvs");

            migrationBuilder.DropIndex(
                name: "IX_Cvs_User_ID",
                table: "Cvs");

            migrationBuilder.AlterColumn<string>(
                name: "User_ID",
                table: "Cvs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_User_ID",
                table: "Cvs",
                column: "User_ID",
                unique: true,
                filter: "[User_ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_AspNetUsers_User_ID",
                table: "Cvs",
                column: "User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_AspNetUsers_User_ID",
                table: "Cvs");

            migrationBuilder.DropIndex(
                name: "IX_Cvs_User_ID",
                table: "Cvs");

            migrationBuilder.AlterColumn<string>(
                name: "User_ID",
                table: "Cvs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_User_ID",
                table: "Cvs",
                column: "User_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_AspNetUsers_User_ID",
                table: "Cvs",
                column: "User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
