using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_Core_Email_System.Migrations
{
    public partial class BactToDefaultDbEmailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AttachmentData",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentName",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentType",
                table: "Emails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
