using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SparkClips.Data;
using SparkClips.Services.BlobBob;

namespace SparkClips.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170528212300_change cost to string")]
    partial class changecosttostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SparkClips.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("HairColor");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry", b =>
                {
                    b.Property<int>("GalleryEntryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Instructions")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("GalleryEntryID");

                    b.ToTable("GalleryEntries");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_ApplicationUser", b =>
                {
                    b.Property<int>("GalleryEntryID");

                    b.Property<string>("ApplicationUserID");

                    b.HasKey("GalleryEntryID", "ApplicationUserID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("GalleryEntry_ApplicationUser");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Image", b =>
                {
                    b.Property<int>("ImageID");

                    b.Property<int>("GalleryEntryID");

                    b.HasKey("ImageID", "GalleryEntryID");

                    b.HasIndex("GalleryEntryID");

                    b.ToTable("GalleryEntry_Image");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Tag", b =>
                {
                    b.Property<int>("TagID");

                    b.Property<int>("GalleryEntryID");

                    b.HasKey("TagID", "GalleryEntryID");

                    b.HasIndex("GalleryEntryID");

                    b.ToTable("GalleryEntry_Tag");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerName");

                    b.Property<string>("Filename");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry", b =>
                {
                    b.Property<int>("LogEntryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("Barbers");

                    b.Property<string>("Cost");

                    b.Property<DateTime>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.HasKey("LogEntryID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry_Image", b =>
                {
                    b.Property<int>("LogEntryID");

                    b.Property<int>("ImageID");

                    b.HasKey("LogEntryID", "ImageID");

                    b.HasIndex("ImageID");

                    b.ToTable("LogEntry_Image");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TagID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SparkClips.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SparkClips.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_ApplicationUser", b =>
                {
                    b.HasOne("SparkClips.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("GalleryEntries")
                        .HasForeignKey("ApplicationUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.GalleryEntry", "GalleryEntry")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("GalleryEntryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Image", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.GalleryEntry", "GalleryEntry")
                        .WithMany("Images")
                        .HasForeignKey("GalleryEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.Image", "Image")
                        .WithMany("GalleryEntries")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.GalleryEntry_Tag", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.GalleryEntry", "GalleryEntry")
                        .WithMany("Tags")
                        .HasForeignKey("GalleryEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.Tag", "Tag")
                        .WithMany("GalleryEntries")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry", b =>
                {
                    b.HasOne("SparkClips.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("LogEntries")
                        .HasForeignKey("ApplicationUserID");
                });

            modelBuilder.Entity("SparkClips.Models.HairyDatabase.LogEntry_Image", b =>
                {
                    b.HasOne("SparkClips.Models.HairyDatabase.Image", "Image")
                        .WithMany("LogEntries")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkClips.Models.HairyDatabase.LogEntry", "LogEntry")
                        .WithMany("Images")
                        .HasForeignKey("LogEntryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
