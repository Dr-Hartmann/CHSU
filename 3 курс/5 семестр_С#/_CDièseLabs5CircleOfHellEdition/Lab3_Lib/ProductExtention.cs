namespace LibShowcase
{
    public static class ProductExtention
    {
        public static void UpdateProductBarcode<T>(this T product, IShowcase<T> showcase) where T : class, IProduct
        {
            if (product is null) return;
            product.Barcode.Text = $"{product.ID}{showcase.ID}{showcase?.GetProductIndexById(product.ID)}";
        }

        public static void DisposeAll<T>(this IEnumerable<T> seq) where T : IDisposable
        {
            foreach (T item in seq)
            {
                item?.Dispose();
            }
        }
        public static string OutputListOfProducts<T>(this Showcase<T> showcase) where T : class, IProduct
        {
            var outPut = new StringBuilder();
            if (!showcase.GetContainer().Any())
            {
                foreach (var item in showcase.GetContainer())
                {
                    outPut.Append($"--< {showcase.GetProductIndexById(item.ID) + 1} >--. {item.Name} - {item.ID}");
                }
            }
            return outPut.ToString();
        }

    }
}