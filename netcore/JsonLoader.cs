using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class JsonLoader
{
    private readonly string _jsonFilePath;

    public JsonLoader(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    public CongestTaxConfigSet LoadConfig()
    {
        string jsonContent = ReadJsonFile();
        return JsonConvert.DeserializeObject<CongestTaxConfigSet>(jsonContent);
    }

    private string ReadJsonFile()
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", _jsonFilePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"The specified JSON file {_jsonFilePath} does not exist.");
            }

            return File.ReadAllText(fullPath);
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed (logging, rethrow, etc.)
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            throw;
        }
    }
}
