﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using CandidateHub.Database;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Database.Context;

public partial class SigmaContext : DbContext
{
    public SigmaContext(DbContextOptions<SigmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidate { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC07C13272DC");

            entity.ToTable("Candidate", "CH");

            entity.HasIndex(e => e.Email, "UQ__Candidat__A9D10534ADB800F1").IsUnique();

            entity.Property(e => e.Comment).IsRequired();
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.ExposeId)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.PhoneNumber).HasMaxLength(1000);
            entity.Property(e => e.TimeInterval).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}