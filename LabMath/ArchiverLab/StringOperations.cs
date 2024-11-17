namespace LabMath.ArchiverLab;

public class StringOperations
{
    public static string ReadStringFromFile(string filePath)
    {
        return File.ReadAllText(filePath); // Читаем всю строку из файла
    }

    public static void WriteStringToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content); // Записываем декодированную строку в файл
    }
}