﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test.dataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ID");
        }
    }
}
