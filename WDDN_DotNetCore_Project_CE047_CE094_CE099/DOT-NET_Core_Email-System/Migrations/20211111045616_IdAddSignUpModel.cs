using Microsoft.EntityFrameworkCore.Migrations;

namespace DOT_NET_Core_Email_System.Migrations
{
    public partial class IdAddSignUpModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "SignUpViewModel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SignUpViewModel",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SignUpViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "SignUpViewModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUpViewModel",
                table: "SignUpViewModel",
                column: "UserName");
        }
    }
}
