﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DB_Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DB_Service.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DB_Service.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileRef")
                        .HasColumnType("text");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DB_Service.Models.Dependence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("First")
                        .HasColumnType("integer");

                    b.Property<int?>("Second")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("First");

                    b.HasIndex("Second");

                    b.ToTable("Dependences");
                });

            modelBuilder.Entity("DB_Service.Models.Edge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("End")
                        .HasColumnType("integer");

                    b.Property<int?>("Start")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("End");

                    b.HasIndex("Start");

                    b.ToTable("Edges");
                });

            modelBuilder.Entity("DB_Service.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Field")
                        .HasColumnType("text");

                    b.Property<string>("LogId")
                        .HasColumnType("text");

                    b.Property<string>("New")
                        .HasColumnType("text");

                    b.Property<string>("Old")
                        .HasColumnType("text");

                    b.Property<string>("Operation")
                        .HasColumnType("text");

                    b.Property<string>("Table")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("DB_Service.Models.Passport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.ToTable("Passports");
                });

            modelBuilder.Entity("DB_Service.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("DB_Service.Models.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("ExpectedTime")
                        .HasColumnType("interval");

                    b.Property<int?>("Head")
                        .HasColumnType("integer");

                    b.Property<bool>("IsTemplate")
                        .HasColumnType("boolean");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("integer");

                    b.Property<int?>("Tail")
                        .HasColumnType("integer");

                    b.Property<string>("TeamcenterRef")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Head")
                        .IsUnique();

                    b.HasIndex("PriorityId");

                    b.HasIndex("Tail")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("DB_Service.Models.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Addenable")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<List<int>>("CanCreate")
                        .HasColumnType("integer[]");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CustomField")
                        .HasColumnType("text");

                    b.Property<bool?>("Mark")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<bool?>("Pass")
                        .HasColumnType("boolean");

                    b.Property<int?>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<int?>("SignId")
                        .HasColumnType("integer");

                    b.Property<string>("Signed")
                        .HasColumnType("text");

                    b.Property<DateTime?>("SignedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.HasIndex("StatusId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("DB_Service.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("DB_Service.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("EndVerificationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<TimeSpan>("ExpectedTime")
                        .HasColumnType("interval");

                    b.Property<int?>("SignId")
                        .HasColumnType("integer");

                    b.Property<string>("Signed")
                        .HasColumnType("text");

                    b.Property<int?>("StageId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DB_Service.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("DB_Service.Models.Comment", b =>
                {
                    b.HasOne("DB_Service.Models.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DB_Service.Models.Dependence", b =>
                {
                    b.HasOne("DB_Service.Models.Stage", "FirstStage")
                        .WithMany("FirstDependences")
                        .HasForeignKey("First")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Stage", "SecondStage")
                        .WithMany("SecondDependences")
                        .HasForeignKey("Second")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("FirstStage");

                    b.Navigation("SecondStage");
                });

            modelBuilder.Entity("DB_Service.Models.Edge", b =>
                {
                    b.HasOne("DB_Service.Models.Stage", "EndStage")
                        .WithMany("EndEdges")
                        .HasForeignKey("End")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Stage", "StartStage")
                        .WithMany("StartEdges")
                        .HasForeignKey("Start")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("EndStage");

                    b.Navigation("StartStage");
                });

            modelBuilder.Entity("DB_Service.Models.Passport", b =>
                {
                    b.HasOne("DB_Service.Models.Process", "Process")
                        .WithMany("Passports")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Process");
                });

            modelBuilder.Entity("DB_Service.Models.Process", b =>
                {
                    b.HasOne("DB_Service.Models.Stage", "HeadStage")
                        .WithOne()
                        .HasForeignKey("DB_Service.Models.Process", "Head")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Priority", "Priority")
                        .WithMany("Processes")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Stage", "TailStage")
                        .WithOne()
                        .HasForeignKey("DB_Service.Models.Process", "Tail")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Type", "Type")
                        .WithMany("Processes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("HeadStage");

                    b.Navigation("Priority");

                    b.Navigation("TailStage");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DB_Service.Models.Stage", b =>
                {
                    b.HasOne("DB_Service.Models.Process", "Process")
                        .WithMany("Stages")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DB_Service.Models.Status", "Status")
                        .WithMany("Stages")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Process");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DB_Service.Models.Task", b =>
                {
                    b.HasOne("DB_Service.Models.Stage", "Stage")
                        .WithMany("Tasks")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("DB_Service.Models.Priority", b =>
                {
                    b.Navigation("Processes");
                });

            modelBuilder.Entity("DB_Service.Models.Process", b =>
                {
                    b.Navigation("Passports");

                    b.Navigation("Stages");
                });

            modelBuilder.Entity("DB_Service.Models.Stage", b =>
                {
                    b.Navigation("EndEdges");

                    b.Navigation("FirstDependences");

                    b.Navigation("SecondDependences");

                    b.Navigation("StartEdges");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DB_Service.Models.Status", b =>
                {
                    b.Navigation("Stages");
                });

            modelBuilder.Entity("DB_Service.Models.Task", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DB_Service.Models.Type", b =>
                {
                    b.Navigation("Processes");
                });
#pragma warning restore 612, 618
        }
    }
}
