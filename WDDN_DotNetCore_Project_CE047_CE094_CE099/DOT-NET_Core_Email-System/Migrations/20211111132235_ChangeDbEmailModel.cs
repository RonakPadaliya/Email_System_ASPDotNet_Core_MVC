using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_Core_Email_System.Migrations
{
    public partial class ChangeDbEmailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentData",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "AttachmentName",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "AttachmentType",
                table: "Emails");

            migrationBuilder.CreateTable(
                name: "UpdateProfileViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    NewPassword = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateProfileViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdateProfileViewModel");

            migrationBuilder.AddColumn<byte[]>(
                name: "AttachmentData",
                table: "Emails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentName",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentType",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
