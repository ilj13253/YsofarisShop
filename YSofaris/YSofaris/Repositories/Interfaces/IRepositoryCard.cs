using SOFARI;

namespace YSofaris.Repositories.Interfaces
{
    public interface IRepositoryCard
    {
        decimal Price { get; }
        IEnumerable<Product> Get();
        IEnumerable<int> Ids();
        void Add(int id, Product product);
        void Remove(int id);
        void Clear();
    }
}
