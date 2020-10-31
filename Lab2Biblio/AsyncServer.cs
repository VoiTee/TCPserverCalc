
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Lab2Biblio;

namespace Lab2Biblio
{
    /// <summary>
    /// This class implements the TCP Server as a child object of the abstract server.
    /// </summary>
    public class AsyncServer : AbstractServer
    {
        bool important = true;
        Regex regex = new Regex(@"\d+");
        Random rnd = new Random();
        public delegate void TransmissionDataDelegate(NetworkStream nStream);
        public AsyncServer(IPAddress IP, int port) : base(IP, port)
        {
        }
        protected override void AcceptClient()
        {
            while (true)
            {
                tcpClient = TcpListener.AcceptTcpClient();
                stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, tcpClient);
            }
        }

        private void login(NetworkStream netStream)
        {
            byte[] buffer = new byte[128];
            byte[] bufferSend = new byte[128];
            //NetworkStream netStream = client.GetStream();
            LoginHandler logHandler = new LoginHandler();

            string sendMessage = "\r\n Enter a login: \r\n";
            netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
            while (true)
            {
                int message_length = -1;

                try
                {
                    message_length = netStream.Read(buffer, 0, buffer.Length);
                    //if to ignore "enter" key
                    if (Encoding.ASCII.GetString(buffer, 0, message_length) != "\r\n" || message_length < 0)
                    {

                        sendMessage = logHandler.makeResponse(Encoding.ASCII.GetString(buffer, 0, message_length)) + "\r\n";
                        Console.WriteLine($"Ilosc odebranych znakow: ({Encoding.ASCII.GetString(buffer, 0, message_length)}): {message_length}");


                        netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                        Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                        System.Threading.Thread.Sleep(500);
                        if (logHandler.logInsuccess) break;
                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("PuTTy zostało zamknięte");
                    break;

                }

            }
            System.Threading.Thread.Sleep(2000);
        }
        protected override void BeginDataTransmission(NetworkStream netStream)
        {

            login(netStream);

            byte[] buffer = new byte[128];
            byte[] bufferSend = new byte[128];
            //NetworkStream netStream = client.GetStream();
            MessageHandler messHandler = new MessageHandler();

            string sendMessage = "Calculate EVERYTHING \r\n Enter a first number: \r\n";
            netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
            while (true)
            {
                int message_length = -1;

                try
                {
                    message_length = netStream.Read(buffer, 0, buffer.Length);
                    //if to ignore "enter" key
                    if (Encoding.ASCII.GetString(buffer, 0, message_length) != "\r\n" || message_length < 0)
                    {

                        sendMessage = messHandler.makeResponse(Encoding.ASCII.GetString(buffer, 0, message_length)) + "\r\n";
                        Console.WriteLine($"Ilosc odebranych znakow: ({Encoding.ASCII.GetString(buffer, 0, message_length)}): {message_length}");


                        netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                        Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                        System.Threading.Thread.Sleep(500);

                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("PuTTy zostało zamknięte");
                    break;

                }

            }
            System.Threading.Thread.Sleep(2000);
        }
        private void TransmissionCallback(IAsyncResult ar)
        {
        }
        /// <summary>
        /// Overrided comment.
        /// </summary>
        public override void Start()
        {
            StartListening();
            //transmission starts within the accept function
            AcceptClient();
        }
    }
}
