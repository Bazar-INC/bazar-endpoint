using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddFeedbackIsEditedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "UsrFeedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "UsrFeedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "UsrFeedbackAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "UsrFeedbacks");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "UsrFeedbackAnswers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "UsrFeedbacks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_UsrProducts_ProductId",
                table: "UsrFeedbacks",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id");
        }
    }
}
