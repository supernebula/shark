using Plunder.Setting.Core.Http.Dispatcher;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace Plunder.Setting.Core.Http
{
    /// <summary>
    /// Copy form <see cref="System.Web.Http.Dispatcher.DefaultHttpControllerSelector "/>, Add namespace support
    /// </summary>
    public class NamespaceHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string NamespaceKey = "namespaces";

        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;
        private readonly HttpControllerTypeCache _controllerTypeCache;
        private readonly Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>> _controllerInfoCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHttpControllerSelector"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public NamespaceHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            if (configuration == null)
            {
                //throw Error.ArgumentNull("configuration");
                throw new ArgumentNullException("configuration");
            }

            _controllerInfoCache = new Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>>(InitializeControllerInfoCache);
            _configuration = configuration;
            _controllerTypeCache = new HttpControllerTypeCache(_configuration);
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string controllerName = GetControllerName(request);

            var @namespace = GetNamespace(request);
            var controllDescriptor = base.SelectController(request);
            if (controllDescriptor.ControllerType.Namespace.Equals(@namespace, StringComparison.CurrentCultureIgnoreCase))
                return controllDescriptor;

            
           

            if (string.IsNullOrEmpty(controllerName))
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound,$"路由无此Controller：{controllerName}"));
            }

            var controllerTypes = _controllerTypeCache.GetControllerTypes(controllerName);
            if (!controllerTypes.Any())
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound, $"无法通过API路由匹配到任何具体的控制器类型：{controllerName}"));
            }

            
            var type = controllerTypes.FirstOrDefault(e => e.Namespace.Equals(@namespace));

            if (type != null)
                controllDescriptor = new HttpControllerDescriptor(_configuration, controllerName, type);
            return controllDescriptor;
        }

        public override string GetControllerName(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                return null;
            }

            // Look up controller in route data
            string[] controllerName = null;
            object valueObj;
            if (routeData.Values.TryGetValue(ControllerKey, out valueObj))
            {
                if (valueObj is string[])
                    controllerName = (string[])valueObj;
            }
            return controllerName?.FirstOrDefault();
        }

        private string GetNamespace(HttpRequestMessage request)
        {
            var data = request.GetRouteData();
            string @namespace = null;
            object valueObj;
            if (data.Values.TryGetValue(NamespaceKey, out valueObj))
            {
                if (valueObj is string)
                    @namespace = (string)valueObj;
            }

            return @namespace;
        }

        public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllerInfoCache.Value.ToDictionary(c => c.Key, c => c.Value, StringComparer.OrdinalIgnoreCase);
        }

        private ConcurrentDictionary<string, HttpControllerDescriptor> InitializeControllerInfoCache()
        {
            var result = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
            var duplicateControllers = new HashSet<string>();
            Dictionary<string, ILookup<string, Type>> controllerTypeGroups = _controllerTypeCache.Cache;

            foreach (KeyValuePair<string, ILookup<string, Type>> controllerTypeGroup in controllerTypeGroups)
            {
                string controllerName = controllerTypeGroup.Key;

                foreach (IGrouping<string, Type> controllerTypesGroupedByNs in controllerTypeGroup.Value)
                {
                    foreach (Type controllerType in controllerTypesGroupedByNs)
                    {
                        if (result.Keys.Contains(controllerName))
                        {
                            duplicateControllers.Add(controllerName);
                            break;
                        }
                        else
                        {
                            result.TryAdd(controllerName, new HttpControllerDescriptor(_configuration, controllerName, controllerType));
                        }
                    }
                }
            }

            foreach (string duplicateController in duplicateControllers)
            {
                HttpControllerDescriptor descriptor;
                result.TryRemove(duplicateController, out descriptor);
            }

            return result;
        }
    }

}