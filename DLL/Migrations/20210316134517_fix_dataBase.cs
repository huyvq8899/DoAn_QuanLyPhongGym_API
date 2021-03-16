using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class fix_dataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardTypies_CardTypeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Function_Roles_Users_UserId",
                table: "Function_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardTypies",
                table: "CardTypies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Functions");

            migrationBuilder.RenameTable(
                name: "CardTypies",
                newName: "CardTypes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardTypes",
                table: "CardTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardTypes_CardTypeId",
                table: "Cards",
                column: "CardTypeId",
                principalTable: "CardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Function_Roles_Users_UserId",
                table: "Function_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardTypes_CardTypeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Function_Roles_Users_UserId",
                table: "Function_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardTypes",
                table: "CardTypes");

            migrationBuilder.RenameTable(
                name: "CardTypes",
                newName: "CardTypies");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Functions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardTypies",
                table: "CardTypies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardTypies_CardTypeId",
                table: "Cards",
                column: "CardTypeId",
                principalTable: "CardTypies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Function_Roles_Users_UserId",
                table: "Function_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
