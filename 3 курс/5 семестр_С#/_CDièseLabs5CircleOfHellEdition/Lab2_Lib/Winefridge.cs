namespace LibProduct
{
    public sealed class Winefridge
        : Wineсase, IProduct
    {
        private int _capacityOfBottle;
        private int _volume;

        public Winefridge(int id, string name, string series, int amount, int price, int capacityOfBottle, int volume)
            : base(id, name, series, amount, price, new BarcodeRecord(id.ToString()))
        {
            CapacityOfBottle = capacityOfBottle;
            Volume = volume;
            Barcode = new(ID.ToString());
            base.Barcode.BarcodeType = BarcodeType.Text;
        }
        public new BarcodeRecord Barcode { get; set; }
        public override int ID
        {
            get => base.ID;
            set => base.ID = value;
        }
        public int CapacityOfBottle
        {
            get => _capacityOfBottle;
            set
            {
                if (value < 0)
                {
                    RaiseNotifyEvent(this, new ProductEventArgs("Количество меньше 0, невозможно присвоить.", "CapacityOfBottle"));
                    return;
                }

                _capacityOfBottle = value;
            }
        }
        public int Volume
        {
            get => _volume;
            set
            {
                if (value < 0)
                {
                    RaiseNotifyEvent(this, new ProductEventArgs("Количество меньше 0, невозможно присвоить.", "Volume"));
                    return;
                }

                _volume = value;
            }
        }
        public override string Type => "Winefridge";
        public override string ToString()
            => $"{base.ToString()}\nВместимость бутылок: {CapacityOfBottle}\nОбъём: {Volume}";
    }
}