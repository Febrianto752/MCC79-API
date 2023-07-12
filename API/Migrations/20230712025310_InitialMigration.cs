using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employees",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nik = table.Column<string>(type: "nchar(6)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employees", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_rooms",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_rooms", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_universities",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_universities", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    expired_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employees_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_bookings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    room_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_bookings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_bookings_tb_m_rooms_room_guid",
                        column: x => x.room_guid,
                        principalTable: "tb_m_rooms",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_educations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    major = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    degree = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    gpa = table.Column<float>(type: "real", nullable: false),
                    university_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_educations", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_educations_tb_m_employees_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_educations_tb_m_universities_university_guid",
                        column: x => x.university_guid,
                        principalTable: "tb_m_universities",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_roles", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_roles_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_roles",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tb_m_employees",
                columns: new[] { "guid", "birth_date", "created_date", "email", "first_name", "gender", "hiring_date", "last_name", "modified_date", "nik", "phone_number" },
                values: new object[,]
                {
                    { new Guid("3e779681-fe6f-4fa6-afa1-0fbeed48a91c"), new DateTime(1974, 2, 18, 5, 3, 43, 171, DateTimeKind.Local).AddTicks(2103), new DateTime(2023, 7, 12, 9, 53, 10, 61, DateTimeKind.Local).AddTicks(3170), "Ana.Greenholt55@yahoo.com", "Norma", 1, new DateTime(2018, 3, 12, 17, 56, 59, 403, DateTimeKind.Local).AddTicks(2471), "Hintz", new DateTime(2023, 7, 12, 9, 53, 10, 61, DateTimeKind.Local).AddTicks(3179), "3197", "+62857-2660-8542" },
                    { new Guid("4d45a634-5d25-488b-99fa-d8bb3e63e32a"), new DateTime(1964, 4, 21, 13, 19, 52, 4, DateTimeKind.Local).AddTicks(7460), new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(9784), "Juana.OConnell9@hotmail.com", "Christian", 0, new DateTime(1980, 12, 12, 9, 35, 21, 635, DateTimeKind.Local).AddTicks(6632), "Rice", new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(9791), "3876", "+62852-1785-0913" },
                    { new Guid("5e676bfc-b645-471a-8a00-2415942f92ee"), new DateTime(1984, 10, 28, 11, 20, 30, 853, DateTimeKind.Local).AddTicks(7366), new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(3525), "Antoinette_Considine65@gmail.com", "Elmo", 1, new DateTime(2018, 4, 10, 3, 2, 22, 766, DateTimeKind.Local).AddTicks(8215), "Bode", new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(3591), "3059", "+62870-6567-9572" },
                    { new Guid("63add09e-d08f-4429-8406-1c5242c91232"), new DateTime(1997, 5, 28, 13, 1, 8, 336, DateTimeKind.Local).AddTicks(9254), new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(5786), "Erin_Weimann69@gmail.com", "Franz", 0, new DateTime(2009, 5, 21, 22, 9, 19, 563, DateTimeKind.Local).AddTicks(9682), "Blick", new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(5792), "6465", "+62817-2860-4971" },
                    { new Guid("64d20f51-62a0-4736-ad40-09534baceacf"), new DateTime(1978, 10, 5, 12, 16, 8, 189, DateTimeKind.Local).AddTicks(7736), new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(9258), "Leona45@gmail.com", "Summer", 1, new DateTime(2021, 10, 7, 3, 2, 2, 212, DateTimeKind.Local).AddTicks(9892), "Turcotte", new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(9264), "4396", "+62842-2328-5415" },
                    { new Guid("6bf40dc9-f119-4c9f-8911-b7c37a55de9d"), new DateTime(1984, 10, 19, 23, 46, 33, 486, DateTimeKind.Local).AddTicks(873), new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(3230), "Terence.Padberg@hotmail.com", "Estevan", 0, new DateTime(2003, 3, 7, 10, 11, 0, 914, DateTimeKind.Local).AddTicks(1977), "Bernier", new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(3237), "6990", "+62854-5227-6464" },
                    { new Guid("8db022b0-9c35-4432-a20b-f1a7ac9428d8"), new DateTime(1972, 9, 29, 2, 4, 47, 309, DateTimeKind.Local).AddTicks(4547), new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(5071), "Seth.Predovic76@hotmail.com", "Alaina", 1, new DateTime(2011, 3, 30, 8, 25, 38, 365, DateTimeKind.Local).AddTicks(3624), "Renner", new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(5077), "8377", "+62830-2388-3742" },
                    { new Guid("9f4d5a50-e3e9-4533-8572-1854caebc4c1"), new DateTime(1978, 5, 29, 7, 23, 47, 774, DateTimeKind.Local).AddTicks(2885), new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(2095), "Dana.Wyman47@hotmail.com", "Dena", 0, new DateTime(2009, 7, 27, 6, 18, 7, 392, DateTimeKind.Local).AddTicks(776), "Barton", new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(2102), "4020", "+62852-6328-7546" },
                    { new Guid("a41be7d5-44e1-4a6c-aac4-ad734d7808f5"), new DateTime(2001, 12, 26, 6, 54, 41, 693, DateTimeKind.Local).AddTicks(9622), new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(6254), "Laura.Considine9@gmail.com", "Louvenia", 1, new DateTime(2021, 2, 21, 23, 46, 19, 138, DateTimeKind.Local).AddTicks(5155), "Witting", new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(6260), "7805", "+62800-6026-9162" },
                    { new Guid("ee519d0c-26e3-465a-b1e4-4670ee92f195"), new DateTime(1991, 4, 20, 11, 19, 24, 420, DateTimeKind.Local).AddTicks(1601), new DateTime(2023, 7, 12, 9, 53, 10, 59, DateTimeKind.Local).AddTicks(9726), "Francis.OConnell@hotmail.com", "Aric", 0, new DateTime(2007, 3, 19, 10, 27, 9, 663, DateTimeKind.Local).AddTicks(5559), "Kerluke", new DateTime(2023, 7, 12, 9, 53, 10, 59, DateTimeKind.Local).AddTicks(9740), "9206", "+62872-6744-7588" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations",
                column: "university_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_nik_email_phone_number",
                table: "tb_m_employees",
                columns: new[] { "nik", "email", "phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_account_guid_role_guid",
                table: "tb_tr_account_roles",
                columns: new[] { "account_guid", "role_guid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_role_guid",
                table: "tb_tr_account_roles",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bookings_room_guid",
                table: "tb_tr_bookings",
                column: "room_guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_educations");

            migrationBuilder.DropTable(
                name: "tb_tr_account_roles");

            migrationBuilder.DropTable(
                name: "tb_tr_bookings");

            migrationBuilder.DropTable(
                name: "tb_m_universities");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_rooms");

            migrationBuilder.DropTable(
                name: "tb_m_employees");
        }
    }
}
