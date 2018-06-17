using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace Connection

{
    public class SocketClient
    {
        byte[] recbuffer = new byte[1024];
        IPEndPoint remoteEP;
        Socket sender;

        public SocketClient(IPAddress adresip, int port)
        {
            remoteEP = new IPEndPoint(adresip, port);
            Connection();
        }
        public void Connection()
        {
            bool value = true;
            while (value)
            {
                try
                {
                    sender = new Socket((remoteEP.Address).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    sender.Connect(remoteEP);
                    Console.WriteLine("Socket połączył się z {0}.", sender.RemoteEndPoint.ToString());
                    value = false;
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.ToString());
                }
            }
        }
        public void Diconnection()
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        public void SendMessage(string msg)
        {
            try
            {
                byte[] msgEncoded = Encoding.ASCII.GetBytes(msg);
                int bytesSent = sender.Send(msgEncoded);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public string ReceiveMessage()
        {
            try
            {
                int bytesRec = sender.Receive(recbuffer);
                string msg = Encoding.ASCII.GetString(recbuffer, 0, bytesRec);
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
        }

    }
    public class SocketServer
    {
        byte[] bytes = new Byte[1024];
        static string data = null;
        IPEndPoint localEndPoint;
        Socket listener;
        Socket handler;
        public SocketServer(IPAddress adresip, int port)
        {
            localEndPoint = new IPEndPoint(adresip, port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connection();
        }


        public void Connection()
        {
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Waiting for a connection...");
                handler = listener.Accept();
                data = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Disconnection()
        {
            try
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                Console.WriteLine("Koniec połączenia");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void SendMessage(string msg)
        {
            try
            {
                byte[] msgEncoded = Encoding.ASCII.GetBytes(msg);
                handler.Send(msgEncoded);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string ReceiveMessage()
        {
            try
            {
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                string msg = data;
                data = null;
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
        }



    }
    public class Program
    {
        //public static int Main(String[] args)
        //{
        //    SocketServer socket = new SocketServer(IPAddress.Parse("127.0.0.1"), 11000);//tu podajesz adres ip i port
        //    string msg = socket.ReceiveMessage(); //pobieranie wiadomości
        //    Console.WriteLine(msg);
        //    socket.SendMessage(msg + "12");//wysyłanie wiadomości
        //    socket.Disconnection();//wyłączanie servera
        //    Console.Read();
        //    return 0;
        //}
    }
}

