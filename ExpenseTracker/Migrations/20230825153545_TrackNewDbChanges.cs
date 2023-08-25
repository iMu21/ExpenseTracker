using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class TrackNewDbChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExpenseId",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ExpenseId",
                table: "Category",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Expense_ExpenseId",
                table: "Category",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Expense_ExpenseId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ExpenseId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Category");
        }
    }
}
