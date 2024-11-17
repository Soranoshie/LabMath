namespace LabMath.ArchiverLab;

public class RleEncoder
{
    // Кодирование с помощью RLE
    public static string Encode(string input)
    {
        var encoded = string.Empty;
        var count = 1;

        for (var i = 1; i <= input.Length; i++)
        {
            if (i < input.Length && input[i] == input[i - 1])
                count++;
            else
            {
                // Если символ - цифра, добавляем двоеточие между количеством и символом
                if (char.IsDigit(input[i - 1]))
                    encoded += $"{count}:{input[i - 1]}";
                else
                    encoded += $"{count}{input[i - 1]}";
                count = 1; // Сбрасываем счетчик
            }
        }

        return encoded;
    }

    // Декодирование с помощью RLE
    public static string Decode(string input)
    {
        var decoded = string.Empty;

        for (var i = 0; i < input.Length; i++)
        {
            // Предполагаем, что количество символов будет занимать одну или более цифр
            var count = 0;

            // Считываем число
            while (i < input.Length && char.IsDigit(input[i]))
            {
                count = count * 10 + (input[i] - '0'); // Преобразуем строку в число
                i++;
            }

            if (i < input.Length && input[i] == ':')
            {
                i++; // Переходим к самому символу
                decoded += new string(input[i], count);
            }
            else if (i < input.Length)
                decoded += new string(input[i], count); // Обычное декодирование символов
        }

        return decoded;
    }
    
    
}