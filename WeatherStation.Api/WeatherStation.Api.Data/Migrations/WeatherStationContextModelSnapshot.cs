﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherStation.Api.Data.implementation;

namespace WeatherStation.Api.Data.Migrations
{
    [DbContext(typeof(WeatherStationContext))]
    partial class WeatherStationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("WeatherStation.Api.Data.model.Broadcaster", b =>
                {
                    b.Property<int>("BroadcasterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("BroadcasterId");

                    b.ToTable("Broadcasters");
                });

            modelBuilder.Entity("WeatherStation.Api.Data.model.Record", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BroadcasterId");

                    b.Property<DateTime>("DateTime");

                    b.Property<float>("Humidity");

                    b.Property<float>("Temperature");

                    b.HasKey("RecordId");

                    b.ToTable("Records");
                });
#pragma warning restore 612, 618
        }
    }
}