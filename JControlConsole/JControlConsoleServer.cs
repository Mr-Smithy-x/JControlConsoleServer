using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace JControlConsole
{
    public class JControlConsoleServer
    {
        private string ip;
        private int port;
        private TcpListener tcpListener;
        private OnRequest getResponse;

        /// <summary>
        /// Initializes the connection
        /// </summary>
        /// <param name="getResponse">After Implementing OnRequest in your form, use "this" in this parameter</param>
        /// <param name="port">Set the port you want it to start listening for request</param>
        public JControlConsoleServer(OnRequest getResponse,int port)
        {
            this.getResponse = getResponse;
            this.ip = WhatsMyIP();
            this.port = port;
        }
        /// <summary>
        /// Initializes the connection
        /// </summary>
        /// <param name="getResponse">After Implementing OnRequest in your form, use "this" in this parameter</param>
        /// <param name="ip">Set a special ip you want to listen on</param>
        /// <param name="port">Set the port number you want it to listen to</param>
        public JControlConsoleServer(OnRequest getResponse, string ip, int port)
        {
            this.getResponse = getResponse;
            this.ip = ip;
            this.port = port;
        }
        /// <summary>
        /// Gets the port number
        /// </summary>
        /// <returns>the port number (int) </returns>
        public int getPort()
        {
            return port;
        }
        /// <summary>
        /// get the ip its listening on
        /// </summary>
        /// <returns>get the ip</returns>
        public string getIP()
        {
            return ip;
        }
        /// <summary>
        /// Gets the computers ip its listening on;
        /// </summary>
        /// <returns>the ip</returns>
        public string WhatsMyIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        /// <summary>
        /// Starts listening to the ip and the port
        /// </summary>
        public void Initialize()
        {
            Start();
        }
        private void StartAccept()
        {
            Console.WriteLine("Accepting using Async");
            tcpListener.BeginAcceptTcpClient(HandleAsyncConnection, tcpListener);

        }
        private void HandleAsyncConnection(IAsyncResult res)
        {
            StartAccept();
            TcpClient client = tcpListener.EndAcceptTcpClient(res);
            Console.WriteLine("Connection accepted from " + client.Client.RemoteEndPoint);
            byte[] b = new byte[1024];
            int k = client.Client.Receive(b);
            char cc = ' ';
            string test = null;
            Console.WriteLine("Recieved...");
            for (int i = 0; i < k; i++)
            {
            //  Console.Write(Convert.ToChar(b[i]));
                cc = Convert.ToChar(b[i]);
                test += cc.ToString();
            }
            if (String.IsNullOrEmpty(test)) Console.WriteLine("Data Recieved From The Android Was Empty: {0}", test);
            Console.WriteLine("Data Recieved From Client: {0}", test);
            try
            {
                if (test != null)
                {
                    ResponseTemplate resTemp = ParseRequest.getResponse(test, client.Client, getResponse);
                    if (resTemp != null)
                    {
                        string str = ParseRequest.MakeJson(resTemp);
                        Console.WriteLine(str);
                        client.Client.Send(ASCIIEncoding.ASCII.GetBytes(str));
                    }
                    else
                    {
                        client.Client.Send(ASCIIEncoding.ASCII.GetBytes("You made connection to the app but we don't speak the same language :("));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); client.Client.Close(); }
            client.Client.Close();
        }
        private void Start()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(ip);
                tcpListener = new TcpListener(ipAd, port);
                tcpListener.Start();
                Console.WriteLine("The server is running at port {0}...", port);
                Console.WriteLine("The local End point is: {0}", tcpListener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");
                StartAccept();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

    }
}
