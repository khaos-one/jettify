using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Jettify.Http {
    public interface IHeaderDictionary : IDictionary<string, StringValues> {}
}
