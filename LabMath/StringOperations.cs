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
    
    public static int[] TextToNumbers(string text)
    {
        // Преобразование текста в массив чисел (ASCII-коды символов)
        return text.Select(c => (int)c).ToArray();
    }

    public static string NumbersToText(int[] numbers)
    {
        // Преобразование массива чисел обратно в текст
        return new string(numbers.Select(n => (char)n).ToArray());
    }
}