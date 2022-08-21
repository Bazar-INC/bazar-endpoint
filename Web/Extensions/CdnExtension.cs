using Microsoft.Extensions.FileProviders;
using static Shared.AppSettings;

namespace Web.Extensions;

public static partial class WebApplicationExtensions
{
    public static void UseCdn(this WebApplication app)
    {
        if(!Directory.Exists(CdnPaths.CdnDirectory))
        {
            Directory.CreateDirectory(CdnPaths.CdnDirectory);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), CdnPaths.CdnDirectory)),
            RequestPath = CdnPaths.RequestCdnPath
        });
    }
}
