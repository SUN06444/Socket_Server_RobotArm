using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

//IP:   192.168.1.37
//port: 6340


namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string _ip = string.Empty;
            string _accessCmd=string.Empty;
            string _receiveData = string.Empty;
            byte[] _buffer = new byte[1024];

            //Create one Socket object to setup tcp connection
            Socket _remoteAccess = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            Console.WriteLine("Example: 127.0.0.1");
            Console.Write("IP Address(press enter if done): ");
            _ip = Console.ReadLine();
            
            //Get IP address associated with user
            IPAddress _ipAddr = IPAddress.Parse(_ip);
            //Establishes a connection to a remote host
            _remoteAccess.Connect(_ipAddr, 6340);
            _remoteAccess.ReceiveTimeout = 3000;

            //Connection Confirm
            Console.WriteLine("Network connect? {0}", _remoteAccess.Connected);

            while (true)
            {
                Console.WriteLine("\nEnter exit to leave.");
                Console.Write("Enter access command.(press enter if done): ");

                _accessCmd = Console.ReadLine();

                if (_accessCmd == "Exit") break;

                //\r\n Check command
                _accessCmd += "\r\n";

                //Sends Data to a connected socket
                _remoteAccess.Send(Encoding.ASCII.GetBytes(_accessCmd));

                //Converts byte array to string
                _receiveData = Encoding.ASCII.GetString(_buffer,0 , _remoteAccess.Receive(_buffer));
            
                Console.Write("Receive Data: {0}", _receiveData);


            }

            

            //Receive Data from Server
            //_remoteAccess.Receive(_buffer);

            //Network disconnection
            _remoteAccess.Disconnect(false);

            //Close Socket
            _remoteAccess.Close();
        }
    }
}
