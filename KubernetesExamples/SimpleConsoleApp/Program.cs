namespace SimpleConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        string? name = Environment.GetEnvironmentVariable("POD_NAME");

        Write("data", "file.txt", name);
    }

    private static void Write(string folderName, string fileName, string? consoleAppName)
    {
        if (string.IsNullOrEmpty(consoleAppName))
            consoleAppName = "Default";

        string folderPath = $"{Environment.CurrentDirectory}/{folderName}";

        string filePath = Path.Combine(folderPath, fileName);

        try
        {
            Directory.CreateDirectory(folderPath);

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{consoleAppName} wrote on {DateTime.Now}");
            }

            Console.WriteLine($"{consoleAppName} wrote to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{consoleAppName} has error: {ex.Message}");
        }
    }
}

