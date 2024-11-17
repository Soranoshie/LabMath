namespace LabMath.ArchiverLab;

public class ArchiverLab
{
    public static void Execute()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var currentFile = Path.Combine(currentDirectory, "numbers.txt");
        var encodedFilePath = Path.Combine(currentDirectory, "encoded.txt");
        var decodedFilePath = Path.Combine(currentDirectory, "decoded.txt");

        // Чтение строки из файла
        var symbols = StringOperations.ReadStringFromFile(currentFile);
        Console.WriteLine($"Read string from {currentFile}");

        // Кодируем последовательность символов
        var encoded = RleEncoder.Encode(symbols);
        StringOperations.WriteStringToFile(encodedFilePath, encoded);
        Console.WriteLine($"Encoded data saved to {encodedFilePath}");

        // Декодируем последовательность символов
        var decoded = RleEncoder.Decode(encoded);
        StringOperations.WriteStringToFile(decodedFilePath, decoded);
        Console.WriteLine($"Decoded data saved to {decodedFilePath}");

        // Подсчет частоты символов
        var frequencies = Indicators.CountFrequencies(symbols);
        var totalSymbols = symbols.Length;
        var uniqueSymbols = frequencies.Count;

        // Расчет H max
        var entropy = Indicators.CalculateEntropy(uniqueSymbols);
        Console.WriteLine($"Entropy (H max): {entropy}");

        // Расчет средней длины символа
        var averageLength = Indicators.CalculateAverageLength(frequencies, totalSymbols, encoded);
        Console.WriteLine($"Average length (L среднее): {averageLength}");

        // Расчет коэффициента статистического сжатия
        var compressionCoefficient = Indicators.CalculateCompressionCoefficient(entropy, averageLength);
        Console.WriteLine($"Compression coefficient (K cc): {compressionCoefficient}");
    }
}