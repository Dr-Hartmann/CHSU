namespace LibShowcase
{
    public interface IShowcase<T> :
        IDisposable
        where T : class, IProduct
    {
        T this[int i] { get; set; }
        int Length { get; }
        int ID { get; set; }
        void Push(T product);
        void Push(T product, int index);
        T DeleteLast();
        T DeleteByIndex(int index);
        int GetProductIndexById(int id);
        int GetProductIndexByName(string name);
        void OrderByName();
        void OrderById();
        string OutputShowcase();
        void UpdateAllProductsInShowcase();
    }
}