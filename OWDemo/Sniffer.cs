using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;

namespace OWDemo
{
    class Sniffer
    {
        PacketDevice Device;
        PacketCommunicator Communicator;
        Task ReceiveTask;

        string ReplayServerAddr = "";
        string DemoName = "";

        /// <summary>
        /// Matches the replay server packet data.
        /// </summary>
        Regex ReplayServerMatcher = new Regex(@"(replay\d+\.valve\.net)");

        /// <summary>
        /// Matches the demo filename packet data.
        /// </summary>
        Regex DemoNameMatcher = new Regex(@"(\/730\/[\d|_]+\.dem\.bz2)");
        
        /// <summary>
        /// The URL to the demo file (constructed after both necessary packets have been received).
        /// </summary>
        public string URL { get; private set; }

        /// <summary>
        /// Whether or not we are currently monitoring network traffic.
        /// </summary>
        public bool Running { get; private set; }
        
        public Sniffer(PacketDevice device)
        {
            this.Device = device;
        }

        private void HandlePacket(Packet packet)
        {
            var data = Encoding.Default.GetString(packet.Buffer);

            //If we don't already have the server address, check the packet for it.
            if(ReplayServerAddr.Equals(""))
            {
                var match = ReplayServerMatcher.Match(data);
                if(match.Success)
                {
                    ReplayServerAddr = match.Groups[1].Value;
                    Console.WriteLine("Replay server: {0}", ReplayServerAddr);
                }
            }

            //If we don't already have the demo filename, check the packet for it.
            if (DemoName.Equals(""))
            {
                var match = DemoNameMatcher.Match(data);
                if(match.Success)
                {
                    DemoName = match.Groups[1].Value;
                    Console.WriteLine("Demo filename: {0}", DemoName);
                }
            }
        }

        /// <summary>
        /// Starts the receiver thread that listens for incoming packets.
        /// </summary>
        public void Start()
        {
            Communicator = Device.Open(65536, PacketDeviceOpenAttributes.None, 1000);
            Running = true;
            ReceiveTask = Task.Run(() => ReceivePackets());
            Console.WriteLine("Now listening...");
        }

        /// <summary>
        /// Stops listening for incoming packets and waits for the receiver thread to end.
        /// </summary>
        public void Stop()
        {
            if (Running)
            {
                Running = false;
                Task.WaitAll(ReceiveTask);
                Communicator.Dispose();
                Communicator = null;
            }
        }

        /// <summary>
        /// Loop to receive packets (blocking).
        /// </summary>
        private void ReceivePackets()
        {
            Packet packet;
            while(Running)
            {
                var result = Communicator.ReceivePacket(out packet);
                switch(result)
                {
                    case PacketCommunicatorReceiveResult.Timeout:
                        continue;
                    case PacketCommunicatorReceiveResult.Ok: //Only handle a packet if it was successfully received
                        HandlePacket(packet);
                        break;
                    default:
                        break;
                }

                //If we have received both necesarry packets, construct URL and stop listening
                if (!(ReplayServerAddr.Equals("") || DemoName.Equals("")))
                {
                    URL = string.Format("http://{0}{1}", ReplayServerAddr, DemoName);
                    Stop();
                }
            }
        }
    }
}
