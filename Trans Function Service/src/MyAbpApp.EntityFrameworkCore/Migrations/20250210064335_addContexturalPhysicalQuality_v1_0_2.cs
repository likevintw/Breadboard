using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAbpApp.Migrations
{
    /// <inheritdoc />
    public partial class addContexturalPhysicalQuality_v1_0_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "process",
                table: "ContexturalPhysicalQualities",
                newName: "Process");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Process",
                table: "ContexturalPhysicalQualities",
                newName: "process");
        }
    }
}
