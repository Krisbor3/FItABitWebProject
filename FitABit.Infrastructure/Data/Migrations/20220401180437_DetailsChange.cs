using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitABit.Infrastructure.Data.Migrations
{
    public partial class DetailsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Details_DetailId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_DetailId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Details",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Details_ExerciseId",
                table: "Details",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Exercises_ExerciseId",
                table: "Details",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Exercises_ExerciseId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_ExerciseId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Details");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_DetailId",
                table: "Exercises",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Details_DetailId",
                table: "Exercises",
                column: "DetailId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
