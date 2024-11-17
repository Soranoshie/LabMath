namespace LabMath.ArchiverLab;

// Класс с методами, вычисляющие необходимые показатели
public static class Indicators
{
    // Подсчет частоты символов
    public static Dictionary<char, int> CountFrequencies(string input)
    {
        var frequencyDict = new Dictionary<char, int>();

        foreach (var c in input)
        {
            if (frequencyDict.ContainsKey(c))
                frequencyDict[c]++;
            else
                frequencyDict[c] = 1;
        }

        return frequencyDict;
    }
    
    // Вычисление энтропии (H max)
    public static double CalculateEntropy(int uniqueSymbolsCount)
    {
        return Math.Log2(uniqueSymbolsCount);
    }

    // Вычисление средней длины символа
    public static double CalculateAverageLength(Dictionary<char, int> frequencies, int totalSymbols, string encoded)
    {
        var totalEncodedLength = encoded.Length; // Длина закодированной строки
        var averageLength = 0.0;

        foreach (var symbol in frequencies)
        {
            var probability = (double)symbol.Value / totalSymbols;
            var symbolEncodedLength = encoded.Count(c => c == symbol.Key); // Подсчет длины закодированного символа
            averageLength += probability * symbolEncodedLength;
        }

        return averageLength;
    }

    // Вычисление коэффициента сжатия
    public static double CalculateCompressionCoefficient(double entropy, double averageLength)
    {
        return entropy / averageLength;
    }
}