using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NetInfo";
            IPGlobalProperties wlasnosciIP = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("Host name: {0}", wlasnosciIP.HostName);
            Console.WriteLine("Domain name: {0}\r\n", wlasnosciIP.DomainName);
            int licznik = 0;
            foreach (NetworkInterface kartySieciowe in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Network card");
                Console.WriteLine("ID: #{0}: {1}", ++licznik, kartySieciowe.Id);
                Console.WriteLine("\tMAC address: {0}", kartySieciowe.GetPhysicalAddress());
                Console.WriteLine("\tName: {0}", kartySieciowe.Name);
                Console.WriteLine("\tDescription: {0}", kartySieciowe.Description);
                Console.WriteLine("\tStatus: {0}", kartySieciowe.OperationalStatus);
                Console.WriteLine("\tSpeed: {0} Mb/s", (kartySieciowe.Speed) / (double)1000000);
                Console.WriteLine("\tGateway addresses:");
                foreach(GatewayIPAddressInformation adresBramy in kartySieciowe.GetIPProperties().GatewayAddresses)
                {
                    Console.WriteLine("\t\t{0}");
                    Console.WriteLine("\t\tDNS servers:");
                    foreach (IPAddress adresip in kartySieciowe.GetIPProperties().DnsAddresses)
                        Console.WriteLine("\t\t\t{0}", adresip);
                    Console.WriteLine("\t\tDHCP servers:");
                    foreach (IPAddress adresip in kartySieciowe.GetIPProperties().DhcpServerAddresses)
                        Console.WriteLine("\t\t\t{0}", adresip);
                    Console.WriteLine("\t\tWINS servers:");
                    foreach (IPAddress adresip in kartySieciowe.GetIPProperties().WinsServersAddresses)
                        Console.WriteLine("\t\t\t{0}", adresip);
                    Console.WriteLine();
                }
                Console.WriteLine("\r\nCurrent client type TCP / IP connections:");
                foreach(TcpConnectionInformation polaczenieTCP in IPGlobalProperties.GetIPGlobalProperties().
                    GetActiveTcpConnections())
                {
                    Console.WriteLine("\tRemote address: {0}:{1}", polaczenieTCP.RemoteEndPoint.Address,
                        polaczenieTCP.RemoteEndPoint.Port);
                    Console.WriteLine("\tStatus: {0}", polaczenieTCP.State);
                }
                Console.WriteLine("\r\nCurrent TCP / IP server connections:");
                foreach (IPEndPoint polaczenieTCP in IPGlobalProperties.GetIPGlobalProperties().
                    GetActiveTcpListeners())
                    Console.WriteLine("\tRemote address: {0}", polaczenieTCP.Port);
                Console.WriteLine("\r\nCurrent UDP connections:");
                foreach (IPEndPoint polaczenieUDP in IPGlobalProperties.GetIPGlobalProperties().
                    GetActiveUdpListeners())
                    Console.WriteLine("\tRemote address: {0}", polaczenieUDP.Port);
                Console.ReadKey();
            }
        }
    }
}
