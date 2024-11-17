namespace LabMath;

public class StringOperations
{
    public static string ReadStringFromFile(string filePath)
    {
        // Читаем всю строку из файла
        return File.ReadAllText(filePath);
    }

    public static void WriteStringToFile(string filePath, string content)
    {
        // Записываем декодированную строку в файл
        File.WriteAllText(filePath, content);
    }
}