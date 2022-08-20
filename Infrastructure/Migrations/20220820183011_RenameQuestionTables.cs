using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RenameQuestionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerEntity_AspNetUsers_OwnerId",
                table: "QuestionAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerEntity_QuestionEntity_QuestionId",
                table: "QuestionAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerEntity_UsrProducts_ProductEntityId",
                table: "QuestionAnswerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionEntity_AspNetUsers_OwnerId",
                table: "QuestionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionEntity_UsrProducts_ProductId",
                table: "QuestionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionEntity",
                table: "QuestionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerEntity",
                table: "QuestionAnswerEntity");

            migrationBuilder.RenameTable(
                name: "QuestionEntity",
                newName: "UsrQuestions");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerEntity",
                newName: "UsrQuestionAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionEntity_ProductId",
                table: "UsrQuestions",
                newName: "IX_UsrQuestions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionEntity_OwnerId",
                table: "UsrQuestions",
                newName: "IX_UsrQuestions_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerEntity_QuestionId",
                table: "UsrQuestionAnswers",
                newName: "IX_UsrQuestionAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerEntity_ProductEntityId",
                table: "UsrQuestionAnswers",
                newName: "IX_UsrQuestionAnswers_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerEntity_OwnerId",
                table: "UsrQuestionAnswers",
                newName: "IX_UsrQuestionAnswers_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsrQuestions",
                table: "UsrQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsrQuestionAnswers",
                table: "UsrQuestionAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrQuestionAnswers_AspNetUsers_OwnerId",
                table: "UsrQuestionAnswers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrQuestionAnswers_UsrProducts_ProductEntityId",
                table: "UsrQuestionAnswers",
                column: "ProductEntityId",
                principalTable: "UsrProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrQuestionAnswers_UsrQuestions_QuestionId",
                table: "UsrQuestionAnswers",
                column: "QuestionId",
                principalTable: "UsrQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsrQuestions_AspNetUsers_OwnerId",
                table: "UsrQuestions",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrQuestions_UsrProducts_ProductId",
                table: "UsrQuestions",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsrQuestionAnswers_AspNetUsers_OwnerId",
                table: "UsrQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrQuestionAnswers_UsrProducts_ProductEntityId",
                table: "UsrQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrQuestionAnswers_UsrQuestions_QuestionId",
                table: "UsrQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrQuestions_AspNetUsers_OwnerId",
                table: "UsrQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrQuestions_UsrProducts_ProductId",
                table: "UsrQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsrQuestions",
                table: "UsrQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsrQuestionAnswers",
                table: "UsrQuestionAnswers");

            migrationBuilder.RenameTable(
                name: "UsrQuestions",
                newName: "QuestionEntity");

            migrationBuilder.RenameTable(
                name: "UsrQuestionAnswers",
                newName: "QuestionAnswerEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UsrQuestions_ProductId",
                table: "QuestionEntity",
                newName: "IX_QuestionEntity_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrQuestions_OwnerId",
                table: "QuestionEntity",
                newName: "IX_QuestionEntity_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrQuestionAnswers_QuestionId",
                table: "QuestionAnswerEntity",
                newName: "IX_QuestionAnswerEntity_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrQuestionAnswers_ProductEntityId",
                table: "QuestionAnswerEntity",
                newName: "IX_QuestionAnswerEntity_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_UsrQuestionAnswers_OwnerId",
                table: "QuestionAnswerEntity",
                newName: "IX_QuestionAnswerEntity_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionEntity",
                table: "QuestionEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerEntity",
                table: "QuestionAnswerEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerEntity_AspNetUsers_OwnerId",
                table: "QuestionAnswerEntity",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerEntity_QuestionEntity_QuestionId",
                table: "QuestionAnswerEntity",
                column: "QuestionId",
                principalTable: "QuestionEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerEntity_UsrProducts_ProductEntityId",
                table: "QuestionAnswerEntity",
                column: "ProductEntityId",
                principalTable: "UsrProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionEntity_AspNetUsers_OwnerId",
                table: "QuestionEntity",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionEntity_UsrProducts_ProductId",
                table: "QuestionEntity",
                column: "ProductId",
                principalTable: "UsrProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
