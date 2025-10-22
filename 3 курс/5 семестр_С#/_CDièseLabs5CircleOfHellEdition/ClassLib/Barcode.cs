namespace LibBarcode
{
    public class Barcode : IBarcode
    {
        private string _text;
        private string _barcode;
        private string _full;
        public Barcode(string text)
        {
            Text = text;
        }
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _barcode = BarcodeUtilities.GetBarCode(Text);

                StringBuilder full = new();
                var outLength = BarCode.Length / BarcodeUtilities.GetBarcodeHeight();
                var firstLength = outLength / 2;
                var halfLengthInputText = Text.Length / 2;
                full.Append("".PadLeft(firstLength - halfLengthInputText, ' '));
                full.Append(Text);
                full.Append("".PadLeft(outLength - firstLength - (Text.Length - halfLengthInputText), ' '));

                _full = BarCode + full.ToString();
            }
        }
        public string BarCode => _barcode;
        public string Full => _full;

        public BarcodeType BarcodeType { get; set; } = BarcodeType.Full;

        public override string ToString()
        {
            return BarcodeType switch
            {
                BarcodeType.Text => $"{Text}",
                BarcodeType.Barcode => $"{BarCode}",
                BarcodeType.Full => $"{Full}",
                _ => "Неизвестный вывод"
            };
        }
    }
}