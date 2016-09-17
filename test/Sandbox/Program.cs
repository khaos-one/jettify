using Jettify.Serve;
using System;

namespace Sandbox {
    public class Program {
        public static void Main(string[] args) {
            var server = new AsyncTcpServerFunc(8080, async (socket, token) => {
                Console.WriteLine("Connection requested.");
            });
            server.Start();
            Console.ReadLine();
        }
    }
}
