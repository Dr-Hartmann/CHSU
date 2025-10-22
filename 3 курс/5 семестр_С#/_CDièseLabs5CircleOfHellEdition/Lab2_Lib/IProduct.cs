namespace LibProduct
{
    public interface IProduct :
        IDisposable
    {
        event EventHandler<ProductEventArgs> ProductIdChanged;
        string Name { get; set; }
        int ID { get; set; }
        IBarcode Barcode { get; }
    }
}