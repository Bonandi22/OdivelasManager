﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IcmOdivelas.Migrations
{
    /// <inheritdoc />
    public partial class IcmOdivelas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FunctionId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionId",
                table: "Members");
        }
    }
}
