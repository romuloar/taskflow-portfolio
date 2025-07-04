﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;

#nullable disable

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(WorkflowDbContext))]
    [Migration("20250527034307_InitialWorkflow")]
    partial class InitialWorkflow
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("Workflow.Domain.Entities.Task.TaskDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.HasKey("Id");

                    b.ToTable("Task", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
