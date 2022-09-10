using Microsoft.Extensions.FileProviders;
using static Shared.AppSettings;

namespace Web.Extensions;

public static partial class WebApplicationExtensions
{
    public static void UseCdn(this WebApplication app)
    {
        var cdnDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), CdnPaths.CdnDirectory);

        UseStatisFiles(app, cdnDirectoryPath, CdnPaths.RequestCdnPath);

        UseStatisFiles(app, Path.Combine(cdnDirectoryPath, CdnPaths.ProductImages), CdnPaths.RequestCdnPath + "/products/images");
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
