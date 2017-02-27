using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace Space_Alert
{
    class ConnectToServer
    {
        
        Windows.Networking.Sockets.DatagramSocket socket = new Windows.Networking.Sockets.DatagramSocket();
        Windows.Networking.HostName serverHost = new Windows.Networking.HostName("192.168.0.102");
        string serverPort;
        string clientPort = "13395";
        public string msg = "test";
        //public ConnectToServer()
        //{
        //    SendMessage("123");
        //}

        public ConnectToServer(string port)
        {
            serverPort = port;
        }
        
        public async void SendMessage()
        {
            

            socket.MessageReceived += Socket_MessageReceived;

            //You can use any port that is not currently in use already on the machine. We will be using two separate and random 
            //ports for the client and server because both the will be running on the same machine.

            //Because we will be running the client and server on the same machine, we will use localhost as the hostname.
            

            //Bind the socket to the clientPort so that we can start listening for UDP messages from the UDP echo server.
            await socket.BindServiceNameAsync(clientPort);

            //Write a message to the UDP echo server.
            
            
        }
        public async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
    Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {
            //Read the message that was received from the UDP echo server.
            Stream streamIn = args.GetDataStream().AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            msg = await reader.ReadToEndAsync();
            
            
        }
        public async void SendMsg(string msg)
        {
            Stream streamOut = (await socket.GetOutputStreamAsync(serverHost, serverPort)).AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            await writer.WriteLineAsync(msg);
            await writer.FlushAsync();
        }
     
    }
}
