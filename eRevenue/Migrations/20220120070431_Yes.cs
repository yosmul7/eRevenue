using Microsoft.EntityFrameworkCore.Migrations;

namespace eRevenue.Migrations
{
    public partial class Yes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationLevels",
                columns: table => new
                {
                    OrganizationLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationLevelNameAmh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationLevels", x => x.OrganizationLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    YearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.YearId);
                });

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    CenterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterNameAmh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.CenterId);
                    table.ForeignKey(
                        name: "FK_Centers_OrganizationLevels_OrganizationLevelId",
                        column: x => x.OrganizationLevelId,
                        principalTable: "OrganizationLevels",
                        principalColumn: "OrganizationLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RevenuePlans",
                columns: table => new
                {
                    RevenuePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    OrganizationLevelId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    RevenuePlanAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RevenuePlanDirect = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RevenuePlanIndirect = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RevenuePlanMunicipality = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenuePlans", x => x.RevenuePlanId);
                    table.ForeignKey(
                        name: "FK_RevenuePlan_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevenuePlan_OrganizationLevelId",
                        column: x => x.OrganizationLevelId,
                        principalTable: "OrganizationLevels",
                        principalColumn: "OrganizationLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevenuePlan_Year",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "YearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RevenueTypeDetails",
                columns: table => new
                {
                    RevenueTypeDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevenueCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevenueTypeDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevenueType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevenuePlan = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    OrganizationLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenueTypeDetails", x => x.RevenueTypeDetailId);
                    table.ForeignKey(
                        name: "FK_RevenueTypeDetails_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "CenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevenueTypeDetails_OrganizationLevels_OrganizationLevelId",
                        column: x => x.OrganizationLevelId,
                        principalTable: "OrganizationLevels",
                        principalColumn: "OrganizationLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Centers_OrganizationLevelId",
                table: "Centers",
                column: "OrganizationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenuePlans_CenterId",
                table: "RevenuePlans",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenuePlans_OrganizationLevelId",
                table: "RevenuePlans",
                column: "OrganizationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenuePlans_YearId",
                table: "RevenuePlans",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueTypeDetails_CenterId",
                table: "RevenueTypeDetails",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueTypeDetails_OrganizationLevelId",
                table: "RevenueTypeDetails",
                column: "OrganizationLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevenuePlans");

            migrationBuilder.DropTable(
                name: "RevenueTypeDetails");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropTable(
                name: "OrganizationLevels");
        }
    }
}
