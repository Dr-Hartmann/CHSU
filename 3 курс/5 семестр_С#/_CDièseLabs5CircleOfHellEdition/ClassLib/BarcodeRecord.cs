namespace LibBarcode
{
    public record BarcodeRecord : IBarcode
    {
        private string _text;
        private string _barcode;
        private string _full;

        public BarcodeRecord(string text)
        {
            Text = text;
            _barcode = BarcodeUtilities.GetBarCode(Text);
            var bufText = $"*{Text}*";
            StringBuilder full = new StringBuilder();
            var outLength = BarCode.Length / BarcodeUtilities.GetBarcodeHeight();
            var firstLength = outLength / 2;
            var halfLengthInputText = bufText.Length / 2;
            full.Append("".PadLeft(firstLength - halfLengthInputText, ' '));
            full.Append(bufText);
            full.Append("".PadLeft(outLength - firstLength - (bufText.Length - halfLengthInputText), ' '));

            _full = BarCode + full;
        }

        public string Text { get => _text; set => _text = value; }
        public string BarCode => _barcode;
        public string Full => _full;

        public BarcodeType BarcodeType { get; set; } = BarcodeType.Full;

        public override string ToString()
        {
            return BarcodeType switch
            {
                BarcodeType.Text => $"*{_text}*",
                BarcodeType.Barcode => $"{_barcode}",
                BarcodeType.Full => $"{_full}",
                _ => "*Неизвестный вывод*"
            };
        }
    }
}