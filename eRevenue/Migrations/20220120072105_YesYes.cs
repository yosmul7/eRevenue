using Microsoft.EntityFrameworkCore.Migrations;

namespace eRevenue.Migrations
{
    public partial class YesYes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevenueTypeDetails_Centers_CenterId",
                table: "RevenueTypeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RevenueTypeDetails_OrganizationLevels_OrganizationLevelId",
                table: "RevenueTypeDetails");

            migrationBuilder.DropIndex(
                name: "IX_RevenueTypeDetails_CenterId",
                table: "RevenueTypeDetails");

            migrationBuilder.DropIndex(
                name: "IX_RevenueTypeDetails_OrganizationLevelId",
                table: "RevenueTypeDetails");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "RevenueTypeDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationLevelId",
                table: "RevenueTypeDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "RevenueTypeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationLevelId",
                table: "RevenueTypeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RevenueTypeDetails_CenterId",
                table: "RevenueTypeDetails",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueTypeDetails_OrganizationLevelId",
                table: "RevenueTypeDetails",
                column: "OrganizationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RevenueTypeDetails_Centers_CenterId",
                table: "RevenueTypeDetails",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RevenueTypeDetails_OrganizationLevels_OrganizationLevelId",
                table: "RevenueTypeDetails",
                column: "OrganizationLevelId",
                principalTable: "OrganizationLevels",
                principalColumn: "OrganizationLevelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
