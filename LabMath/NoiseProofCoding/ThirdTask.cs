namespace LabMath.NoiseProofCoding;

public class ThirdTask
{
    public static void Do()
    {
        // Задание №3
        int[] generatorPoly = { 1, 0, 0, 1, 1, 1 };
        int[] data = { 1, 0, 0, 1, 1, 1, 0, 1, 0 };
        int[] cyclicCodeword = GenerateCyclicCodeword(data, generatorPoly);
        
        Console.WriteLine("Циклический код: " + string.Join("", cyclicCodeword));
        CheckCyclicCodeword(cyclicCodeword, generatorPoly);
    }
    
    static int[] GenerateCyclicCodeword(int[] data, int[] generatorPoly)
    {
        int dataLength = data.Length;
        int polyLength = generatorPoly.Length;
        int[] codeword = new int[dataLength + polyLength - 1];
        Array.Copy(data, codeword, dataLength);

        for (int i = 0; i < dataLength; i++)
        {
            if (codeword[i] == 1)
            {
                for (int j = 0; j < polyLength; j++)
                {
                    codeword[i + j] ^= generatorPoly[j];
                }
            }
        }

        for (int i = 0; i < dataLength; i++)
        {
            codeword[i] = data[i];
        }

        return codeword;
    }


    static void CheckCyclicCodeword(int[] codeword, int[] generatorPoly)
    {
        int codeLength = codeword.Length;
        int polyLength = generatorPoly.Length;
        int[] remainder = new int[polyLength - 1];
        int[] tempCodeword = (int[])codeword.Clone();

        for (int i = 0; i < codeLength - polyLength + 1; i++)
        {
            if (tempCodeword[i] == 1)
            {
                for (int j = 0; j < polyLength; j++)
                {
                    tempCodeword[i + j] ^= generatorPoly[j];
                }
            }
        }

        Array.Copy(tempCodeword, codeLength - polyLength + 1, remainder, 0, polyLength - 1);

        bool errorDetected = false;
        foreach (var bit in remainder)
        {
            if (bit != 0)
            {
                errorDetected = true;
                break;
            }
        }

        if (!errorDetected)
        {
            Console.WriteLine("Циклический код принят без ошибок.");
        }
        else
        {
            Console.WriteLine("Обнаружена ошибка в циклическом коде. Остаток: " + string.Join("", remainder));
            // Для простоты реализации нахождение однократной ошибки
            for (int i = 0; i < codeLength; i++)
            {
                int[] testCodeword = (int[])codeword.Clone();
                testCodeword[i] ^= 1; // Flip bit

                int[] testRemainder = new int[polyLength - 1];
                Array.Copy(testCodeword, tempCodeword, codeLength);

                for (int j = 0; j < codeLength - polyLength + 1; j++)
                {
                    if (tempCodeword[j] == 1)
                    {
                        for (int k = 0; k < polyLength; k++)
                        {
                            tempCodeword[j + k] ^= generatorPoly[k];
                        }
                    }
                }

                Array.Copy(tempCodeword, codeLength - polyLength + 1, testRemainder, 0, polyLength - 1);

                bool corrected = true;
                foreach (var bit in testRemainder)
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