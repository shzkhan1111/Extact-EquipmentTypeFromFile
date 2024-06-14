using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExtractEquipmentType
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string inputDirectoryPath = "C:\\Users\\shahzaib.ahmed.DESKTOP-EB9K6GQ\\Downloads\\New folder (2)";  // Update this to your directory path
            string outputFilePath = "C:\\Users\\shahzaib.ahmed.DESKTOP-EB9K6GQ\\Desktop\\extracted_equipment_types.txt";

            List<string> equipmentTypes = new List<string>();

            // Ensure the directory exists
            if (!Directory.Exists(inputDirectoryPath))
            {
                Console.WriteLine($"The directory {inputDirectoryPath} does not exist.");
                return;
            }

            // Get all .jsonl files in the directory
            string[] files = Directory.GetFiles(inputDirectoryPath, "*.jsonl");

            foreach (string file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        try
                        {
                            // Parse the JSON line
                            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(line);
                            if (jsonObject.ContainsKey("EquipmentType"))
                            {
                                equipmentTypes.Add(jsonObject["EquipmentType"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing line in file {file}: {ex.Message}");
                        }
                    }
                }
            }

            // Write the extracted equipment types to the output file
            await File.WriteAllLinesAsync(outputFilePath, equipmentTypes);

            Console.WriteLine($"Extracted equipment types have been written to {outputFilePath}");
        }
    }
}
