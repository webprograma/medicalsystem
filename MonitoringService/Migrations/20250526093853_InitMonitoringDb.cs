using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringService.Migrations
{
    /// <inheritdoc />
    public partial class InitMonitoringDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitoringRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringRecords");
        }
    }
}
