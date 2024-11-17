using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace LabMath.QrCodeLab;

public class BarcodeService
{
    private string inputFilePathQr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qr.jpg");
    private string inputFilePathBar = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcode.jpg");

    private QrCodeEncodingOptions qrCodeEncodingOptions = new()
    {
        CharacterSet = "UTF-8",
        DisableECI = true,
        Width = 250,
        Height = 250,
        ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
    };

    public void GenerateQRCode(string data)
    {
        // Создание объекта для генерации QR-кода
        var barcodeWriter = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.QR_CODE, // Выбор формата QR-кода
            Options = qrCodeEncodingOptions,
            Renderer = new BitmapRenderer()
        };

        // Генерация QR-кода и сохранение в файл
        var bitmap = barcodeWriter.Write(data);
        bitmap.Save(inputFilePathQr, System.Drawing.Imaging.ImageFormat.Png);
    }

    public void GenerateBarcode(string data)
    {
        // Создаем объект BarcodeWriter для генерации штрих-кода
        var barcodeWriter = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.CODE_128, // Устанавливаем формат штрих-кода (например, CODE_128)
            Options = qrCodeEncodingOptions,
            Renderer = new BitmapRenderer() // Указываем рендерер
        };

        // Генерируем штрих-код и получаем изображение в формате Bitmap
        var bitmap = barcodeWriter.Write(data);

        // Сохраняем изображение в файл по указанному пути
        bitmap.Save(inputFilePathBar, System.Drawing.Imaging.ImageFormat.Png);
    }
}