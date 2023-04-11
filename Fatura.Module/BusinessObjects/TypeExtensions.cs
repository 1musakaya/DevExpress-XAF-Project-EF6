using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatura.Module.BusinessObjects
{
    public static class TypeExtensions
    {
        private const string ProxyNamespace = @"System.Data.Entity.DynamicProxies";

        public static Type GetEntityType(this Type entityType)
        {
            if (entityType.Namespace == ProxyNamespace)
            {
                return GetEntityType(entityType.BaseType);
            }

            return entityType;
        }
    }
}
