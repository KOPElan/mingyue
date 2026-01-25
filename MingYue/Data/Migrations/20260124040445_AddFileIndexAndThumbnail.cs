using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MingYue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFileIndexAndThumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileIndexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IndexedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileIndexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    ThumbnailData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileIndexes_FilePath",
                table: "FileIndexes",
                column: "FilePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileIndexes_IndexedAt",
                table: "FileIndexes",
                column: "IndexedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_FilePath",
                table: "Thumbnails",
                column: "FilePath",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileIndexes");

            migrationBuilder.DropTable(
                name: "Thumbnails");
        }
    }
}
