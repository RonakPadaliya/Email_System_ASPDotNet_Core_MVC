using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_Core_Email_System.Migrations
{
    public partial class ChangeSignUpModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "SignUpViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "SignUpViewModel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "SignUpViewModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SignUpViewModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel",
                column: "Email");
        }
    }
}
