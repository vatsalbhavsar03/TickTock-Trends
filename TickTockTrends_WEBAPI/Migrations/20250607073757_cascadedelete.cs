using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TickTockTrends_WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class cascadedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Brand__category___3E52440B",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK__Cart__user_id__49C3F6B7",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__CartItem__cart_i__4CA06362",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK__CartItem__produc__4D94879B",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK__Order__user_id__5070F446",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderItem__order__534D60F1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderItem__produ__5441852A",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK__Payment__order_i__571DF1D5",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__brand_i__4222D4EF",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__categor__412EB0B6",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__Review__product___5EBF139D",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK__Review__user_id__5DCAEF64",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK__User__role_id__398D8EEE",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK__Wishlist__produc__5AEE82B9",
                table: "Wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK__Wishlist__user_i__59FA5E80",
                table: "Wishlist");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderIte__A0B0C5903659C8B8",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Wishlist__6151514E7DDDBF79",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK__User__B9BE370FD62FF6A0",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Role__760965CC0A99C65A",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Review__60883D90D4DE854C",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Product__47027DF5E1A55C44",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Payment__ED1FC9EA521A1E36",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Order__46596229D87C43B0",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Category__D54EE9B431E0794C",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CartItem__66AF91ADEDF6A26C",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cart__2EF52A2767FD9772",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Brand__5E5A8E2767B9AAC5",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Wishlist",
                newName: "Wishlists");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "orderitem_id",
                table: "OrderItems",
                newName: "OrderitemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_product_id",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameColumn(
                name: "wishlist_date",
                table: "Wishlists",
                newName: "WishlistDate");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Wishlists",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Wishlists",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "wishlist_id",
                table: "Wishlists",
                newName: "WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_user_id",
                table: "Wishlists",
                newName: "IX_Wishlists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_product_id",
                table: "Wishlists",
                newName: "IX_Wishlists_ProductId");

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

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "review_date",
                table: "Reviews",
                newName: "ReviewDate");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Reviews",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "review_id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_user_id",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_product_id",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "brand_id",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_category_id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_brand_id",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Payments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "Payments",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "payment_status",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "payment_method",
                table: "Payments",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "payment_date",
                table: "Payments",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Payments",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Payments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "payment_id",
                table: "Payments",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_order_id",
                table: "Payments",
                newName: "IX_Payments_OrderId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Orders",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Orders",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Orders",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_user_id",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "CartItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "CartItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "cartitem_id",
                table: "CartItems",
                newName: "CartitemId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_product_id",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_cart_id",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_user_id",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Brands",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "brand_name",
                table: "Brands",
                newName: "BrandName");

            migrationBuilder.RenameColumn(
                name: "brand_id",
                table: "Brands",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_category_id",
                table: "Brands",
                newName: "IX_Brands_CategoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WishlistDate",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

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
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "OrderitemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                column: "WishlistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "CartitemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                newName: "Wishlist");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderitemId",
                table: "OrderItems",
                newName: "orderitem_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_order_id");

            migrationBuilder.RenameColumn(
                name: "WishlistDate",
                table: "Wishlist",
                newName: "wishlist_date");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Wishlist",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Wishlist",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "Wishlist",
                newName: "wishlist_id");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlist",
                newName: "IX_Wishlist_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_ProductId",
                table: "Wishlist",
                newName: "IX_Wishlist_product_id");

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

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Review",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Review",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Review",
                newName: "review_date");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Review",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Review",
                newName: "review_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Review",
                newName: "IX_Review_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "Review",
                newName: "IX_Review_product_id");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Product",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Product",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Product",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Product",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Product",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Product",
                newName: "brand_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Product",
                newName: "IX_Product_brand_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Payment",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Payment",
                newName: "transaction_id");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payment",
                newName: "payment_status");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Payment",
                newName: "payment_method");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payment",
                newName: "payment_date");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Payment",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Payment",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payment",
                newName: "payment_id");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_OrderId",
                table: "Payment",
                newName: "IX_Payment_order_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Order",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Order",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Order",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Order",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Order",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Order",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Order",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_user_id");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Category",
                newName: "category_name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Cart",
                newName: "cart_id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "Cart",
                newName: "IX_Cart_user_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItem",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItem",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItem",
                newName: "cart_id");

            migrationBuilder.RenameColumn(
                name: "CartitemId",
                table: "CartItem",
                newName: "cartitem_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItem",
                newName: "IX_CartItem_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "CartItem",
                newName: "IX_CartItem_cart_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Brand",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brand",
                newName: "brand_name");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Brand",
                newName: "brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_CategoryId",
                table: "Brand",
                newName: "IX_Brand_category_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "OrderItems",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "wishlist_date",
                table: "Wishlist",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

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
                name: "comment",
                table: "Review",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "review_date",
                table: "Review",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Product",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Product",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Product",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Product",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "Product",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "Payment",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "Payment",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "transaction_id",
                table: "Payment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "payment_status",
                table: "Payment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "payment_method",
                table: "Payment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "payment_date",
                table: "Payment",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Payment",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Order",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Order",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "Order",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "Order",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "Order",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Order",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderIte__A0B0C5903659C8B8",
                table: "OrderItems",
                column: "orderitem_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Wishlist__6151514E7DDDBF79",
                table: "Wishlist",
                column: "wishlist_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__User__B9BE370FD62FF6A0",
                table: "User",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Role__760965CC0A99C65A",
                table: "Role",
                column: "role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Review__60883D90D4DE854C",
                table: "Review",
                column: "review_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Product__47027DF5E1A55C44",
                table: "Product",
                column: "product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Payment__ED1FC9EA521A1E36",
                table: "Payment",
                column: "payment_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Order__46596229D87C43B0",
                table: "Order",
                column: "order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Category__D54EE9B431E0794C",
                table: "Category",
                column: "category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cart__2EF52A2767FD9772",
                table: "Cart",
                column: "cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CartItem__66AF91ADEDF6A26C",
                table: "CartItem",
                column: "cartitem_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Brand__5E5A8E2767B9AAC5",
                table: "Brand",
                column: "brand_id");

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    shipping_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    shipped_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    shipping_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                name: "IX_Shipping_order_id",
                table: "Shipping",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Brand__category___3E52440B",
                table: "Brand",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Cart__user_id__49C3F6B7",
                table: "Cart",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItem__cart_i__4CA06362",
                table: "CartItem",
                column: "cart_id",
                principalTable: "Cart",
                principalColumn: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItem__produc__4D94879B",
                table: "CartItem",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Order__user_id__5070F446",
                table: "Order",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderItem__order__534D60F1",
                table: "OrderItems",
                column: "order_id",
                principalTable: "Order",
                principalColumn: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderItem__produ__5441852A",
                table: "OrderItems",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Payment__order_i__571DF1D5",
                table: "Payment",
                column: "order_id",
                principalTable: "Order",
                principalColumn: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Product__brand_i__4222D4EF",
                table: "Product",
                column: "brand_id",
                principalTable: "Brand",
                principalColumn: "brand_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Product__categor__412EB0B6",
                table: "Product",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Review__product___5EBF139D",
                table: "Review",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Review__user_id__5DCAEF64",
                table: "Review",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__User__role_id__398D8EEE",
                table: "User",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Wishlist__produc__5AEE82B9",
                table: "Wishlist",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Wishlist__user_i__59FA5E80",
                table: "Wishlist",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id");
        }
    }
}
