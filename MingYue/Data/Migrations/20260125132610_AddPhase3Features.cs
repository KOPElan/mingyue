using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MingYue.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhase3Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnydropMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    SenderDeviceId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SenderDeviceName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnydropMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTaskExecutionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    Output = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    ErrorMessage = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTaskExecutionHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TaskType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TaskData = table.Column<string>(type: "TEXT", nullable: false),
                    CronExpression = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastRunAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NextRunAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnydropAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnydropAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnydropAttachments_AnydropMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "AnydropMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnydropAttachments_MessageId",
                table: "AnydropAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnydropMessages_CreatedAt",
                table: "AnydropMessages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AnydropMessages_IsRead",
                table: "AnydropMessages",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedAt",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTaskExecutionHistories_StartedAt",
                table: "ScheduledTaskExecutionHistories",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTaskExecutionHistories_TaskId",
                table: "ScheduledTaskExecutionHistories",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_IsEnabled",
                table: "ScheduledTasks",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_NextRunAt",
                table: "ScheduledTasks",
                column: "NextRunAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_TaskType",
                table: "ScheduledTasks",
                column: "TaskType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnydropAttachments");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ScheduledTaskExecutionHistories");

            migrationBuilder.DropTable(
                name: "ScheduledTasks");

            migrationBuilder.DropTable(
                name: "AnydropMessages");
        }
    }
}
