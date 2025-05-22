using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TickTockTrends_WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "User",
                newName: "phoneno");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_role_id");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Role",
                newName: "role_name");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Role",
                newName: "role_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "phoneno",
                table: "User",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "role_name",
                table: "Role",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK__User__B9BE370FD62FF6A0",
                table: "User",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Role__760965CC0A99C65A",
                table: "Role",
                column: "role_id");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__2EF52A2767FD9772", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK__Cart__user_id__49C3F6B7",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    phone = table.Column<double>(type: "float", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__46596229D87C43B0", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Order__user_id__5070F446",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    review_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__60883D90D4DE854C", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Review__product___5EBF139D",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK__Review__user_id__5DCAEF64",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    wishlist_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    wishlist_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Wishlist__6151514E7DDDBF79", x => x.wishlist_id);
                    table.ForeignKey(
                        name: "FK__Wishlist__produc__5AEE82B9",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK__Wishlist__user_i__59FA5E80",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    cartitem_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cart_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__66AF91ADEDF6A26C", x => x.cartitem_id);
                    table.ForeignKey(
                        name: "FK__CartItem__cart_i__4CA06362",
                        column: x => x.cart_id,
                        principalTable: "Cart",
                        principalColumn: "cart_id");
                    table.ForeignKey(
                        name: "FK__CartItem__produc__4D94879B",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderitem_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__A0B0C5903659C8B8", x => x.orderitem_id);
                    table.ForeignKey(
                        name: "FK__OrderItem__order__534D60F1",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__OrderItem__produ__5441852A",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    transaction_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    payment_status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__ED1FC9EA521A1E36", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK__Payment__order_i__571DF1D5",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    shipping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    shipping_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    shipped_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    shipping_status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__059B15A93F057252", x => x.shipping_id);
                    table.ForeignKey(
                        name: "FK__Shipping__order___619B8048",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_user_id",
                table: "Cart",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_cart_id",
                table: "CartItem",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_product_id",
                table: "CartItem",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_product_id",
                table: "OrderItems",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_order_id",
                table: "Payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_product_id",
                table: "Review",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_user_id",
                table: "Review",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_order_id",
                table: "Shipping",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_product_id",
                table: "Wishlist",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_user_id",
                table: "Wishlist",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__User__role_id__398D8EEE",
                table: "User",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User__role_id__398D8EEE",
                table: "User");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK__User__B9BE370FD62FF6A0",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Role__760965CC0A99C65A",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "phoneno",
                table: "Users",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_role_id",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "role_name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
