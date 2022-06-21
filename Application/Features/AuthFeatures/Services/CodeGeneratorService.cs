
namespace Application.Features.AuthFeatures.Services;

public static class CodeGeneratorService
{
    public static string GenerateCode()
    {
        const int codeLenght = 4;
        string code = "";

        for (int i = 0; i < codeLenght; i++)
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
