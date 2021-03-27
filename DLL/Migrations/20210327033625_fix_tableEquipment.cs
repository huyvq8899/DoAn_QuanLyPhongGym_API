using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class fix_tableEquipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_UserId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Equipments");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Equipments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_CreatedBy",
                table: "Equipments",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Users_CreatedBy",
                table: "Equipments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Users_CreatedBy",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_CreatedBy",
                table: "Equipments");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Equipments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Equipments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_UserId",
                table: "Equipments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Users_UserId",
                table: "Equipments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
