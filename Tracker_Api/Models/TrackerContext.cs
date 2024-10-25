using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tracker_Api.Models.Tracker;

namespace Tracker_Api.Models;

public partial class TrackerContext : DbContext
{
    public TrackerContext()
    {
    }

    public TrackerContext(DbContextOptions<TrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TrackingData> TrackingDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackingData>(entity =>
        {
            entity.HasKey(e => new { e.User, e.Date }).HasName("PK_TrackingData");

            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HappySentence)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
