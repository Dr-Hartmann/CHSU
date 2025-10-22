namespace LibShowcase
{
    public class Showcase<T> :
        IShowcase<T>
        where T : class, IProduct/*, new()*/
    {
        private const string SYSTEM_MESSAGE = "Системное сообщение";
        private T[] _container;
        /// моё
        public event Action<Showcase<T>, ShowcaseEventArgs> Notify;

        private event Action<Showcase<T>> IdChanged;

        private Showcase(int amount) => _container = new T[Length = amount];
        public static implicit operator Showcase<T>(int amount) => new(amount);
        public static implicit operator Showcase<T>((int amount, int id) turple) => new(turple.amount) { ID = turple.id };

        public T this[int i]
        {
            get
            {
                if (i < Length && _container[i] is not null)
                {
                    IdChanged -= _container[i].UpdateProductBarcode;
                    _container[i].ProductIdChanged -= Showcase_ProductIdChanged;
                    var tmp = _container[i];
                    _container[i] = null;
                    return tmp;
                }
                else if (i >= Length)
                {
                    Notify?.Invoke(this, new($"Индекс {i} выходит за границы массива {Length}, невозможно извлечь.", "Ошибка: 'индексатор'"));
                    return null;
                }
                else
                {
                    Notify?.Invoke(this, new($"Пустой элемент, невозможно извлечь.", "Ошибка: 'индексатор'"));
                    return null;
                }
            }
            set
            {
                if (i < Length)
                {
                    _container[i] = value;

                    if (value is null) return;

                    IdChanged += _container[i].UpdateProductBarcode;
                    _container[i].ProductIdChanged += Showcase_ProductIdChanged;
                    _container[i].UpdateProductBarcode(this);
                }
                else
                {
                    Notify?.Invoke(this, new($"Индекс {i} выходит за границы массива {Length}, невозможно присвоить.", "Ошибка: 'индексатор'"));
                }
            }
        }

        private void Showcase_ProductIdChanged(object sender, ProductEventArgs e)
        {
            if (sender is T item)
            {
                item.UpdateProductBarcode(this);
                Console.WriteLine($"ID изменён {item.ID}");
            }
        }

        public int Length { get; }

        public int ID { get; set; }

        public void Push(T product)
        {
            for (var i = 0; i < Length; ++i)
            {
                foreach (var item in _container)
                {
                    if(item == product)
                    {
                        Notify?.Invoke(this, new("Товар уже добавлен, невозможно добавить.", "Ошибка: 'Push1'"));
                        return;
                    }
                }
                if (_container[i] is null)
                {
                    this[i] = product;
                    return;
                }
            }
            Notify?.Invoke(this, new("Витрина переполнена, невозможно добавить.", "Ошибка: 'Push1'"));
        }

        public void Push(T product, int index)
        {
            if (index < Length)
            {
                foreach (var item in _container)
                {
                    if (item == product)
                    {
                        Notify?.Invoke(this, new("Товар уже добавлен, невозможно добавить.", "Ошибка: 'Push1'"));
                        return;
                    }
                }
                this[index] = product;
                return;
            }
            Notify?.Invoke(this, new($"Неверный индекс {index}, невозможно добавить.", "Ошибка: 'Push2'"));
        }

        public T DeleteLast()
        {
            T product = null;
            for (int i = Length - 1; i > 0; --i)
            {
                if (_container[i] != null) product = this[i];
            }
            return product;
        }

        public void DeleteAll()
        {
            for (int i = Length - 1; i > 0; --i)
            {
                if (_container[i] is not null)
                {
                    this[i] = null;
                }
            }
        }

        public T DeleteByIndex(int index)
        {
            T product = null;
            if (index < Length)
            {
                product = this[index];
            }
            else
            {
                Notify?.Invoke(this, new($"Неверный индекс {index}, невозможно удалить.", "Ошибка: 'DeleteByIndex'"));
            }
            return product;
        }

        private int GetProductIndexBy(Func<T, bool> predicate)
        {
            int i = 0;
            for (; i < Length; ++i)
            {
                if (predicate(_container[i])) return i;
            }
            return -1;
        }
        public int GetProductIndexById(int id) => GetProductIndexBy((a) => a?.ID == id);
        public int GetProductIndexByName(string name) => GetProductIndexBy((a) => a?.Name == name);

        private void OrderBy(Func<T, T, int> predicate)
        {
            Console.WriteLine("СРавнение");
            Array.Sort(_container, (a, b) =>
            {
                if (a is null) return -1;
                if (b is null) return -1;
                if (a == b) return 0;
                return predicate(a, b);
                //TODO
            });
            UpdateAllProductsInShowcase();
        }
        public void OrderByName() => OrderBy((a, b) => a.Name.CompareTo(b.Name));
        public void OrderById() => OrderBy((a, b) => a.ID.CompareTo(b.ID));

        public override string ToString()
        {
            var outPut = new StringBuilder();
            foreach (var item in _container)
            {
                if (item is not null)
                {
                    outPut.AppendLine(item.ToString() + "\n");
                }
            }
            return outPut.ToString();
        }

        public string OutputShowcase()
        {
            if (_container is null)
            {
                Notify?.Invoke(this, new("Витрина, что называется, пуста.", SYSTEM_MESSAGE));
                return "Витрина, что называется, пуста.";
            }

            var outPut = new StringBuilder();
            foreach (var item in _container)
            {
                if (item is not null)
                {
                    outPut.Append($"--< {GetProductIndexById(item.ID) + 1} >--\n{item.ToString()}\n");
                }
            }
            return outPut.ToString();
        }

        public void UpdateAllProductsInShowcase()
        {
            foreach (var item in _container)
            {
                item.UpdateProductBarcode(this);
            }
        }
        public T[] GetContainer() => _container;

        private bool disposed = false;
        public void Dispose()
        {
            DeleteAll();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;
            if (Notify != null)
            {
                foreach (var item in Notify.GetInvocationList())
                {
                    Notify -= item as Action<IShowcase<T>, ShowcaseEventArgs>;
                }
            }
            disposed = true;
        }
        ~Showcase()
        {
            Dispose(false);
        }
    }
}