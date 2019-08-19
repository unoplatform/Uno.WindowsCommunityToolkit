using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    /// <summary>
    /// TODO: Move to Toolkit Core
    /// 
    /// Provides an easy-to-use thread-safe Singleton Pattern via ConcurrentDictionary.
    /// 
    /// Use by adding a static property to your class for traditional access pattern:
    ///   public static T Instance => Singleton<T>.Instance;
    /// </summary>
    public static class Singleton<T>
        where T : new()
    {
        private static ConcurrentDictionary<Type, T> _instances = new ConcurrentDictionary<Type, T>();

        public static T Instance
        {
            get
            {
                return _instances.GetOrAdd(typeof(T), (t) => new T());
            }
        }
    }
}
