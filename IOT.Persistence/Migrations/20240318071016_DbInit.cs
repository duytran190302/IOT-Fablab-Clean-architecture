using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOT.Persistence.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "Oder",
                columns: table => new
                {
                    OderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Custummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oder", x => x.OderId);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerId);
                });

            migrationBuilder.CreateTable(
                name: "OEE",
                columns: table => new
                {
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdleTime = table.Column<float>(type: "real", nullable: false),
                    ShiftTime = table.Column<float>(type: "real", nullable: false),
                    OperationTime = table.Column<float>(type: "real", nullable: false),
                    Oee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OEE", x => new { x.TimeStamp, x.MachineId });
                    table.ForeignKey(
                        name: "FK_OEE_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_Oder_OderId",
                        column: x => x.OderId,
                        principalTable: "Oder",
                        principalColumn: "OderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerPicture",
                columns: table => new
                {
                    WorkerPictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerPicture", x => new { x.WorkerPictureId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_WorkerPicture_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    DetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetailStatus = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_Detail_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "MachineId");
                    table.ForeignKey(
                        name: "FK_Detail_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detail_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "WorkerId");
                });

            migrationBuilder.CreateTable(
                name: "DetailPicture",
                columns: table => new
                {
                    DetailPictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPicture", x => new { x.DetailPictureId, x.DetailId });
                    table.ForeignKey(
                        name: "FK_DetailPicture_Detail_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Detail",
                        principalColumn: "DetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detail_MachineId",
                table: "Detail",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_ProjectId",
                table: "Detail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_WorkerId",
                table: "Detail",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPicture_DetailId",
                table: "DetailPicture",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OEE_MachineId",
                table: "OEE",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_OderId",
                table: "Project",
                column: "OderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerPicture_WorkerId",
                table: "WorkerPicture",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailPicture");

            migrationBuilder.DropTable(
                name: "OEE");

            migrationBuilder.DropTable(
                name: "WorkerPicture");

            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Oder");
        }
    }
}
