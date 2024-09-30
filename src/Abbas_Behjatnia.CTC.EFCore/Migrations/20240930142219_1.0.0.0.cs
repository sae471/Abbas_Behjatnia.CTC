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
                name: "CountryDivisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryDivisions_CountryDivisions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CountryDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "TollStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TollStations_CountryDivisions_CityId",
                        column: x => x.CityId,
                        principalTable: "CountryDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TollStations_CountryDivisions_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "CountryDivisions",
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
                    YearofManufacture = table.Column<short>(type: "smallint", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TaxExempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", nullable: false),
                    IsExempt = table.Column<bool>(type: "bit", nullable: false),
                    ValueIsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayofWeek = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<short>(type: "smallint", nullable: false),
                    Week = table.Column<byte>(type: "tinyint", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TollStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxExempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxExempts_CountryDivisions_CityId",
                        column: x => x.CityId,
                        principalTable: "CountryDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxExempts_CountryDivisions_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "CountryDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxExempts_TollStations_TollStationId",
                        column: x => x.TollStationId,
                        principalTable: "TollStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxExempts_VehicleCategories_VehicleCategoryId",
                        column: x => x.VehicleCategoryId,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Traffic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TollStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traffic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traffic_TollStations_TollStationId",
                        column: x => x.TollStationId,
                        principalTable: "TollStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traffic_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryDivisions_ParentId",
                table: "CountryDivisions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxExempts_CityId",
                table: "TaxExempts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxExempts_ProvinceId",
                table: "TaxExempts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxExempts_TollStationId",
                table: "TaxExempts",
                column: "TollStationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxExempts_VehicleCategoryId",
                table: "TaxExempts",
                column: "VehicleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TollStations_CityId",
                table: "TollStations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TollStations_ProvinceId",
                table: "TollStations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Traffic_TollStationId",
                table: "Traffic",
                column: "TollStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Traffic_VehicleId",
                table: "Traffic",
                column: "VehicleId");

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
                name: "TaxExempts");

            migrationBuilder.DropTable(
                name: "Traffic");

            migrationBuilder.DropTable(
                name: "TollStations");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "CountryDivisions");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}
