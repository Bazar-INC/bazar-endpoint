using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using static Shared.AppSettings;

namespace Web.Extensions;

public static partial class WebApplicationExtensions
{
    public static void UseSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>();
            var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();

            context!.Database.Migrate();

            Seed(roleManager!, userManager!).Wait();
        }
    }

    private static async Task Seed(RoleManager<RoleEntity> roleManager, UserManager<UserEntity> userManager)
    {
        await SeedRoles(roleManager);

        await SeedUsers(userManager);
    }

    /// <summary>
    /// seed roles using reflection
    /// </summary>
    /// <param name="roleManager">role manager to manage roles</param>
    /// <returns>awaited Task</returns>
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

            if (!isRoleExist)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }

    private static async Task SeedUsers(UserManager<UserEntity> userManager)
    {
        await SeedUser(userManager, Users.AdminUserPhone, Roles.Admin);
        await SeedUser(userManager, Users.SellerUserPhone, Roles.Seller);
        await SeedUser(userManager, Users.ManagerUserPhone, Roles.Manager);
    }

    private static async Task SeedUser(UserManager<UserEntity> userManager, string phoneNumber, string role)
    {
        var user = await userManager.FindByNameAsync(phoneNumber);
        bool isUserExist = user != null;

        if (!isUserExist)
        {
            user = new UserEntity()
            {
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = true,
                UserName = phoneNumber
            };

            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}