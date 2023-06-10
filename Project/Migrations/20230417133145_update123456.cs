using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class update123456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    MinTemp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTemp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Leakage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evaporation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Electricity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter1Dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter1Speed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter2_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter2_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter3_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter3_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter4_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter4_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter5_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter5_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter6_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter6_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter7_9am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter7_3pm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Failure_today = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RISK_MM = table.Column<double>(type: "float", nullable: false),
                    Fail_tomorrow = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predictives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UDI = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirTemperature = table.Column<float>(type: "real", nullable: false),
                    ProcessTemperature = table.Column<float>(type: "real", nullable: false),
                    RotationalSpeed = table.Column<float>(type: "real", nullable: false),
                    Torque = table.Column<float>(type: "real", nullable: false),
                    ToolWear = table.Column<float>(type: "real", nullable: false),
                    Target = table.Column<float>(type: "real", nullable: false),
                    FailureType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictives", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Predictives");
        }
    }
}
