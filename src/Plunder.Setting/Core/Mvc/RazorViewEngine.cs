//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http.Controllers;
//using Plunder.Setting.Core.Extensions;
//using System.Globalization;

//namespace Plunder.Setting.Core.Mvc
//{
//    public class RazorViewEngine : IViewEngine
//    {
//        internal static readonly string ViewStartFileName = "_ViewStart";

//        private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:"; //{4}:
//        private const string CacheKeyPrefixMaster = "Master";
//        private const string CacheKeyPrefixPartial = "Partial";
//        private const string CacheKeyPrefixView = "View";
//        private static readonly string[] _emptyLocations = new string[0];

//        public string[] ViewLocationFormats { get; set; }

//        public string[] MasterLocationFormats { get; set; }

//        public string[] PartialViewLocationFormats { get; set; }

//        public string[] FileExtensions { get; set; }


//        public ViewEngineResult FindPartialView(HttpControllerContext controllerContext, string partialViewName, bool useCache)
//        {
//            throw new NotImplementedException();
//        }

//        public ViewEngineResult FindView(HttpControllerContext controllerContext, string viewName, string masterName, bool useCache)
//        {
//            if (controllerContext == null)
//            {
//                throw new ArgumentNullException("controllerContext");
//            }
//            if (String.IsNullOrEmpty(viewName))
//            {
//                throw new ArgumentNullException("viewName");
//            }

//            string[] viewLocationsSearched;
//            string[] masterLocationsSearched;

//            string controllerName = controllerContext.RouteData.Values.GetValue<string>("controller");
//            string viewPath = GetPath(controllerContext, ViewLocationFormats, /*AreaViewLocationFormats,*/ "ViewLocationFormats", viewName, controllerName, CacheKeyPrefixView, out viewLocationsSearched);
//            string masterPath = GetPath(controllerContext, MasterLocationFormats,/* AreaMasterLocationFormats,*/ "MasterLocationFormats", masterName, controllerName, CacheKeyPrefixMaster, out masterLocationsSearched);

//            if (String.IsNullOrEmpty(viewPath) || (String.IsNullOrEmpty(masterPath) && !String.IsNullOrEmpty(masterName)))
//            {
//                return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
//            }

//            return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
//        }

//        private string GetPath(HttpControllerContext controllerContext, string[] locations, /*string[] areaLocations,*/ string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, out string[] searchedLocations)
//        {
//            searchedLocations = _emptyLocations;

//            if (String.IsNullOrEmpty(name))
//            {
//                return String.Empty;
//            }

//            //string areaName = AreaHelpers.GetAreaName(controllerContext.RouteData);
//            //bool usingAreas = !String.IsNullOrEmpty(areaName);
//            List<ViewLocation> viewLocations = GetViewLocations(locations/*, (usingAreas) ? areaLocations : null*/);

//            if (viewLocations.Count == 0)
//                throw new InvalidOperationException(nameof(locations));

//            bool nameRepresentsPath = IsSpecificPath(name);
//            string cacheKey = CreateCacheKey(cacheKeyPrefix, name, (nameRepresentsPath) ? String.Empty : controllerName/*, areaName*/);

//            return nameRepresentsPath
//                   ? GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations)
//                   : GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName/*, areaName*/, cacheKey, ref searchedLocations);
//        }

//        private string GetPathFromGeneralName(HttpControllerContext controllerContext, List<ViewLocation> locations, string name, string controllerName, /*string areaName,*/ string cacheKey, ref string[] searchedLocations)
//        {
//            string result = String.Empty;
//            searchedLocations = new string[locations.Count];

//            for (int i = 0; i < locations.Count; i++)
//            {
//                ViewLocation location = locations[i];
//                string virtualPath = location.Format(name, controllerName);
//                DisplayInfo virtualPathDisplayInfo = DisplayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, controllerContext.HttpContext, path => FileExists(controllerContext, path), controllerContext.DisplayMode);

//                if (virtualPathDisplayInfo != null)
//                {
//                    string resolvedVirtualPath = virtualPathDisplayInfo.FilePath;

