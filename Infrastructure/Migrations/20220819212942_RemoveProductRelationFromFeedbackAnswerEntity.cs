using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RemoveProductRelationFromFeedbackAnswerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "FeedbackAnswerEntity",
                newName: "ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnswerEntity_ProductId",
                table: "FeedbackAnswerEntity",
                newName: "IX_FeedbackAnswerEntity_ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductEntityId",
                table: "FeedbackAnswerEntity",
                column: "ProductEntityId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductEntityId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.RenameColumn(
                name: "ProductEntityId",
                table: "FeedbackAnswerEntity",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnswerEntity_ProductEntityId",
                table: "FeedbackAnswerEntity",
                newName: "IX_FeedbackAnswerEntity_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductId",
                table: "FeedbackAnswerEntity",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }
    }
}
