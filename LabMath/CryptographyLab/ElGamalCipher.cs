namespace LabMath.CryptographyLab;

public static class ElGamalCipher
{
    public static (long[], long) Encrypt(string data, int p, int g, int x, int k)
    {
        var y = CustomModPow(g, x, p);
        var a = CustomModPow(g, k, p);
        var encryptedData = new long[data.Length];

        for (var i = 0; i < data.Length; i++)
            encryptedData[i] = (CustomModPow((int)y, k, p) * data[i]) % p;

        return (encryptedData, a);
    }

    public static string Decrypt(long[] encryptedData, long a, int p, int x)
    {
        var decryptedData = string.Empty;
        var p0 = (p - 1) - x;
        var m1 = CustomModPow((int)a, p0, p);

        foreach (var b in encryptedData)
        {
            var decryptedChar = (m1 * b) % p;
            decryptedData += (char)decryptedChar;
        }

        return decryptedData;
    }

    public static bool IsPrime(int number)
    {
        if ((number & 1) == 0)
            return number == 2;

        for (var i = 3; (i * i) <= number; i += 2)
        {
            if ((number % i) == 0)
                return false;
        }

        return number != 1;
    }

    // Реализация ModPow из библиотеки "System.Numerics" с использованием простых типов данных
    private static long CustomModPow(int baseValue, int exponent, int modulus)
    {
        long result = 1;
        long baseMod = baseValue % modulus;
        while (exponent > 0)
        {
            if ((exponent & 1) == 1) // Проверка, является ли текущий бит степени 1
            {
                result = (result * baseMod) % modulus;
            }
            exponent >>= 1; // Сдвиг вправо для деления степени на 2
            baseMod = (baseMod * baseMod) % modulus; // Возведение в квадрат по модулю
        }
        return result;
    }
}