//                    searchedLocations = _emptyLocations;
//                    result = resolvedVirtualPath;
//                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, virtualPathDisplayInfo.DisplayMode.DisplayModeId), result);

//                    if (controllerContext.DisplayMode == null)
//                    {
//                        controllerContext.DisplayMode = virtualPathDisplayInfo.DisplayMode;
//                    }

//                    // Populate the cache for all other display modes. We want to cache both file system hits and misses so that we can distinguish
//                    // in future requests whether a file's status was evicted from the cache (null value) or if the file doesn't exist (empty string).
//                    IEnumerable<IDisplayMode> allDisplayModes = DisplayModeProvider.Modes;
//                    foreach (IDisplayMode displayMode in allDisplayModes)
//                    {
//                        if (displayMode.DisplayModeId != virtualPathDisplayInfo.DisplayMode.DisplayModeId)
//                        {
//                            DisplayInfo displayInfoToCache = displayMode.GetDisplayInfo(controllerContext.HttpContext, virtualPath, virtualPathExists: path => FileExists(controllerContext, path));

//                            string cacheValue = String.Empty;
//                            if (displayInfoToCache != null && displayInfoToCache.FilePath != null)
//                            {
//                                cacheValue = displayInfoToCache.FilePath;
//                            }
//                            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), cacheValue);
//                        }
//                    }
//                    break;
//                }

//                searchedLocations[i] = virtualPath;
//            }

//            return result;
//        }

//        private string GetPathFromSpecificName(HttpControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
//        {
//            string result = name;

//            if (!(FilePathIsSupported(name) && FileExists(controllerContext, name)))
//            {
//                result = String.Empty;
//                searchedLocations = new[] { name };
//            }

//            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
//            return result;
//        }

//        private bool FilePathIsSupported(string virtualPath)
//        {
//            if (FileExtensions == null)
//            {
//                // legacy behavior for custom ViewEngine that might not set the FileExtensions property
//                return true;
//            }
//            else
//            {
//                // get rid of the '.' because the FileExtensions property expects extensions withouth a dot.
//                string extension = GetExtensionThunk(virtualPath).TrimStart('.');
//                return FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
//            }
//        }

//        private static List<ViewLocation> GetViewLocations(string[] viewLocationFormats/*, string[] areaViewLocationFormats*/)
//        {
//            List<ViewLocation> allLocations = new List<ViewLocation>();

//            //if (areaViewLocationFormats != null)
//            //{
//            //    foreach (string areaViewLocationFormat in areaViewLocationFormats)
//            //    {
//            //        allLocations.Add(new AreaAwareViewLocation(areaViewLocationFormat));
//            //    }
//            //}

//            if (viewLocationFormats != null)
//            {
//                foreach (string viewLocationFormat in viewLocationFormats)
//                {
//                    allLocations.Add(new ViewLocation(viewLocationFormat));
//                }
//            }

//            return allLocations;
//        }


//        internal virtual string CreateCacheKey(string prefix, string name, string controllerName/*, string areaName*/)
//        {
//            return String.Format(CultureInfo.InvariantCulture, CacheKeyFormat,
//                                 GetType().AssemblyQualifiedName, prefix, name, controllerName/*, areaName*/);
//        }

//        private static bool IsSpecificPath(string name)
//        {
//            char c = name[0];
//            return (c == '~' || c == '/');
//        }

//        public void ReleaseView(HttpControllerContext controllerContext, IView view)
//        {
//            throw new NotImplementedException();
//        }


//        public RazorViewEngine()
//        { 
//            ViewLocationFormats = new[]
//            {
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/Shared/{0}.cshtml"
//            };
//            MasterLocationFormats = new[]
//            {
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/Shared/{0}.cshtml"
//            };
//            PartialViewLocationFormats = new[]
//            {
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/Shared/{0}.cshtml"
//            };

//            FileExtensions = new[]
//            {
//                "cshtml"
//            };
//        }
//    }
//}
