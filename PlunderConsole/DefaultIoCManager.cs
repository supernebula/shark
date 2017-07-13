using Autofac;
using Plunder.Ioc;
using System;
using System.Collections.Generic;

namespace Plunder
{
    public class DefaultIocManager : IIocManager
    {

        private IContainer _container;


        private Func<IServiceProvider> _serviceProviderthunk;

        public DefaultIocManager(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolve dependency injection service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            throw new NotImplementedException();
        }
    }
}
