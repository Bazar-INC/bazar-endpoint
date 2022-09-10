using Microsoft.Extensions.FileProviders;
using static Shared.AppSettings;

namespace Web.Extensions;

public static partial class WebApplicationExtensions
{
    public static void UseCdn(this WebApplication app)
    {
        var cdnDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), CdnPaths.CdnDirectory);
        if (!Directory.Exists(cdnDirectoryPath))
        {
            Directory.CreateDirectory(cdnDirectoryPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(cdnDirectoryPath)),
            RequestPath = CdnPaths.RequestCdnPath
        });
    }
}
