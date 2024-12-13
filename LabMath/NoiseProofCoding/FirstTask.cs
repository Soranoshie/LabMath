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
        
        int[] info = [1, 0, 1, 1, 1];
        var codeword = GenerateCodeword(info, H);
        
        Console.WriteLine("Избыточный код: " + string.Join("", codeword));
        CheckCodeword(codeword, H);
    }

    private static int[] GenerateCodeword(int[] info, int[,] H)
    {
        var infoBitsCount = info.Length;
        var verificationBitsCount = H.GetLength(1);
        var codewordFullLength = infoBitsCount + verificationBitsCount;
        var codeword = new int[codewordFullLength]; // Создаем массив для кодового слова нужной длины

        // Копируем информационные биты в кодовое слово
        Array.Copy(info, codeword, infoBitsCount);

        // Вычисление проверочных битов
        for (var i = 0; i < verificationBitsCount; i++)
        {
            var parity = 0;
            
            for (var j = 0; j < infoBitsCount; j++)
                parity ^= info[j] & H[j, i]; // Индексация
            
            codeword[infoBitsCount + i] = parity;
        }

        return codeword; // Возвращаем избыточный код
    }

    private static void CheckCodeword(int[] codeword, int[,] H)
    {
        var codeWordLength = codeword.Length;
        var verificationBitsTotalCount = H.GetLength(0);
        var syndrome = new int[codeWordLength - verificationBitsTotalCount]; // Массив для хранения синдрома (проверки ошибок)

        // Вычисление синдрома
        for (var i = 0; i < codeWordLength - verificationBitsTotalCount; i++)
        {
            var parity = 0;
            
            for (var j = 0; j < codeWordLength; j++)
                parity ^= codeword[j] & H[j % verificationBitsTotalCount, i];

            syndrome[i] = parity;
        }

        // Проверка синдрома на наличие ошибок
        var errorDetected = syndrome.Any(bit => bit != 0);

        if (!errorDetected)
            Console.WriteLine("Кодовая комбинация принята без ошибок.");
        else // Если ошибка обнаружена
        {
            // Выводим синдром для диагностики ошибки
            Console.WriteLine("Обнаружена ошибка в кодовой комбинации. Синдром: " + string.Join("", syndrome));
            
            // Попытка обнаружить позицию однократной ошибки
            for (var i = 0; i < codeWordLength; i++)
            {
                var testCodeword = (int[])codeword.Clone();
                testCodeword[i] ^= 1; // Переводим бит в позицию i в противоположное состояние (перевернули бит)

                var testSyndrome = new int[codeWordLength - verificationBitsTotalCount]; // Массив для нового синдрома после изменения бита
                for (var j = 0; j < codeWordLength - verificationBitsTotalCount; j++) // Пересчитываем синдром для измененного кода
                {
                    var parity = 0; // Переменная для вычисления синдрома
                     
                    // Вычисление синдрома для измененного кодового слова
                    for (var l = 0; l < codeWordLength; l++)
                        parity ^= testCodeword[l] & H[l % verificationBitsTotalCount, j]; // Побитовая операция и с новым кодом

                    testSyndrome[j] = parity; // Сохраняем новый синдром
                }

                // Если синдром теперь равен нулю, ошибка исправлена
                var corrected = testSyndrome.All(bit => bit == 0);

                if (!corrected)
                    continue;
                
                Console.WriteLine("Ошибка в разряде: " + i);
                return;
            }
        }
    }
}