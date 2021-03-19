using System;
using System.Linq;
using SATokenRegisterApi.Common.Types;

namespace SATokenRegisterApi.Data.Dto
{
    public class IdGenerator
    {
        public static URN GetOne(string name, Guid id)
        {
            return new URN("fan", name, id.ToString().GetHashCode().ToString("x"));
        }
    }
}
