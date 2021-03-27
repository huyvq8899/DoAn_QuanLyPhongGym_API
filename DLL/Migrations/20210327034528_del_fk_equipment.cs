using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class del_fk_equipment : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
