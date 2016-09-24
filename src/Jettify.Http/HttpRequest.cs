using System.IO;

namespace Jettify.Http {
    public class HttpRequest : IHttpRequestFeature {
        public HttpRequest() {}
        public Stream Body { get; }
        public IHeaderDictionary Headers { get; }
        public string Path { get; }
        public string PathBase { get; }
        public string Protocol { get; }
        public string QueryString { get; }
        public string RawTarget { get; }
        public string Scheme { get; }
    }
}
