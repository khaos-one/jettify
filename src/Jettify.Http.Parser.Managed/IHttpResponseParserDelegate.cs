namespace Jettify.Http.Parser.Managed {
    public interface IHttpResponseParserDelegate : IHttpParserDelegate {
        void OnResponseCode(HttpParser parser, int statusCode, string statusReason);
    }
}