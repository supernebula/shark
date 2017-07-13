using System.Collections.Generic;

namespace Plunder.Ioc
{
    public interface IIocServiceGetter
    {
        T GetService<T>();

        IEnumerable<T> GetServices<T>();
    }
}
