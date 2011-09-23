using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace SnapNShare
{

    public class UICommandSender : UIPortDelegate
    {
        PayloadChannel fCollaborationChannel;

        GraphPortChunkEncoder fChunkEncoder;

        public UICommandSender(PayloadChannel channel)
        {
            fCollaborationChannel = channel;

            // Create the sending graph port
            fChunkEncoder = new GraphPortChunkEncoder();
            fChunkEncoder.ChunkPackedEvent += new GraphPortChunkEncoder.ChunkPacked(GDIChunkPacked);
            AddPort(fChunkEncoder);
            
        }

        #region Properties
        public PayloadChannel Channel
        {
            get { return fCollaborationChannel; }
        }
        #endregion



        /// <summary>
        /// The GraphportChunkEncoder calls this method automatically when it has 
        /// a chunk that is ready to send out to the network.
        /// </summary>
        /// <param name="command"></param>
        void GDIChunkPacked(NewTOAPIA.BufferChunk command)
        {
            Channel.Send(command);
        }

    }
}
