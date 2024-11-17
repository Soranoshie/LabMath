namespace LabMath.QrCodeLab;

public class QrCodeLab
{
    public static void Execute(string data = "https://translate.yandex.ru/")
    {
        var barcodeService = new BarcodeService();
        
        barcodeService.GenerateQRCode(data);
        barcodeService.GenerateBarcode(data);
        
    }
}