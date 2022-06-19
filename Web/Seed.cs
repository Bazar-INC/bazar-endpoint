﻿using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Web;

public static partial class WebApplicationExtensions
{
    public static void UseSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>();

            Seed(roleManager!).Wait();
        }
    }

    private static async Task Seed(RoleManager<RoleEntity> roleManager)
    {
        await SeedRoles(roleManager);
    }

    private static async Task SeedRoles(RoleManager<RoleEntity> roleManager)
    {
        Type type = typeof(AppSettings.Roles);

        foreach (var roleField in type.GetFields().Where(f => f.IsPublic))
        {
            var role = new RoleEntity()
            {
                Name = roleField.GetValue(roleField)?.ToString()
            };

            var isRoleExist = await roleManager.RoleExistsAsync(role.Name);

            if(!isRoleExist)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}