using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MingYue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnydropRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "AnydropMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "AnydropAttachments",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "AnydropAttachments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "AnydropAttachments",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "AnydropAttachments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnydropLinkMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    SiteName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    FaviconUrl = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    FetchedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsFetchSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    FetchError = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnydropLinkMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnydropLinkMetadatas_AnydropMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "AnydropMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnydropMessages_MessageType",
                table: "AnydropMessages",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_AnydropAttachments_ContentType",
                table: "AnydropAttachments",
                column: "ContentType");

            migrationBuilder.CreateIndex(
                name: "IX_AnydropLinkMetadatas_MessageId",
                table: "AnydropLinkMetadatas",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnydropLinkMetadatas_Url",
                table: "AnydropLinkMetadatas",
                column: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnydropLinkMetadatas");

            migrationBuilder.DropIndex(
                name: "IX_AnydropMessages_MessageType",
                table: "AnydropMessages");

            migrationBuilder.DropIndex(
                name: "IX_AnydropAttachments_ContentType",
                table: "AnydropAttachments");

            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "AnydropMessages");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "AnydropAttachments");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AnydropAttachments");

            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "AnydropAttachments");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "AnydropAttachments");
        }
    }
}
