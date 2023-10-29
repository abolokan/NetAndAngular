using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donkey.DAL.Migrations
{
    public partial class CreateFileExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtensionId",
                table: "FileInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileExtensions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_ExtensionId",
                table: "FileInfos",
                column: "ExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileInfos_FileExtensions_ExtensionId",
                table: "FileInfos",
                column: "ExtensionId",
                principalTable: "FileExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileInfos_FileExtensions_ExtensionId",
                table: "FileInfos");

            migrationBuilder.DropTable(
                name: "FileExtensions");

            migrationBuilder.DropIndex(
                name: "IX_FileInfos_ExtensionId",
                table: "FileInfos");

            migrationBuilder.DropColumn(
                name: "ExtensionId",
                table: "FileInfos");
        }
    }
}
