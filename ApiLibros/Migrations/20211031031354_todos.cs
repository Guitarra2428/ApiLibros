using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiLibros.Migrations
{
    public partial class todos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autors_autorID",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_autorID",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "autorID",
                table: "Libros");

            migrationBuilder.AddColumn<int>(
                name: "libroID",
                table: "Autors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Autors_libroID",
                table: "Autors",
                column: "libroID");

            migrationBuilder.AddForeignKey(
                name: "FK_Autors_Libros_libroID",
                table: "Autors",
                column: "libroID",
                principalTable: "Libros",
                principalColumn: "LibtoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autors_Libros_libroID",
                table: "Autors");

            migrationBuilder.DropIndex(
                name: "IX_Autors_libroID",
                table: "Autors");

            migrationBuilder.DropColumn(
                name: "libroID",
                table: "Autors");

            migrationBuilder.AddColumn<int>(
                name: "autorID",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_autorID",
                table: "Libros",
                column: "autorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autors_autorID",
                table: "Libros",
                column: "autorID",
                principalTable: "Autors",
                principalColumn: "AutorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
