using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Alert
{

    class GetMessage
    {

        public static string message = " ";
        public async void get()
        {
            Windows.Networking.Sockets.DatagramSocket socket = new Windows.Networking.Sockets.DatagramSocket();

            socket.MessageReceived += Socket_MessageReceived;

            //You can use any port that is not currently in use already on the machine.
            string serverPort = "21777";

            //Bind the socket to the serverPort so that we can start listening for UDP messages from the UDP echo client.
            await socket.BindServiceNameAsync(serverPort);
        }
        private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {
            //Read the message that was received from the UDP echo client.
            Stream streamIn = args.GetDataStream().AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
             message = await reader.ReadLineAsync();

            //Create a new socket to send the same message back to the UDP echo client.
           
        }

    }
}
