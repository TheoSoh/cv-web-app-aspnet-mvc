using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_ASPMVC_GROUP2.Migrations
{
    /// <inheritdoc />
    public partial class MigrationThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvCompetence_Competences_CompetenceId",
                table: "CvCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_CvCompetence_Cvs_CvId",
                table: "CvCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_CvEducation_Cvs_CvId",
                table: "CvEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_CvEducation_Educations_EducationId",
                table: "CvEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_CvExperience_Cvs_CvId",
                table: "CvExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_CvExperience_Experiences_ExperienceId",
                table: "CvExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvExperience",
                table: "CvExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvEducation",
                table: "CvEducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvCompetence",
                table: "CvCompetence");

            migrationBuilder.RenameTable(
                name: "UserProject",
                newName: "UserProjects");

            migrationBuilder.RenameTable(
                name: "CvExperience",
                newName: "CvExperiences");

            migrationBuilder.RenameTable(
                name: "CvEducation",
                newName: "CvEducations");

            migrationBuilder.RenameTable(
                name: "CvCompetence",
                newName: "CvCompetences");

            migrationBuilder.RenameIndex(
                name: "IX_UserProject_ProjectId",
                table: "UserProjects",
                newName: "IX_UserProjects_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CvExperience_ExperienceId",
                table: "CvExperiences",
                newName: "IX_CvExperiences_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_CvEducation_EducationId",
                table: "CvEducations",
                newName: "IX_CvEducations_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_CvCompetence_CompetenceId",
                table: "CvCompetences",
                newName: "IX_CvCompetences_CompetenceId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Experiences",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Educations",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competences",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvExperiences",
                table: "CvExperiences",
                columns: new[] { "CvId", "ExperienceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvEducations",
                table: "CvEducations",
                columns: new[] { "CvId", "EducationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvCompetences",
                table: "CvCompetences",
                columns: new[] { "CvId", "CompetenceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CvCompetences_Competences_CompetenceId",
                table: "CvCompetences",
                column: "CompetenceId",
                principalTable: "Competences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvCompetences_Cvs_CvId",
                table: "CvCompetences",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvEducations_Cvs_CvId",
                table: "CvEducations",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvEducations_Educations_EducationId",
                table: "CvEducations",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvExperiences_Cvs_CvId",
                table: "CvExperiences",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvExperiences_Experiences_ExperienceId",
                table: "CvExperiences",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CvCompetences_Competences_CompetenceId",
                table: "CvCompetences");

            migrationBuilder.DropForeignKey(
                name: "FK_CvCompetences_Cvs_CvId",
                table: "CvCompetences");

            migrationBuilder.DropForeignKey(
                name: "FK_CvEducations_Cvs_CvId",
                table: "CvEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CvEducations_Educations_EducationId",
                table: "CvEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CvExperiences_Cvs_CvId",
                table: "CvExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_CvExperiences_Experiences_ExperienceId",
                table: "CvExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvExperiences",
                table: "CvExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvEducations",
                table: "CvEducations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CvCompetences",
                table: "CvCompetences");

            migrationBuilder.RenameTable(
                name: "UserProjects",
                newName: "UserProject");

            migrationBuilder.RenameTable(
                name: "CvExperiences",
                newName: "CvExperience");

            migrationBuilder.RenameTable(
                name: "CvEducations",
                newName: "CvEducation");

            migrationBuilder.RenameTable(
                name: "CvCompetences",
                newName: "CvCompetence");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProject",
                newName: "IX_UserProject_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CvExperiences_ExperienceId",
                table: "CvExperience",
                newName: "IX_CvExperience_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_CvEducations_EducationId",
                table: "CvEducation",
                newName: "IX_CvEducation_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_CvCompetences_CompetenceId",
                table: "CvCompetence",
                newName: "IX_CvCompetence_CompetenceId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvExperience",
                table: "CvExperience",
                columns: new[] { "CvId", "ExperienceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvEducation",
                table: "CvEducation",
                columns: new[] { "CvId", "EducationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CvCompetence",
                table: "CvCompetence",
                columns: new[] { "CvId", "CompetenceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CvCompetence_Competences_CompetenceId",
                table: "CvCompetence",
                column: "CompetenceId",
                principalTable: "Competences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvCompetence_Cvs_CvId",
                table: "CvCompetence",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvEducation_Cvs_CvId",
                table: "CvEducation",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvEducation_Educations_EducationId",
                table: "CvEducation",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvExperience_Cvs_CvId",
                table: "CvExperience",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CvExperience_Experiences_ExperienceId",
                table: "CvExperience",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId",
                table: "UserProject",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
