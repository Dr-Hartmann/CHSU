namespace LibProduct
{
    public abstract class Product
        : IProduct
    {
        protected const string SYSTEM_MESSAGE = "Системное сообщение";
        private int _id;
        public event EventHandler<ProductEventArgs> ProductIdChanged;

        protected Product(int id, string name, IBarcode barcode)
        {
            Name = name; _id = id; Barcode = barcode;
        }

        public string Name { get; set; }
        public virtual int ID
        {
            get => _id;
            set
            {
                _id = value;
                Barcode.Text = _id.ToString();
            }
        }
        public IBarcode Barcode { get; }
        public abstract string Type { get; }
        protected void RaiseNotifyEvent(Product sender, ProductEventArgs e) => ProductIdChanged?.Invoke(sender, e);
        public override string ToString() => $"{Barcode}\n{Type}: {Name}\nID: {ID}";

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;
            if (ProductIdChanged != null)
            {
                foreach (var item in ProductIdChanged.GetInvocationList())
                {
                    ProductIdChanged -= item as EventHandler<ProductEventArgs>;
                }
            }
            disposed = true;
        }
        ~Product()
        {
            Dispose(false);
        }
    }
}