using Microsoft.EntityFrameworkCore.Migrations;

namespace eRevenue.Migrations
{
    public partial class YesYesYes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RevenuePlan",
                table: "RevenueTypeDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<decimal>(
                name: "RevenuePlanId",
                table: "RevenueTypeDetails",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevenuePlanId",
                table: "RevenueTypeDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "RevenuePlan",
                table: "RevenueTypeDetails",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
