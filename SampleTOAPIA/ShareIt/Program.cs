using System;
using System.Net;

namespace ShowIt
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("239.1.1.1"), 5006);
            ConferenceSession session = new ConferenceSession(ep);

            session.Run();
        }
    }
}