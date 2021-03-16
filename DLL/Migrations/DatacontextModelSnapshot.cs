﻿// <auto-generated />
using System;
using DLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DLL.Migrations
{
    [DbContext(typeof(Datacontext))]
    partial class DatacontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DLL.Entity.Card", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CardCode");

                    b.Property<string>("CardTypeId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CustomerId");

                    b.Property<string>("FacilityId");

                    b.Property<string>("FacilityName");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Note");

                    b.Property<int>("NumOfDay");

                    b.Property<decimal>("Price");

                    b.Property<string>("ServiceId");

                    b.Property<bool?>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("DLL.Entity.CardType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NameType");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.ToTable("CardTypes");
                });

            modelBuilder.Entity("DLL.Entity.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaoLanhThanhToan");

                    b.Property<string>("CacVanDeCuaNhaCCCu");

                    b.Property<string>("CheckCIC");

                    b.Property<string>("ChucVu");

                    b.Property<string>("CongNo");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("DanhGiaChung");

                    b.Property<string>("DeXuatNhanVien");

                    b.Property<string>("DiaChi");

                    b.Property<string>("DiaChiGiaoHang");

                    b.Property<string>("Email");

                    b.Property<string>("EmailNhanHoaDon");

                    b.Property<string>("GiaTrietKhau");

                    b.Property<string>("GiamDoc");

                    b.Property<string>("HanMuc");

                    b.Property<string>("KeToan");

                    b.Property<string>("LoaiKhachHang");

                    b.Property<string>("LuuY");

                    b.Property<string>("MaKhachHang");

                    b.Property<string>("MaNganhNghe");

                    b.Property<string>("MaSoThue");

                    b.Property<string>("MaVung");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("MongMuonKhachHang");

                    b.Property<string>("NganhNghe");

                    b.Property<string>("NganhNgheId");

                    b.Property<string>("NguoiDaiDienPhapLuat");

                    b.Property<string>("NguoiLienHe");

                    b.Property<string>("NhaCungCapHienTai");

                    b.Property<string>("PhuongAnNhapHang");

                    b.Property<decimal>("SanLuongHangThang");

                    b.Property<string>("SoDienThoai");

                    b.Property<string>("SoDienThoaiKeToan");

                    b.Property<string>("SoDienThoaiNguoiDaiDien");

                    b.Property<bool?>("Status");

                    b.Property<string>("TenKhachHang");

                    b.Property<string>("TenNganhNghe");

                    b.Property<string>("TenVung");

                    b.Property<string>("ThoiHanNo");

                    b.Property<bool?>("TiemNang");

                    b.Property<string>("TrangThaiKhachHang");

                    b.Property<string>("VanPhongGiaoDich");

                    b.Property<string>("VungId");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DLL.Entity.Equipment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<bool?>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("DLL.Entity.Facility", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("FacilityName");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("DLL.Entity.Function", b =>
                {
                    b.Property<string>("FunctionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("FunctionName");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<bool?>("Status");

                    b.HasKey("FunctionId");

                    b.ToTable("Functions");
                });

            modelBuilder.Entity("DLL.Entity.Function_User", b =>
                {
                    b.Property<string>("Function_UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FunctionId");

                    b.Property<string>("UserId");

                    b.HasKey("Function_UserId");

                    b.HasIndex("FunctionId");

                    b.HasIndex("UserId");

                    b.ToTable("Function_Roles");
                });

            modelBuilder.Entity("DLL.Entity.RetrieveOrderRecord", b =>
                {
                    b.Property<int>("STT")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("STT");

                    b.ToTable("RetrieveOrderRecords");
                });

            modelBuilder.Entity("DLL.Entity.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ParentRoleId");

                    b.Property<string>("RoleName");

                    b.Property<bool?>("Status");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DLL.Entity.Service", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<decimal>("Money");

                    b.Property<string>("ServiceName");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DLL.Entity.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<int?>("Gender");

                    b.Property<bool?>("IsOnline");

                    b.Property<int?>("LoginCount");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NguoiQuanLy");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("RoleId");

                    b.Property<bool?>("Status");

                    b.Property<string>("Title");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DLL.Entity.Card", b =>
                {
                    b.HasOne("DLL.Entity.CardType", "CardType")
                        .WithMany("Cards")
                        .HasForeignKey("CardTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DLL.Entity.Facility", "Facility")
                        .WithMany("Cards")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DLL.Entity.Service", "Service")
                        .WithMany("Cards")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DLL.Entity.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DLL.Entity.Equipment", b =>
                {
                    b.HasOne("DLL.Entity.User", "User")
                        .WithMany("Equipments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DLL.Entity.Function_User", b =>
                {
                    b.HasOne("DLL.Entity.Function", "Function")
                        .WithMany("Function_Users")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DLL.Entity.User", "User")
                        .WithMany("Function_Users")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DLL.Entity.User", b =>
                {
                    b.HasOne("DLL.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
