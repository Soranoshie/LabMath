using System.Drawing;

namespace LabMath.ArchiverLab;

public class FractalCompressor
{
    private int blockSize;

    public FractalCompressor(int blockSize)
    {
        this.blockSize = blockSize;
    }

    // Функция для сжатия изображения
    public void Compress(Bitmap image)
    {
        var width = image.Width;
        var height = image.Height;

        for (var i = 0; i < width; i += blockSize)
        {
            for (var j = 0; j < height; j += blockSize)
            {
                // Извлекаем текущий блок
                var block = GetBlock(image, i, j, blockSize, blockSize);

                // Ищем похожий блок
                var bestMatchBlock = FindSimilarBlock(image, block);

                // Сохраняем трансформацию (упрощенно: координаты блока)
                Console.WriteLine($"Block at ({i},{j}) is similar to another block.");
            }
        }
    }

    // Извлечение блока из изображения
    private Bitmap GetBlock(Bitmap image, int x, int y, int width, int height)
    {
        var block = new Bitmap(width, height);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (x + i < image.Width && y + j < image.Height)
                    block.SetPixel(i, j, image.GetPixel(x + i, y + j));
            }
        }

        return block;
    }

    // Поиск похожего блока в изображении (поиск на основе среднего значения)
    private Bitmap FindSimilarBlock(Bitmap image, Bitmap block)
    {
        int bestX = 0, bestY = 0;
        var bestDifference = double.MaxValue;

        for (var i = 0; i < image.Width - blockSize; i += blockSize)
        {
            for (var j = 0; j < image.Height - blockSize; j += blockSize)
            {
                var candidateBlock = GetBlock(image, i, j, blockSize, blockSize);
                var difference = CompareBlocks(block, candidateBlock);

                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestX = i;
                    bestY = j;
                }
            }
        }

        return GetBlock(image, bestX, bestY, blockSize, blockSize);
    }

    // Сравнение двух блоков на основе среднего значения цвета (упрощенно)
    private double CompareBlocks(Bitmap block1, Bitmap block2)
    {
        var difference = 0.0;

        for (var i = 0; i < blockSize; i++)
        {
            for (var j = 0; j < blockSize; j++)
            {
                var color1 = block1.GetPixel(i, j);
                var color2 = block2.GetPixel(i, j);

                // Вычисляем разницу в яркости пикселей
                difference += Math.Abs(color1.R - color2.R) + Math.Abs(color1.G - color2.G) + Math.Abs(color1.B - color2.B);
            }
        }

        return difference;
    }
}