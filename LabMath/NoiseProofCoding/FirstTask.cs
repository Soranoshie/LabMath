namespace LabMath.NoiseProofCoding;

public class FirstTask
{
    public static void Do()
    {
        // Задание №1
        int[,] H = {
            {0, 1, 0, 1},
            {1, 0, 1, 1},
            {1, 0, 1, 0},
            {1, 0, 0, 1},
            {0, 0, 1, 1}
        };
        
        int[] info = { 1, 0, 1, 1, 1 };
        int[] codeword = GenerateCodeword(info, H);
        
        Console.WriteLine("Избыточный код: " + string.Join("", codeword));
        CheckCodeword(codeword, H);
    }
    
    static int[] GenerateCodeword(int[] info, int[,] H)
    {
        int k = info.Length; // Количество информационных битов
        int n = H.GetLength(0); // Количество проверочных битов
        int[] codeword = new int[k + n];

        // Копируем информационные биты в кодовое слово
        Array.Copy(info, codeword, k);

        // Вычисление проверочных битов
        for (int i = 0; i < n; i++)
        {
            int parity = 0;
            for (int j = 0; j < k; j++)
            {
                parity ^= (info[j] & H[i, j]);
            }
            codeword[k + i] = parity;
        }

        return codeword;
    }

    static void CheckCodeword(int[] codeword, int[,] H)
    {
        int n = codeword.Length;
        int k = H.GetLength(0);
        int[] syndrome = new int[n - k];

        for (int i = 0; i < n - k; i++)
        {
            int parity = 0;
            for (int j = 0; j < n; j++)
            {
                parity ^= (codeword[j] & H[j % k, i]);
            }

            syndrome[i] = parity;
        }

        bool errorDetected = false;
        foreach (var bit in syndrome)
        {
            if (bit != 0)
            {
                errorDetected = true;
                break;
            }
        }

        if (!errorDetected)
        {
            Console.WriteLine("Кодовая комбинация принята без ошибок.");
        }
        else
        {
            Console.WriteLine("Обнаружена ошибка в кодовой комбинации. Синдром: " + string.Join("", syndrome));
            // Для простоты реализации нахождение однократной ошибки
            for (int i = 0; i < n; i++)
            {
                int[] testCodeword = (int[])codeword.Clone();
                testCodeword[i] ^= 1; // Flip bit

                int[] testSyndrome = new int[n - k];
                for (int j = 0; j < n - k; j++)
                {
                    int parity = 0;
                    for (int l = 0; l < n; l++)
                    {
                        parity ^= (testCodeword[l] & H[l % k, j]);
                    }

                    testSyndrome[j] = parity;
                }

                bool corrected = true;
                foreach (var bit in testSyndrome)
                {
                    if (bit != 0)
                    {
                        corrected = false;
                        break;
                    }
                }

                if (corrected)
                {
                    Console.WriteLine("Ошибка в разряде: " + i);
                    return;
                }
            }
        }
    }
}