using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PostSaveInject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostInformationId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSaves", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostInformationId",
                table: "Photos",
                column: "PostInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_PostInformations_PostInformationId",
                table: "Photos",
                column: "PostInformationId",
                principalTable: "PostInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_PostInformations_PostInformationId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "PostSaves");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PostInformationId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PostInformationId",
                table: "Photos");
        }
    }
}
