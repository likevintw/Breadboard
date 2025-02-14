using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAbpApp.Migrations
{
    /// <inheritdoc />
    public partial class addContexturalPhysicalQuality_v1_0_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "ContexturalPhysicalQualities",
                newName: "SensorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "ContexturalPhysicalQualities",
                newName: "DeviceId");
        }
    }
}
