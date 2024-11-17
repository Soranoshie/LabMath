namespace LabMath.CryptographyLab;

public class CryptographyLab
{
    public static void Execute()
    {
        // Инициализация значений: простое число p, генератор g, секретный ключ x и случайное число k
        int p, g = 50, x = 15, k = 20;
        p = 257; // Значение p должно быть простым, в данном случае задаётся вручную

        // Проверка, является ли p простым числом
        if (ElGamalCipher.IsPrime(p))
        {
            // Пути к файлам для ввода и вывода данных
            var inputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cryptoinput.txt");
            var encryptedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cryptoencoded.txt");
            var decryptedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cryptodecoded.txt");

            // Чтение данных из входного файла
            var data = File.ReadAllText(inputFilePath);

            // Шифрование данных и запись результата в файл
            var (encryptedData, a) = ElGamalCipher.Encrypt(data, p, g, x, k);
            File.WriteAllText(encryptedFilePath, string.Join(",", encryptedData));
            Console.WriteLine($"\nEncrypted data saved to {encryptedFilePath}");

            // Расшифровка данных и запись результата в файл
            var decryptedData = ElGamalCipher.Decrypt(encryptedData, a, p, x);
            File.WriteAllText(decryptedFilePath, decryptedData);
            Console.WriteLine($"\nDecrypted data saved to {decryptedFilePath}");
        }
        else
        {
            // Сообщение, если p не является простым числом
            Console.WriteLine("\nThis number is not prime. Please try again.");
        }
    }
}