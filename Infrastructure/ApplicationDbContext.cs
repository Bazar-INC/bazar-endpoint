﻿using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public virtual DbSet<CodeEntity>? Codes { get; set; }
    public virtual DbSet<CodeEntity>? Products { get; set; }
    public virtual DbSet<CodeEntity>? Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
