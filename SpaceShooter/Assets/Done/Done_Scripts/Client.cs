using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Client
    {
        private String controldata;
        private Socket clientSocket;
        private static Client client;
        private Thread t;
        private Client()
        {
            StartRec();
        }
        public static Client GetInstance()
        {
            if (client == null)
            {
                client = new Client();
            }
            return client;
        }


        public void StartRec()
        {
            t = new Thread(ReciveMsg);
            t.IsBackground = true;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            clientSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000));
            t.Start();
        }

        public String GetControldata()
        {
            return controldata;
        }

        public void ReStart()
        {
            if (!t.IsAlive)
            {
                t.Start();
            }
        }

        public void Destroy()
        {
            if (t.IsAlive)
            {
                t.Abort();
            }
               clientSocket.Close();
        }
        public void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6001);
                byte[] buffer = new byte[1024];
                int length = clientSocket.ReceiveFrom(buffer, ref point);
                controldata = Encoding.UTF8.GetString(buffer, 0, length);
            }
        }

    }