﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutGenerator.Data;

namespace WorkoutGenerator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190817102940_RemovedExerciseConstraint")]
    partial class RemovedExerciseConstraint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WorkoutGenerator.Data.BodyBuildingProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseType");

                    b.Property<int>("MuscleType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.MuscleExercises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MuscleType");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("MuscleExercises");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DaysType");

                    b.Property<int>("TemplateType");

                    b.Property<int>("TrainerLevelType");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MuscleExercisesId");

                    b.Property<string>("Name");

                    b.Property<string>("Reps");

                    b.Property<string>("Rest");

                    b.Property<string>("Sets");

                    b.HasKey("Id");

                    b.HasIndex("MuscleExercisesId");

                    b.ToTable("WorkoutExercise");
                });

            modelBuilder.Entity("WorkoutGenerator.Data.BodyBuildingProgram", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.MuscleExercises", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.Workout", "Workout")
                        .WithMany("MuscleExercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.Workout", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.Template", "Template")
                        .WithMany("Workouts")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutGenerator.Data.WorkoutExercise", b =>
                {
                    b.HasOne("WorkoutGenerator.Data.MuscleExercises")
                        .WithMany("Exercises")
                        .HasForeignKey("MuscleExercisesId");
                });
#pragma warning restore 612, 618
        }
    }
}
