namespace SimpleConsoleApp;

class Program
{
    private static string folder_name = "data";
    private static string file_name = "file.txt";

    static void Main(string[] args)
    {
        
        string? pod_name = Environment.GetEnvironmentVariable("POD_NAME");

        ReadLastLine(folder_name, file_name, pod_name);
        Write(folder_name, file_name, pod_name);

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

    private static void ReadLastLine(string folderName, string fileName, string? consoleAppName)
    {
        string folderPath = Path.Combine(Environment.CurrentDirectory, folderName);
        string filePath = Path.Combine(folderPath, fileName);

        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    Console.WriteLine($"{consoleAppName} read last line: {lines[lines.Length - 1]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{consoleAppName} has error {filePath}: {ex.Message}");
        }
    }
}

