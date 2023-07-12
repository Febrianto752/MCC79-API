﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    partial class BookingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("expired_time");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit")
                        .HasColumnName("is_used");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<int>("Otp")
                        .HasColumnType("int")
                        .HasColumnName("otp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_accounts");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_guid");

                    b.HasKey("Guid");

                    b.HasIndex("RoleGuid");

                    b.HasIndex("AccountGuid", "RoleGuid")
                        .IsUnique();

                    b.ToTable("tb_tr_account_roles");
                });

            modelBuilder.Entity("API.Models.Booking", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<Guid>("EmployeeGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("employee_guid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("remarks");

                    b.Property<Guid>("RoomGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_guid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Guid");

                    b.HasIndex("EmployeeGuid");

                    b.HasIndex("RoomGuid");

                    b.ToTable("tb_tr_bookings");
                });

            modelBuilder.Entity("API.Models.Education", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("degree");

                    b.Property<float>("Gpa")
                        .HasColumnType("real")
                        .HasColumnName("gpa");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("major");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("UniversityGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("university_guid");

                    b.HasKey("Guid");

                    b.HasIndex("UniversityGuid");

                    b.ToTable("tb_m_educations");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("hiring_date");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Nik")
                        .IsRequired()
                        .HasColumnType("nchar(6)")
                        .HasColumnName("nik");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.HasKey("Guid");

                    b.HasIndex("Nik", "Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("tb_m_employees");

                    b.HasData(
                        new
                        {
                            Guid = new Guid("5e676bfc-b645-471a-8a00-2415942f92ee"),
                            BirthDate = new DateTime(1984, 10, 28, 11, 20, 30, 853, DateTimeKind.Local).AddTicks(7366),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(3525),
                            Email = "Antoinette_Considine65@gmail.com",
                            FirstName = "Elmo",
                            Gender = 1,
                            HiringDate = new DateTime(2018, 4, 10, 3, 2, 22, 766, DateTimeKind.Local).AddTicks(8215),
                            LastName = "Bode",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(3591),
                            Nik = "3059",
                            PhoneNumber = "+62870-6567-9572"
                        },
                        new
                        {
                            Guid = new Guid("63add09e-d08f-4429-8406-1c5242c91232"),
                            BirthDate = new DateTime(1997, 5, 28, 13, 1, 8, 336, DateTimeKind.Local).AddTicks(9254),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(5786),
                            Email = "Erin_Weimann69@gmail.com",
                            FirstName = "Franz",
                            Gender = 0,
                            HiringDate = new DateTime(2009, 5, 21, 22, 9, 19, 563, DateTimeKind.Local).AddTicks(9682),
                            LastName = "Blick",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(5792),
                            Nik = "6465",
                            PhoneNumber = "+62817-2860-4971"
                        },
                        new
                        {
                            Guid = new Guid("64d20f51-62a0-4736-ad40-09534baceacf"),
                            BirthDate = new DateTime(1978, 10, 5, 12, 16, 8, 189, DateTimeKind.Local).AddTicks(7736),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(9258),
                            Email = "Leona45@gmail.com",
                            FirstName = "Summer",
                            Gender = 1,
                            HiringDate = new DateTime(2021, 10, 7, 3, 2, 2, 212, DateTimeKind.Local).AddTicks(9892),
                            LastName = "Turcotte",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 56, DateTimeKind.Local).AddTicks(9264),
                            Nik = "4396",
                            PhoneNumber = "+62842-2328-5415"
                        },
                        new
                        {
                            Guid = new Guid("9f4d5a50-e3e9-4533-8572-1854caebc4c1"),
                            BirthDate = new DateTime(1978, 5, 29, 7, 23, 47, 774, DateTimeKind.Local).AddTicks(2885),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(2095),
                            Email = "Dana.Wyman47@hotmail.com",
                            FirstName = "Dena",
                            Gender = 0,
                            HiringDate = new DateTime(2009, 7, 27, 6, 18, 7, 392, DateTimeKind.Local).AddTicks(776),
                            LastName = "Barton",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(2102),
                            Nik = "4020",
                            PhoneNumber = "+62852-6328-7546"
                        },
                        new
                        {
                            Guid = new Guid("8db022b0-9c35-4432-a20b-f1a7ac9428d8"),
                            BirthDate = new DateTime(1972, 9, 29, 2, 4, 47, 309, DateTimeKind.Local).AddTicks(4547),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(5071),
                            Email = "Seth.Predovic76@hotmail.com",
                            FirstName = "Alaina",
                            Gender = 1,
                            HiringDate = new DateTime(2011, 3, 30, 8, 25, 38, 365, DateTimeKind.Local).AddTicks(3624),
                            LastName = "Renner",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 57, DateTimeKind.Local).AddTicks(5077),
                            Nik = "8377",
                            PhoneNumber = "+62830-2388-3742"
                        },
                        new
                        {
                            Guid = new Guid("ee519d0c-26e3-465a-b1e4-4670ee92f195"),
                            BirthDate = new DateTime(1991, 4, 20, 11, 19, 24, 420, DateTimeKind.Local).AddTicks(1601),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 59, DateTimeKind.Local).AddTicks(9726),
                            Email = "Francis.OConnell@hotmail.com",
                            FirstName = "Aric",
                            Gender = 0,
                            HiringDate = new DateTime(2007, 3, 19, 10, 27, 9, 663, DateTimeKind.Local).AddTicks(5559),
                            LastName = "Kerluke",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 59, DateTimeKind.Local).AddTicks(9740),
                            Nik = "9206",
                            PhoneNumber = "+62872-6744-7588"
                        },
                        new
                        {
                            Guid = new Guid("6bf40dc9-f119-4c9f-8911-b7c37a55de9d"),
                            BirthDate = new DateTime(1984, 10, 19, 23, 46, 33, 486, DateTimeKind.Local).AddTicks(873),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(3230),
                            Email = "Terence.Padberg@hotmail.com",
                            FirstName = "Estevan",
                            Gender = 0,
                            HiringDate = new DateTime(2003, 3, 7, 10, 11, 0, 914, DateTimeKind.Local).AddTicks(1977),
                            LastName = "Bernier",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(3237),
                            Nik = "6990",
                            PhoneNumber = "+62854-5227-6464"
                        },
                        new
                        {
                            Guid = new Guid("a41be7d5-44e1-4a6c-aac4-ad734d7808f5"),
                            BirthDate = new DateTime(2001, 12, 26, 6, 54, 41, 693, DateTimeKind.Local).AddTicks(9622),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(6254),
                            Email = "Laura.Considine9@gmail.com",
                            FirstName = "Louvenia",
                            Gender = 1,
                            HiringDate = new DateTime(2021, 2, 21, 23, 46, 19, 138, DateTimeKind.Local).AddTicks(5155),
                            LastName = "Witting",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(6260),
                            Nik = "7805",
                            PhoneNumber = "+62800-6026-9162"
                        },
                        new
                        {
                            Guid = new Guid("4d45a634-5d25-488b-99fa-d8bb3e63e32a"),
                            BirthDate = new DateTime(1964, 4, 21, 13, 19, 52, 4, DateTimeKind.Local).AddTicks(7460),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(9784),
                            Email = "Juana.OConnell9@hotmail.com",
                            FirstName = "Christian",
                            Gender = 0,
                            HiringDate = new DateTime(1980, 12, 12, 9, 35, 21, 635, DateTimeKind.Local).AddTicks(6632),
                            LastName = "Rice",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 60, DateTimeKind.Local).AddTicks(9791),
                            Nik = "3876",
                            PhoneNumber = "+62852-1785-0913"
                        },
                        new
                        {
                            Guid = new Guid("3e779681-fe6f-4fa6-afa1-0fbeed48a91c"),
                            BirthDate = new DateTime(1974, 2, 18, 5, 3, 43, 171, DateTimeKind.Local).AddTicks(2103),
                            CreatedDate = new DateTime(2023, 7, 12, 9, 53, 10, 61, DateTimeKind.Local).AddTicks(3170),
                            Email = "Ana.Greenholt55@yahoo.com",
                            FirstName = "Norma",
                            Gender = 1,
                            HiringDate = new DateTime(2018, 3, 12, 17, 56, 59, 403, DateTimeKind.Local).AddTicks(2471),
                            LastName = "Hintz",
                            ModifiedDate = new DateTime(2023, 7, 12, 9, 53, 10, 61, DateTimeKind.Local).AddTicks(3179),
                            Nik = "3197",
                            PhoneNumber = "+62857-2660-8542"
                        });
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_roles");
                });

            modelBuilder.Entity("API.Models.Room", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasColumnName("capacity");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<int>("Floor")
                        .HasColumnType("int")
                        .HasColumnName("floor");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_rooms");
                });

            modelBuilder.Entity("API.Models.University", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_universities");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Models.Account", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.HasOne("API.Models.Account", "Account")
                        .WithMany("AccountRole")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("AccountRole")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Models.Booking", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("Bookings")
                        .HasForeignKey("EmployeeGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("API.Models.Education", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithOne("Education")
                        .HasForeignKey("API.Models.Education", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.University", "University")
                        .WithMany("Educations")
                        .HasForeignKey("UniversityGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("University");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("AccountRole");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Bookings");

                    b.Navigation("Education");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Navigation("AccountRole");
                });

            modelBuilder.Entity("API.Models.Room", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("API.Models.University", b =>
                {
                    b.Navigation("Educations");
                });
#pragma warning restore 612, 618
        }
    }
}
