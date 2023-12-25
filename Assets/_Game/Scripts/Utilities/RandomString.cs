using System.Linq;

public static class RandomString
{
    public static string Generate(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new System.Random();
        return new string(Enumerable.Repeat(chars, length)
                       .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
