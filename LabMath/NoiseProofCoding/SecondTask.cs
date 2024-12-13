namespace LabMath.NoiseProofCoding;

public class SecondTask
{
    public static void Do()
    {
        // Задание №2 - Код Хэмминга
        // Для создания кода, способного закодировать 18 кодовых комбинаций,
        // исправляющего одиночные и двойные ошибки, можно использовать код Хэмминга.
        // Этот код способен исправлять одиночные и обнаруживать двойные ошибки.
        // Для 18 кодовых комбинаций нам потребуется k=5 информационных битов
        // (так как 2^5=32>18), и соответственно r=5 контрольных битов
        // (для 32 комбинаций с 1 битом на проверку ошибок).
        int[] hammingData = [1, 0, 1, 1, 1];
        var hammingCodeword = GenerateHammingCode(hammingData);

        Console.WriteLine("Код Хэмминга: " + string.Join("", hammingCodeword));
        CheckAndCorrectHammingCode(hammingCodeword);

        // Проверка и исправление (вводим ошибку для проверки)
        hammingCodeword[2] ^= 1; // Ввод ошибки
        Console.WriteLine("Код Хэмминга с ошибкой: " + string.Join("", hammingCodeword));
        CheckAndCorrectHammingCode(hammingCodeword);
        Console.WriteLine("Исправленный код Хэмминга: " + string.Join("", hammingCodeword));
    }

    private static int[] GenerateHammingCode(int[] data)
    {
        var infoBitsCount = data.Length;
        var verificationBitsCount = 0;
        
        while (Math.Pow(2, verificationBitsCount) < (infoBitsCount + verificationBitsCount + 1))
            verificationBitsCount++;

        var codeword = new int[infoBitsCount + verificationBitsCount];
        var infoBitsCounter = 0;
        var verificationBitsCounter = 0;

        // Расставляем информационные биты и места для проверочных битов
        for (var i = 1; i <= codeword.Length; i++)
        {
            if (Math.Abs(Math.Pow(2, infoBitsCounter) - i) < 1E-6)
            {
                codeword[i - 1] = 0;
                infoBitsCounter++;
            }
            else
            {
                codeword[i - 1] = data[verificationBitsCounter];
                verificationBitsCounter++;
            }
        }

        // Вычисляем проверочные биты
        for (var i = 0; i < verificationBitsCount; i++)
        {
            var parityPosition = (int)Math.Pow(2, i);
            var parity = 0;

            for (var j = parityPosition - 1; j < codeword.Length; j += 2 * parityPosition)
                for (var k = 0; k < parityPosition && j + k < codeword.Length; k++)
                    parity ^= codeword[j + k];

            codeword[parityPosition - 1] = parity;
        }

        return codeword;
    }

    private static void CheckAndCorrectHammingCode(int[] codeword)
    {
        var verificationBitsTotalCount = 0;
        while (Math.Pow(2, verificationBitsTotalCount) < codeword.Length)
            verificationBitsTotalCount++;

        var errorPosition = 0;

        // Вычисляем синдром
        for (var i = 0; i < verificationBitsTotalCount; i++)
        {
            var parityPosition = (int)Math.Pow(2, i);
            var parity = 0;

            for (var j = parityPosition - 1; j < codeword.Length; j += 2 * parityPosition)
                for (var k = 0; k < parityPosition && j + k < codeword.Length; k++)
                    parity ^= codeword[j + k];

            if (parity != 0)
                errorPosition += parityPosition;
        }

        if (errorPosition != 0)
        {
            Console.WriteLine($"Ошибка в позиции: {errorPosition}");
            codeword[errorPosition - 1] ^= 1;
        }
        else
            Console.WriteLine("Кодовая комбинация принята без ошибок.");
    }
}