using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddProductToFeedbacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "FeedbackEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "FeedbackAnswerEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackEntity_ProductId",
                table: "FeedbackEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackAnswerEntity_ProductId",
                table: "FeedbackAnswerEntity",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductId",
                table: "FeedbackAnswerEntity",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackEntity_UsrProducts_ProductId",
                table: "FeedbackEntity",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackAnswerEntity_UsrProducts_ProductId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackEntity_UsrProducts_ProductId",
                table: "FeedbackEntity");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackEntity_ProductId",
                table: "FeedbackEntity");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackAnswerEntity_ProductId",
                table: "FeedbackAnswerEntity");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FeedbackEntity");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FeedbackAnswerEntity");
        }
    }
}
