using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangeFeedbacksTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_AspNetUsers_OwnerId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_FeedbackEntity_FeedbackId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductEntityId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackEntity_AspNetUsers_OwnerId",
                table: "FeedbackEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackEntity_UsrProducts_ProductId",
                table: "FeedbackEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackEntity",
                table: "FeedbackEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackAnswerEntity",
                table: "FeedbackAnswerEntity");

            migrationBuilder.RenameTable(
                name: "FeedbackEntity",
                newName: "UsrFeedbacks");

            migrationBuilder.RenameTable(
                name: "FeedbackAnswerEntity",
                newName: "UsrFeedbackAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackEntity_ProductId",
                table: "UsrFeedbacks",
                newName: "IX_UsrFeedbacks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackEntity_OwnerId",
                table: "UsrFeedbacks",
                newName: "IX_UsrFeedbacks_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnswerEntity_ProductEntityId",
                table: "UsrFeedbackAnswers",
                newName: "IX_UsrFeedbackAnswers_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnswerEntity_OwnerId",
                table: "UsrFeedbackAnswers",
                newName: "IX_UsrFeedbackAnswers_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackAnswerEntity_FeedbackId",
                table: "UsrFeedbackAnswers",
                newName: "IX_UsrFeedbackAnswers_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsrFeedbacks",
                table: "UsrFeedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsrFeedbackAnswers",
                table: "UsrFeedbackAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbackAnswers_UsrFeedbacks_FeedbackId",
                table: "UsrFeedbackAnswers",
                column: "FeedbackId",
                principalTable: "UsrFeedbacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbackAnswers_UsrProducts_ProductEntityId",
                table: "UsrFeedbackAnswers",
                column: "ProductEntityId",
                principalTable: "UsrProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbackAnswers_UsrFeedbacks_FeedbackId",
                table: "UsrFeedbackAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbackAnswers_UsrProducts_ProductEntityId",
                table: "UsrFeedbackAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsrFeedbacks",
                table: "UsrFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsrFeedbackAnswers",
                table: "UsrFeedbackAnswers");

            migrationBuilder.RenameTable(
                name: "UsrFeedbacks",
                newName: "FeedbackEntity");

            migrationBuilder.RenameTable(
                name: "UsrFeedbackAnswers",
                newName: "FeedbackAnswerEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UsrFeedbacks_ProductId",
                table: "FeedbackEntity",
                newName: "IX_FeedbackEntity_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrFeedbacks_OwnerId",
                table: "FeedbackEntity",
                newName: "IX_FeedbackEntity_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrFeedbackAnswers_ProductEntityId",
                table: "FeedbackAnswerEntity",
                newName: "IX_FeedbackAnswerEntity_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrFeedbackAnswers_OwnerId",
                table: "FeedbackAnswerEntity",
                newName: "IX_FeedbackAnswerEntity_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrFeedbackAnswers_FeedbackId",
                table: "FeedbackAnswerEntity",
                newName: "IX_FeedbackAnswerEntity_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackEntity",
                table: "FeedbackEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackAnswerEntity",
                table: "FeedbackAnswerEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_AspNetUsers_OwnerId",
                table: "FeedbackAnswerEntity",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_FeedbackEntity_FeedbackId",
                table: "FeedbackAnswerEntity",
                column: "FeedbackId",
                principalTable: "FeedbackEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductEntityId",
                table: "FeedbackAnswerEntity",
                column: "ProductEntityId",
                principalTable: "UsrProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackEntity_AspNetUsers_OwnerId",
                table: "FeedbackEntity",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackEntity_UsrProducts_ProductId",
                table: "FeedbackEntity",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }
    }
}
