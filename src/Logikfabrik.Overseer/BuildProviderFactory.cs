using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logikfabrik.Overseer.Settings;

namespace Logikfabrik.Overseer
{
    public static class BuildProviderFactory
    {
        public static BuildProvider GetProvider(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var providerType = settings.GetProviderType();

            var constructor = providerType.GetConstructor(Type.EmptyTypes);

            return constructor.Invoke(new object[] {}) as BuildProvider;
        }
    }
}
