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
        int[] hammingData = { 1, 0, 1, 1, 1 };
        int[] hammingCodeword = GenerateHammingCode(hammingData);

        Console.WriteLine("Код Хэмминга: " + string.Join("", hammingCodeword));
        CheckAndCorrectHammingCode(hammingCodeword);

        // Проверка и исправление (вводим ошибку для проверки)
        hammingCodeword[2] ^= 1; // Ввод ошибки
        Console.WriteLine("Код Хэмминга с ошибкой: " + string.Join("", hammingCodeword));
        CheckAndCorrectHammingCode(hammingCodeword);
        Console.WriteLine("Исправленный код Хэмминга: " + string.Join("", hammingCodeword));
    }

    static int[] GenerateHammingCode(int[] data)
    {
        int m = data.Length;
        int r = 0;

        while (Math.Pow(2, r) < (m + r + 1))
        {
            r++;
        }

        int[] codeword = new int[m + r];
        int p = 0;
        int o = 0;

        for (int i = 1; i <= codeword.Length; i++)
        {
            if (Math.Pow(2, p) == i)
            {
                codeword[i - 1] = 0;
                p++;
            }
            else
            {
                codeword[i - 1] = data[o];
                o++;
            }
        }

        for (int i = 0; i < r; i++)
        {
            int parityPosition = (int)Math.Pow(2, i);
            int parity = 0;

            for (int j = parityPosition - 1; j < codeword.Length; j += 2 * parityPosition)
            {
                for (int k = 0; k < parityPosition && j + k < codeword.Length; k++)
                {
                    parity ^= codeword[j + k];
                }
            }

            codeword[parityPosition - 1] = parity;
        }

        return codeword;
    }

    static void CheckAndCorrectHammingCode(int[] codeword)
    {
        int r = 0;
        while (Math.Pow(2, r) < codeword.Length)
        {
            r++;
        }

        int errorPosition = 0;

        for (int i = 0; i < r; i++)
        {
            int parityPosition = (int)Math.Pow(2, i);
            int parity = 0;

            for (int j = parityPosition - 1; j < codeword.Length; j += 2 * parityPosition)
            {
                for (int k = 0; k < parityPosition && j + k < codeword.Length; k++)
                {
                    parity ^= codeword[j + k];
                }
            }

            if (parity != 0)
            {
                errorPosition += parityPosition;
            }
        }

        if (errorPosition != 0)
        {
            Console.WriteLine($"Ошибка в позиции: {errorPosition}");
            codeword[errorPosition - 1] ^= 1;
        }
        else
        {
            Console.WriteLine("Кодовая комбинация принята без ошибок.");
        }
    }
}