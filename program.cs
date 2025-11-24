using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConfigValidatorTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Config File Validator ===");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run <config.json>");
                return;
            }

            string path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine($"Error: File not found -> {path}");
                return;
            }

            try
            {
                string jsonText = File.ReadAllText(path);
                JObject config = JObject.Parse(jsonText);

                var result = ConfigValidator.Validate(config);

                Console.WriteLine("\nValidation Results:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"❌ {error}");
                }

                if (result.Errors.Count == 0)
                    Console.WriteLine("✅ Config file is valid!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
