using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gwt.Api.Migrations
{
    public partial class FirstSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName" },
                values: new object[,]
                {
                    { new Guid("837d3ad1-ecad-441d-8f99-39843cf4f574"), "Johnny" },
                    { new Guid("852ed8fa-3f1c-457a-9136-8dcf31d8713e"), "Carl" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("837d3ad1-ecad-441d-8f99-39843cf4f574"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("852ed8fa-3f1c-457a-9136-8dcf31d8713e"));
        }
    }
}
