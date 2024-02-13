using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomFacilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facility_Room_RoomId",
                table: "Facility");

            migrationBuilder.DropIndex(
                name: "IX_Facility_RoomId",
                table: "Facility");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Facility");

            migrationBuilder.CreateTable(
                name: "RoomFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    FacilityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomFacility_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomFacility_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacility_FacilityId",
                table: "RoomFacility",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacility_RoomId",
                table: "RoomFacility",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomFacility");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Facility",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facility_RoomId",
                table: "Facility",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facility_Room_RoomId",
                table: "Facility",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
