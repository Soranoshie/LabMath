namespace LabMath.NoiseProofCoding;

public class ThirdTask
{
    public static void Do()
    {
        // Задание №3
        int[] generatorPoly = [1, 0, 0, 1, 1, 1];
        int[] data = [1, 0, 0, 1, 1, 1, 0, 1, 0];
        var cyclicCodeword = GenerateCyclicCodeword(data, generatorPoly);
        
        Console.WriteLine("Циклический код: " + string.Join("", cyclicCodeword));
        CheckCyclicCodeword(cyclicCodeword, generatorPoly);
    }

    private static int[] GenerateCyclicCodeword(int[] data, int[] generatorPoly)
    {
        var dataLength = data.Length;
        var polyLength = generatorPoly.Length;
        var codeword = new int[dataLength + polyLength - 1];
        Array.Copy(data, codeword, dataLength);

        for (var i = 0; i < dataLength; i++)
        {
            if (codeword[i] != 1)
                continue;
            
            for (var j = 0; j < polyLength; j++)
                codeword[i + j] ^= generatorPoly[j];
        }

        for (var i = 0; i < dataLength; i++)
            codeword[i] = data[i];

        return codeword;
    }


    private static void CheckCyclicCodeword(int[] codeword, int[] generatorPoly)
    {
        var codeLength = codeword.Length;
        var polyLength = generatorPoly.Length;
        var remainder = new int[polyLength - 1];
        var tempCodeword = (int[])codeword.Clone();

        for (var i = 0; i < codeLength - polyLength + 1; i++)
        {
            if (tempCodeword[i] != 1)
                continue;
            
            for (var j = 0; j < polyLength; j++)
                tempCodeword[i + j] ^= generatorPoly[j];
        }

        Array.Copy(tempCodeword, codeLength - polyLength + 1, remainder, 0, polyLength - 1);

        var errorDetected = remainder.Any(bit => bit != 0);

        if (!errorDetected)
            Console.WriteLine("Циклический код принят без ошибок.");
        else
        {
            Console.WriteLine("Обнаружена ошибка в циклическом коде. Остаток: " + string.Join("", remainder));
            // Для простоты реализации нахождение однократной ошибки
            for (var i = 0; i < codeLength; i++)
            {
                var testCodeword = (int[])codeword.Clone();
                testCodeword[i] ^= 1;

                var testRemainder = new int[polyLength - 1];
                Array.Copy(testCodeword, tempCodeword, codeLength);

                for (var j = 0; j < codeLength - polyLength + 1; j++)
                {
                    if (tempCodeword[j] != 1)
                        continue;
                    
                    for (var k = 0; k < polyLength; k++)
                        tempCodeword[j + k] ^= generatorPoly[k];
                }

                Array.Copy(tempCodeword, codeLength - polyLength + 1, testRemainder, 0, polyLength - 1);

                var corrected = testRemainder.All(bit => bit == 0);

                if (!corrected)
                    continue;
                
                Console.WriteLine("Ошибка в разряде: " + i);
                return;
            }
        }
    }
}