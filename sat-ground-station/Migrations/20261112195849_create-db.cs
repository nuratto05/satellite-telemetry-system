using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sat_ground_station.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
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
                    satelliteName = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    severity = table.Column<string>(type: "text", nullable: false),
                    velocityLevel = table.Column<string>(type: "text", nullable: true),
                    temperatureLevel = table.Column<string>(type: "text", nullable: true),
                    batteryLevel = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string[]>(type: "text[]", nullable: false)
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
                    satalliteId = table.Column<long>(type: "bigint", nullable: false),
                    satalliteName = table.Column<string>(type: "text", nullable: false),
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
                    satelliteId = table.Column<long>(type: "bigint", nullable: false),
                    satelliteName = table.Column<string>(type: "text", nullable: false),
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
                columns: new[] { "id", "Country", "missionId", "satelliteId", "satelliteName", "status" },
                values: new object[,]
                {
                    { 1, "US", 1, 7854376908376L, "SAT-100-EXPLORER", "Active" },
                    { 2, "US", 1, 7854376908377L, "SAT-101-EXPLORER", "Active" },
                    { 3, "US", 2, 7854376908378L, "SAT-102-WEATHER", "Active" },
                    { 4, "US", 2, 7854376908379L, "SAT-103-WEATHER", "Active" },
                    { 5, "US", 3, 7854376908380L, "SAT-104-COMMUNICATION", "Active" },
                    { 6, "US", 3, 7854376908381L, "SAT-105-COMMUNICATION", "Active" },
                    { 7, "US", 4, 7854376908382L, "SAT-106-NAVIGATION", "Active" },
                    { 8, "US", 4, 7854376908383L, "SAT-107-NAVIGATION", "Active" },
                    { 9, "US", 5, 7854376908384L, "SAT-108-SCIENCE", "Active" },
                    { 10, "US", 5, 7854376908385L, "SAT-109-SCIENCE", "Active" },
                    { 11, "US", 6, 7854376908386L, "SAT-110-IMAGING", "Active" },
                    { 12, "US", 6, 7854376908387L, "SAT-111-IMAGING", "Active" },
                    { 13, "US", 7, 7854376908388L, "SAT-112-TRACKING", "Active" },
                    { 14, "US", 7, 7854376908389L, "SAT-113-TRACKING", "Active" },
                    { 15, "US", 8, 7854376908390L, "SAT-114-RESEARCH", "Active" },
                    { 16, "US", 8, 7854376908391L, "SAT-115-RESEARCH", "Active" },
                    { 17, "US", 9, 7854376908392L, "SAT-116-DEFENSE", "Active" },
                    { 18, "US", 9, 7854376908393L, "SAT-117-DEFENSE", "Active" },
                    { 19, "US", 10, 7854376908394L, "SAT-118-TECHNOLOGY", "Active" },
                    { 20, "US", 10, 7854376908395L, "SAT-119-TECHNOLOGY", "Active" }
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
