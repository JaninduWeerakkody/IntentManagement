using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntentManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class TMF921FullRestore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntentSpecificationId",
                table: "Intents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IntentReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidFor_StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidFor_EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpressionId = table.Column<int>(type: "INTEGER", nullable: true),
                    IntentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntentReports_Expressions_ExpressionId",
                        column: x => x.ExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IntentReports_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IntentSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<string>(type: "TEXT", nullable: true),
                    ValidForFrom = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidForTo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LifecycleStatus = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentSpecifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intents_IntentSpecificationId",
                table: "Intents",
                column: "IntentSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentReports_ExpressionId",
                table: "IntentReports",
                column: "ExpressionId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentReports_IntentId",
                table: "IntentReports",
                column: "IntentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_IntentSpecifications_IntentSpecificationId",
                table: "Intents",
                column: "IntentSpecificationId",
                principalTable: "IntentSpecifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intents_IntentSpecifications_IntentSpecificationId",
                table: "Intents");

            migrationBuilder.DropTable(
                name: "IntentReports");

            migrationBuilder.DropTable(
                name: "IntentSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Intents_IntentSpecificationId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "IntentSpecificationId",
                table: "Intents");
        }
    }
}
