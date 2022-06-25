
using Shared;

namespace Application.Features.AuthFeatures.Services;

public static class CodeGeneratorService
{
    public static string GenerateCode()
    {
        string code = "";

        for (int i = 0; i < AppSettings.Sms.CodeLength; i++)
        {
            code += GenerateRandomNumber();
        }

        return code;
    }

    private static int GenerateRandomNumber()
    {
        return new Random().Next(1, 9);
    }
}
