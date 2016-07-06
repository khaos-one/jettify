using Jettify.Serve;

namespace TestSandbox {
    public class Program {
        public static void Main(string[] args) {
            using (var server = new TcpThreadedServerFunc(7777, (s) => {

            })) {
                server.Start();
                server.WaitForJoin();
            }
        }
    }
}
