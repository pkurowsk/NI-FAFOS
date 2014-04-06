using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.IO;

namespace FAFOS
{
    class TCP_Model
    {
        Socket TCPsocket; //socket used to send/receive TCP messages
        IPAddress ServerIPAddr = null;
        IPAddress ClientIPAddr = null;
        Socket listening_TCPsocket = null;
        int TCP_PORT;
        private const int BufferSize = 15000;


        public TCP_Model(string IP, int port)
        {
            //Establish a TCP connection with the server to exchange TCP messages
            //------------------
            TCPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Creating Endpoint 
            ServerIPAddr = IPAddress.Parse(IP);
            TCP_PORT = port;
            IPEndPoint TCP_serverEndPoint = new IPEndPoint(ServerIPAddr, TCP_PORT);

            // try to connect
            try
            {
                TCPsocket.Connect(TCP_serverEndPoint);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception caught: " + ex);
            }

            ClientIPAddr = ((IPEndPoint)TCPsocket.LocalEndPoint).Address;

        }


        public TCP_Model(int GivenPort)
        {
            // Creating TCP listening socket
            listening_TCPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Creating Endpoint to the localhost address with the given port number
            ServerIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            //IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
            //ServerIP = ipAddress[1];

            IPEndPoint listenEndPoint = new IPEndPoint(ServerIPAddr, GivenPort);

            // Bind server socket to Endpoint object
            listening_TCPsocket.Bind(listenEndPoint);
            listening_TCPsocket.Listen(int.MaxValue);
        }

        public TCP_Model(Socket s)
        {
            this.TCPsocket = s;
            ClientIPAddr = ((IPEndPoint)s.RemoteEndPoint).Address;
            TCP_PORT = ((IPEndPoint)s.RemoteEndPoint).Port;
        }

        public Socket AcceptOneClient()
        {
            //blocks until a client has connected to the server
            return this.listening_TCPsocket.Accept();
        }

        public void Send_to_TCP(String data)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                TCPsocket.Send(byteData, 0, byteData.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                throw new Exception("TCPsocket.Send : " + e.Message);
            }
        }

        public String Get_From_TCP()
        {
            byte[] data = new byte[BufferSize];
            int byteRecv = 0;
            string receivedData = null;
            try
            {
                byteRecv = TCPsocket.Receive(data, 0, BufferSize, SocketFlags.None);
                if (byteRecv > 0)
                {
                    return receivedData = Encoding.ASCII.GetString(data, 0, byteRecv);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
            //try
            //{
            //    byte[] buffer = new byte[2048];
            //    byte[] output;

            //    int byteRecv = TCPsocket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

            //    byte[] receivedData = new byte[byteRecv];
            //    Array.Copy(buffer, receivedData, byteRecv);
            //    output = receivedData;

            //    return Encoding.ASCII.GetString(output);
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}

        }

        public IPAddress ServerIP
        {
            get { return ServerIPAddr; }
            set { ServerIPAddr = value; }
        }

        public IPAddress ClientIP { get; set; }
        public int ClientPort { get; set; }

        public void shutdown()
        {
            TCPsocket.Close();
        }
    }
}
