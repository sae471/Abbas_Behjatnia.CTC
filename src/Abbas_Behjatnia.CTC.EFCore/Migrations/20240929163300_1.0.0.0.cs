using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abbas_Behjatnia.CTC.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _1000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleCategories_VehicleCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OwnerShipType = table.Column<int>(type: "int", nullable: false),
                    PlateNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    ChassisNumber = table.Column<string>(type: "varchar(200)", nullable: false),
                    ManufacturerCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearofManufacture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleCategories_VehicleCategoryId",
                        column: x => x.VehicleCategoryId,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleCategories_ParentId",
                table: "VehicleCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCategoryId",
                table: "Vehicles",
                column: "VehicleCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}
