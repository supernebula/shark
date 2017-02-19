
namespace Plunder.Filter
{
    public interface IDuplicateFilter<in T>
    {
        bool Contains(T item);

        void Add(T item);
    }
}
