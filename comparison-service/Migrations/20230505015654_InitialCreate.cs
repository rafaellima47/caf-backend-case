using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comparison_service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "caf");

            migrationBuilder.CreateTable(
                name: "anonymized_faces",
                schema: "caf",
                columns: table => new
                {
                    identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    embedding = table.Column<double[]>(type: "double precision[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anonymized_faces", x => x.identifier);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anonymized_faces",
                schema: "caf");
        }
    }
}
