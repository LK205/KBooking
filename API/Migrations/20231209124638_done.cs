using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<long>(type: "bigint", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DayOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CityOfResidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Specifically = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<long>(type: "bigint", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    RoomArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomBills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<long>(type: "bigint", nullable: true),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceTotal = table.Column<long>(type: "bigint", nullable: true),
                    AdditionalServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromBokDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToBokDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CraetionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBills", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "RoomBills");
        }
    }
}
