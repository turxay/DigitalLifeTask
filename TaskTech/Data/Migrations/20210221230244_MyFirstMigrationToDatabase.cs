using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTech.Data.Migrations
{
    public partial class MyFirstMigrationToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projeler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProName = table.Column<string>(maxLength: 100, nullable: false),
                    ProTitle = table.Column<string>(maxLength: 500, nullable: false),
                    ProImage = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoicess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceName = table.Column<string>(maxLength: 20, nullable: false),
                    InvoiceTitle = table.Column<string>(maxLength: 300, nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 50, nullable: false),
                    NetAmount = table.Column<int>(nullable: false),
                    TazAmount = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(maxLength: 100, nullable: false),
                    Note = table.Column<string>(maxLength: 700, nullable: false),
                    PaymentStatusPending = table.Column<bool>(nullable: false),
                    ProjectsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoicess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoicess_Projeler_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoicess_ProjectsId",
                table: "Invoicess",
                column: "ProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoicess");

            migrationBuilder.DropTable(
                name: "Projeler");
        }
    }
}
