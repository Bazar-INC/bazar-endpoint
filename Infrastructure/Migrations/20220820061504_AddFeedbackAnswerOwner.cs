using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddFeedbackAnswerOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "UsrFeedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "UsrFeedbackAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "UsrFeedbacks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "UsrFeedbackAnswers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbackAnswers_AspNetUsers_OwnerId",
                table: "UsrFeedbackAnswers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrFeedbacks_AspNetUsers_OwnerId",
                table: "UsrFeedbacks",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
