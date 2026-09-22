using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sat_ground_station.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    satelliteId = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    severity = table.Column<string>(type: "text", nullable: false),
                    velocityLevel = table.Column<string>(type: "text", nullable: true),
                    temperatureLevel = table.Column<string>(type: "text", nullable: true),
                    batteryLevel = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Telemetry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    satalliteId = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    altitude = table.Column<int>(type: "integer", nullable: false),
                    velocity = table.Column<int>(type: "integer", nullable: false),
                    temperature = table.Column<int>(type: "integer", nullable: false),
                    batteryLevel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Satellite",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    satelliteId = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    missionId = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellite", x => x.id);
                    table.ForeignKey(
                        name: "FK_Satellite_Mission_missionId",
                        column: x => x.missionId,
                        principalTable: "Mission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Mission",
                columns: new[] { "id", "description", "name", "status" },
                values: new object[,]
                {
                    { 1, "Exploration mission", "Explorer", "Active" },
                    { 2, "Weather monitoring mission", "Weather", "Active" },
                    { 3, "Communication mission", "Communication", "Active" },
                    { 4, "Navigation mission", "Navigation", "Active" },
                    { 5, "Scientific research mission", "Science", "Active" },
                    { 6, "Earth imaging mission", "Imaging", "Active" },
                    { 7, "Tracking mission", "Tracking", "Active" },
                    { 8, "Research mission", "Research", "Active" },
                    { 9, "Monitoring mission", "Defense", "Active" },
                    { 10, "Technology demonstration mission", "Technology", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Satellite",
                columns: new[] { "id", "Country", "missionId", "satelliteId", "status" },
                values: new object[,]
                {
                    { 1, "US", 1, "SAT-001-Explorer", "Active" },
                    { 2, "US", 1, "SAT-002-Explorer", "Active" },
                    { 3, "US", 2, "SAT-003-Weather", "Active" },
                    { 4, "US", 2, "SAT-004-Weather", "Active" },
                    { 5, "US", 3, "SAT-005-Communication", "Active" },
                    { 6, "US", 3, "SAT-006-Communication", "Active" },
                    { 7, "US", 4, "SAT-007-Navigation", "Active" },
                    { 8, "US", 4, "SAT-008-Navigation", "Active" },
                    { 9, "US", 5, "SAT-009-Science", "Active" },
                    { 10, "US", 5, "SAT-010-Science", "Active" },
                    { 11, "US", 6, "SAT-011-Imaging", "Active" },
                    { 12, "US", 6, "SAT-012-Imaging", "Active" },
                    { 13, "US", 7, "SAT-013-Tracking", "Active" },
                    { 14, "US", 7, "SAT-014-Tracking", "Active" },
                    { 15, "US", 8, "SAT-015-Research", "Active" },
                    { 16, "US", 8, "SAT-016-Research", "Active" },
                    { 17, "US", 9, "SAT-017-Defense", "Active" },
                    { 18, "US", 9, "SAT-018-Defense", "Active" },
                    { 19, "US", 10, "SAT-019-Technology", "Active" },
                    { 20, "US", 10, "SAT-020-Technology", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satellite_missionId",
                table: "Satellite",
                column: "missionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "Satellite");

            migrationBuilder.DropTable(
                name: "Telemetry");

            migrationBuilder.DropTable(
                name: "Mission");
        }
    }
}
