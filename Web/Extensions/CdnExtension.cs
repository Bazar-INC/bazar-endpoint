using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Shared;
using System.IO;
using static Shared.AppSettings;

namespace Web.Extensions;

public static partial class WebApplicationExtensions
{
    public static void UseCdn(this WebApplication app)
    {
        var cdnDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), CdnPaths.CdnDirectory.Item1);

        // use base Cdn path
        UseStatisFiles(app, cdnDirectoryPath, CdnPaths.CdnDirectory.Item2);

        UseStatisFiles(app, Path.Combine(cdnDirectoryPath, CdnPaths.CategoryIcons.Item1), CdnPaths.CategoryIcons.Item2);
        UseStatisFiles(app, Path.Combine(cdnDirectoryPath, CdnPaths.CategoryImages.Item1), CdnPaths.CategoryImages.Item2);
        UseStatisFiles(app, Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Cdn/"), CdnPaths.ProductImages.Item1), CdnPaths.ProductImages.Item2);
        UseStatisFiles(app, Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Cdn/"), CdnPaths.UsersAvatars.Item1), CdnPaths.UsersAvatars.Item2);

        //// use other children paths
        //UseAllCdnStaticFiles(app, cdnDirectoryPath);
    }

    /// <summary>
    /// use static files using reflection
    /// </summary>
    private static void UseAllCdnStaticFiles(WebApplication app, string baseDirectory)
    {
        Type type = typeof(CdnPaths);

        // get all public fields in CdnPaths class
        foreach (var cdnPathField in type.GetFields().Where(f => f.IsPublic &&
        // and exclude base CdnDirectory
        !((Tuple<string, string>)f.GetValue(f)!).Item1!.Equals(CdnPaths.CdnDirectory.Item1)))
        {
            var path = (Tuple<string, string>)cdnPathField.GetValue(cdnPathField)!;

            UseStatisFiles(app, Path.Combine(baseDirectory, path.Item1), path.Item2);
        }
    }

    private static void UseStatisFiles(WebApplication app, string filePath, string requestPath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(filePath),
            RequestPath = requestPath
        });
    }
}
