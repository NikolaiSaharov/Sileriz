using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

// Пример модели "Figure"
public class Figure
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Figure(string name, int width, int height)
    {
        Name = name;
        Width = width;
        Height = height;
    }
}

// Класс для чтения\сохранения файла
public class FileHandler
{
    // Загрузка данных из файла
    public static void LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден!");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }

    // Сохранение данных в файл
    public static void SaveData(string filePath, object data)
    {
        string extension = Path.GetExtension(filePath).ToLower();

        switch (extension)
        {
            case ".txt":
                SaveAsTxt(filePath, data);
                break;
            case ".json":
                SaveAsJson(filePath, data);
                break;
            case ".xml":
                SaveAsXml(filePath, data);
                break;
            default:
                Console.WriteLine("Неподдерживаемый формат файла!");
                break;
        }
    }

    private static void SaveAsTxt(string filePath, object data)
    {
        File.WriteAllText(filePath, data.ToString());
        Console.WriteLine("Файл успешно сохранен в формате txt!");
    }

    private static void SaveAsJson(string filePath, object data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, jsonData);
        Console.WriteLine("Файл успешно сохранен в формате json!");
    }

    private static void SaveAsXml(string filePath, object data)
    {
        XmlSerializer serializer = new XmlSerializer(data.GetType());

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, data);
        }

        Console.WriteLine("Файл успешно сохранен в формате xml!");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к файлу:");
        string filePath = Console.ReadLine();

        FileHandler.LoadData(filePath);

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.F1)
            {
                FileHandler.SaveData(filePath, new Figure("Прямоугольник", 10, 5));
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }
}