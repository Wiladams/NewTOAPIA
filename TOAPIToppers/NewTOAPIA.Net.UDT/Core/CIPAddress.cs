namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Net;
    using System.Net.Sockets;

public class CIPAddress
{
public static bool ipcmp(IPAddress addr1, IPAddress addr2, AddressFamily ver)
{
    return addr1 == addr2;
   //if (AddressFamily.InterNetwork == ver)
   //{
   //   sockaddr_in* a1 = (sockaddr_in*)addr1;
   //   sockaddr_in* a2 = (sockaddr_in*)addr2;

   //   if ((a1.sin_port == a2.sin_port) && (a1.sin_addr.s_addr == a2.sin_addr.s_addr))
   //      return true;
   //}
   //else
   //{
   //   sockaddr_in6* a1 = (sockaddr_in6*)addr1;
   //   sockaddr_in6* a2 = (sockaddr_in6*)addr2;

   //   if (a1.sin6_port == a2.sin6_port)
   //   {
   //      for (int i = 0; i < 16; ++ i)
   //         if (*((char*)&(a1.sin6_addr) + i) != *((char*)&(a2.sin6_addr) + i))
   //            return false;

   //      return true;
   //   }
   //}

   //return false;
}

public static void ntop(IPAddress addr, UInt32[] ip, AddressFamily ver)
{
   if (AddressFamily.InterNetwork == ver)
   {
      sockaddr_in* a = (sockaddr_in*)addr;
      ip[0] = a.sin_addr.s_addr;
   }
   else
   {
      sockaddr_in6* a = (sockaddr_in6*)addr;
      ip[3] = (a.sin6_addr.s6_addr[15] << 24) + (a.sin6_addr.s6_addr[14] << 16) + (a.sin6_addr.s6_addr[13] << 8) + a.sin6_addr.s6_addr[12];
      ip[2] = (a.sin6_addr.s6_addr[11] << 24) + (a.sin6_addr.s6_addr[10] << 16) + (a.sin6_addr.s6_addr[9] << 8) + a.sin6_addr.s6_addr[8];
      ip[1] = (a.sin6_addr.s6_addr[7] << 24) + (a.sin6_addr.s6_addr[6] << 16) + (a.sin6_addr.s6_addr[5] << 8) + a.sin6_addr.s6_addr[4];
      ip[0] = (a.sin6_addr.s6_addr[3] << 24) + (a.sin6_addr.s6_addr[2] << 16) + (a.sin6_addr.s6_addr[1] << 8) + a.sin6_addr.s6_addr[0];
   }
}

public static void pton(IPAddress addr, UInt32[] ip, AddressFamily ver)
{
   if (AddressFamily.InterNetwork == ver)
   {
      sockaddr_in* a = (sockaddr_in*)addr;
      a.sin_addr.s_addr = ip[0];
   }
   else
   {
      sockaddr_in6* a = (sockaddr_in6*)addr;
      for (int i = 0; i < 4; ++ i)
      {
         a.sin6_addr.s6_addr[i * 4] = ip[i] & 0xFF;
         a.sin6_addr.s6_addr[i * 4 + 1] = (byte)((ip[i] & 0xFF00) >> 8);
         a.sin6_addr.s6_addr[i * 4 + 2] = (byte)((ip[i] & 0xFF0000) >> 16);
         a.sin6_addr.s6_addr[i * 4 + 3] = (byte)((ip[i] & 0xFF000000) >> 24);
      }
   }
}
}
}