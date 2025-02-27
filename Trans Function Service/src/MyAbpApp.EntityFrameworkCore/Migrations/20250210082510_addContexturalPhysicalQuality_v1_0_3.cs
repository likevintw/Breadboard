﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAbpApp.Migrations
{
    /// <inheritdoc />
    public partial class addContexturalPhysicalQuality_v1_0_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "ContexturalPhysicalQualities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "ContexturalPhysicalQualities");
        }
    }
}
