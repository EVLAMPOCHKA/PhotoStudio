using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvlampochkaPhotoStudio.Migrations
{
    public partial class aaappp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedDates_Room_RoomId",
                table: "BookedDates");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "BookedDates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedDates_Room_RoomId",
                table: "BookedDates",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedDates_Room_RoomId",
                table: "BookedDates");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "BookedDates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedDates_Room_RoomId",
                table: "BookedDates",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
