# Лабораторная работа: Алгоритм шифрования Эль-Гамаля

## Описание проекта
Данная лабораторная работа посвящена реализации криптографического алгоритма Эль-Гамаля, который является одним из наиболее известных асимметричных алгоритмов шифрования. Алгоритм использует пары публичного и приватного ключей для обеспечения конфиденциальности передаваемой информации. В рамках проекта реализовано шифрование и дешифрование текста с помощью алгоритма Эль-Гамаля.

## Содержание проекта
Проект состоит из следующих основных компонентов:
- **Алгоритм Эль-Гамаля** для шифрования и дешифрования данных.
- Модуль для работы с числами по модулю, реализующий вычисление по модулю с использованием простых типов данных (`long`).
- Основной класс для взаимодействия с пользователем, включая генерацию ключей и выполнение шифрования/дешифрования.

## Структура проекта
- **Program.cs** — точка входа для запуска программы.
- **ElGamalCipher.cs** — класс, реализующий алгоритм Эль-Гамаля, включая шифрование и дешифрование.
- **KeyGeneration.cs** — модуль для генерации публичных и приватных ключей.
- **CryptoOperations.cs** — модуль для выполнения операций по модулю, включая возведение в степень по модулю.

## Демонстрация работы
Перед запуском программы создается файл `cryptoinput.txt` в директории `LabMath\bin\Debug\net8.0`, содержащий текст, который будет зашифрован с помощью алгоритма Эль-Гамаля. Программа затем шифрует этот текст и сохраняет его в файл `cryptoencoded.txt`, а также расшифровывает обратно и сохраняет результат в файл `cryptodecoded.txt`.

### Шаги выполнения программы:
1. **Чтение исходных данных**: Программа считывает строку из файла `cryptoinput.txt`.
2. **Шифрование данных**: Исходная строка шифруется с использованием алгоритма Эль-Гамаля, и результат записывается в файл `cryptoencoded.txt`.
3. **Дешифрование данных**: Зашифрованная строка расшифровывается, и результат записывается в файл `cryptodecoded.txt`.

### Пример результатов:
**Исходные данные: Hello crypro cipher v1.0!**
**Зашифрованные данные (вывод программы): 122,164,183,183,81,197,232,236,255,47,236,81,197,232,28,47,62,164,236,197,100,133,235,167,163**
**Дешифрованные данные (результат дешифрования): Hello crypro cipher v1.0!**

## Объяснение работы алгоритма

Алгоритм Эль-Гамаля работает следующим образом:
- Для шифрования используется публичный ключ, который состоит из простого числа `p`, основания `g`, и значения `y = g^x mod p`, где `x` — это приватный ключ.
- Для шифрования сообщения каждый символ строки шифруется с использованием случайного числа `k`, а зашифрованный символ вычисляется по формуле:
  \[
  E(m) = (g^k \cdot m) \mod p
  \]
  где `m` — это числовое представление символа.
- Для дешифрования используется приватный ключ `x` и результат шифрования, что позволяет восстановить исходное сообщение.