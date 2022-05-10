using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBookStore.MvcApp.Migrations
{
    public partial class ExtendedOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "Orders",
                newName: "DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                newName: "IX_Orders_DeliveryTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Middlename",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartLine_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_BookId",
                table: "CartLine",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_OrderId",
                table: "CartLine",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTypes_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTypes_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Middlename",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryTypeId",
                table: "Orders",
                newName: "OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders",
                newName: "IX_Orders_OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
