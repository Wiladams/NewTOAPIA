using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.RUDP
{
/*
 *  one state structure per destination
 */
public class Reliable
{
	Ref;

	Reliable *next;

	byte	addr[IPaddrlen];	/* always V6 when put here */
	ushort	port;

	Block	*unacked;	/* unacked msg list */
	Block	*unackedtail;	/*  and its tail */

	int	timeout;	/* time since first unacked msg sent */
	int	xmits;		/* number of times first unacked msg sent */

	ulong	sndseq;		/* next packet to be sent */
	ulong	sndgen;		/*  and its generation */

	ulong	rcvseq;		/* last packet received */
	ulong	rcvgen;		/*  and its generation */

	ulong	acksent;	/* last ack sent */
	ulong	ackrcvd;	/* last msg for which ack was rcvd */

	/* flow control */
	QLock	lock;
	Rendez	vous;
	int	blocked;
};
}
