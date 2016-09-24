/**
 * This interface is compliant with `IHttpRequestFeature` from ASP.Net Core
 * but was duplicated in order not to reference heavy ASP.Net Core libraries.
 * 
 * @see https://docs.asp.net/projects/api/en/latest/autoapi/Microsoft/AspNetCore/Http/Features/IHttpRequestFeature/index.html
 */

using System.IO;

namespace Jettify.Http {
    /// <summary>
    /// The basic HTTP request feature.
    /// </summary>
    /// <remarks>
    /// This interface is compliant with `IHttpRequestFeature` from ASP.Net Core.
    /// </remarks>
    public interface IHttpRequestFeature {
        Stream Body { get; }
        IHeaderDictionary Headers { get; }
        string Path { get; }
        string PathBase { get; }
        string Protocol { get; }
        string QueryString { get; }
        string RawTarget { get; }
        string Scheme { get; }
    }
}
