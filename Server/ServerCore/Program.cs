using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Program
    {
        static Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {
                // 클라이언트 메시지 수신
                byte[] recvBuff = new byte[1024];
                int recvBytes = clientSocket.Receive(recvBuff);
                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                Console.WriteLine($"[From Client] {recvData}");

                // 서버 응답 전송
                byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server !");
                clientSocket.Send(sendBuff);

                // 연결 종료
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"서버 오류: {e}");
            }
        }

        static void Main(string[] args)
        {
            // DNS (Domain Name System)에서 호스트 이름과 IP 주소를 가져오기
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 포트 번호 7777

             _listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("서버가 클라이언트를 기다리고 있습니다...");


            while (true)
            {
                ;
            }
        }
    }
}