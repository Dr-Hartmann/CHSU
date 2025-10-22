namespace LibBarcode
{
    public interface IBarcode
    {
        string Text { get; set; }
        string BarCode { get; }
        string Full { get; }
        BarcodeType BarcodeType { get; set; }
    }
}