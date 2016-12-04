using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using Assets.Script;

public class SocketClient : MonoBehaviour{

    public string serverIP = "127.0.0.1";
    public Int32 port = 10200;

    private HandController handCtrller;

    private static SocketClient Instance;
    private Socket client;
    private string SPLIT = "<EOF>";
    // Size of receive buffer.
    private const int BufferSize = 4096;
    // Receive buffer.
    private byte[] buffer = new byte[BufferSize];
    // Received data string.

    public static SocketClient GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SocketClient();
        }
        return Instance;
    }

    private SocketClient()
    {

    }

    void Start()
    {
        //set hand controller
        handCtrller = GetComponent<HandController>();
        // start client
        StartClient(serverIP, port);
        Debug.Log(System.Environment.Version);
    }



    public void StartClient(string addr, int port)
    {
        try
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(addr), port);
            // Create a TCP/IP  socket.
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            client.Connect(remoteEP);
            if (client.Connected)
            {
                string logInfo = string.Format("connected to {0}", remoteEP);
                Debug.Log(logInfo);
            }

            new Thread(ReceiveFunc).Start();
        }
        catch (Exception)
        {
            Debug.Log("initialization fail");
        }
    }

    private void ReceiveFunc()
    {
        string data = null;
        while (client != null && client.Connected)
        {
            try
            {
                data = null;
                // An incoming connection needs to be processed.
                while (true)
                {
                    buffer = new byte[1024*8];
                    int bytesRec = client.Receive(buffer);
                    data += Encoding.ASCII.GetString(buffer, 0, bytesRec);
                    if (data.IndexOf(SPLIT) > -1)
                    {
                        break;
                    }
                }
                // Show the data on the console.
				data = data.Substring(0, data.IndexOf(SPLIT));
                //string logInfo = string.Format("Data received : {0}", data);
                //Debug.Log(logInfo);
				//FrameData frame = JsonConvert.DeserializeObject<FrameData>(data);
				var frame = JsonConvert.DeserializeObject<SkeletonJson>(data);
                if (!handCtrller.Mutex)
                {
                    handCtrller.update_data(frame);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }
}
