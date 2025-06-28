using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntentManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExpressionValue = table.Column<string>(type: "TEXT", nullable: true),
                    Iri = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true),
                    ExpressionType = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expressions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LifecycleStatus = table.Column<string>(type: "TEXT", nullable: true),
                    StatusChangeDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    IsBundle = table.Column<bool>(type: "INTEGER", nullable: false),
                    Context_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Context_BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    Context_SchemaLocation = table.Column<string>(type: "TEXT", nullable: true),
                    Context_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Context_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Context_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Context_CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Context_LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Context_LifecycleStatus = table.Column<string>(type: "TEXT", nullable: true),
                    Context_StatusChangeDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Context_Version = table.Column<string>(type: "TEXT", nullable: true),
                    ValidFor_StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidFor_EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpressionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true),
                    IntentType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intents_Expressions_ExpressionId",
                        column: x => x.ExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IntentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AttachmentType = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    ValidFor_StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidFor_EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true),
                    ReferredType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => new { x.IntentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Attachment_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IntentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristic", x => new { x.IntentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Characteristic_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntentRelationship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    RelationshipType = table.Column<string>(type: "TEXT", nullable: true),
                    IntentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntentRelationship_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelatedParty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IntentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Href = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseType = table.Column<string>(type: "TEXT", nullable: true),
                    SchemaLocation = table.Column<string>(type: "TEXT", nullable: true),
                    ReferredType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedParty", x => new { x.IntentId, x.Id });
                    table.ForeignKey(
                        name: "FK_RelatedParty_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntentRelationship_IntentId",
                table: "IntentRelationship",
                column: "IntentId");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_ExpressionId",
                table: "Intents",
                column: "ExpressionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Characteristic");

            migrationBuilder.DropTable(
                name: "IntentRelationship");

            migrationBuilder.DropTable(
                name: "RelatedParty");

            migrationBuilder.DropTable(
                name: "Intents");

            migrationBuilder.DropTable(
                name: "Expressions");
        }
    }
}
