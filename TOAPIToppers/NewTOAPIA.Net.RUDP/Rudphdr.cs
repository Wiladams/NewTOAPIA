using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.RUDP
{
public class Rudphdr
{
	/* ip header */
	byte	vihl;		/* Version and header length */
	byte	tos;		/* Type of service */
	byte[]	length = new byte[2];	/* packet length */
	byte[]	id = new byte[2];		/* Identification */
	byte[]	frag = new byte[2];	/* Fragment information */

	/* pseudo header starts here */
	byte	Unused;
	byte	udpproto;	/* Protocol */
	byte[]	udpplen = new byte[2];	/* Header plus data length */
	byte[]	udpsrc = new byte[4];	/* Ip source */
	byte[]	udpdst = new byte[4];	/* Ip destination */

	/* udp header */
	byte[]	udpsport = new byte[2];	/* Source port */
	byte[]	udpdport= new byte[2];	/* Destination port */
	byte[]	udplen = new byte[2];	/* data length (includes rudp header) */
	byte[]	udpcksum = new byte[2];	/* Checksum */

	/* rudp header */
	byte[]	relseq = new byte[4];	/* id of this packet (or 0) */
	byte[]	relsgen = new byte[4];	/* generation/time stamp */
	byte[]	relack = new byte[4];	/* packet being acked (or 0) */
	byte[]	relagen = new byte[4];	/* generation/time stamp */
}
}
