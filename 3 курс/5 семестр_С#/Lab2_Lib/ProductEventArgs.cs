namespace LibProduct
{
    public sealed class ProductEventArgs
        : EventArgs
    {
        public ProductEventArgs(string message, string type)
        {
            Message = message;
            Type = type;
        }
        public string Message { get; }
        public string Type { get; }
    }
}