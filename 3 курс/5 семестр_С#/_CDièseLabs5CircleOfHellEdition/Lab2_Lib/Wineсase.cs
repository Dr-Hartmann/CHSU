namespace LibProduct
{
    public class Wineсase
        : Product
    {
        private int _amount;
        private int _price;

        public Wineсase(int id, string name, string series, int amount, int price)
            : this(id, name, series, amount, price, new Barcode(id.ToString())) { }

        protected Wineсase(int id, string name, string series, int amount, int price, IBarcode barcode)
            : base(id, name, barcode)
        {
            Series = series;
            Amount = amount;
            Price = price;
            Barcode.BarcodeType = BarcodeType.Full;
        }

        public string Series { get; set; }
        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    RaiseNotifyEvent(this, new($"{value} < 0, невозможно присвоить.", "Amount"));
                    return;
                }
                _amount = value;
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    RaiseNotifyEvent(this, new ProductEventArgs($"{value} < 0, невозможно присвоить.", "Price"));
                    return;
                }

                _price = value;
            }
        }
        public override string Type => "Wineсase";
        public override string ToString()
            => $"{base.ToString()}\nСерия: {Series}\nНа складе: {Amount}\nЦена: {Price}";
    }
}