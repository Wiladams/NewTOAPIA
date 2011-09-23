using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace HamSketch
{
    class GraphicsChannel : GraphPortDelegate
    {
        PayloadChannel fCollaborationChannel;

        IGraphPort fRenderer;

        GraphPortChunkEncoder fChunkEncoder;
        GraphPortChunkDecoder fChunkDecoder;

        public GraphicsChannel(IGraphPort localRenderer, PayloadChannel channel)
        {
            fCollaborationChannel = channel;

            fRenderer = localRenderer;

            fChunkDecoder = new GraphPortChunkDecoder();
            fChunkDecoder.AddGraphPort(fRenderer);

            // Create the sending graph port
            fChunkEncoder = new GraphPortChunkEncoder();
            fChunkEncoder.ChunkPackedEvent += new GraphPortChunkEncoder.ChunkPacked(GDIChunkPacked);
            AddGraphPort(fChunkEncoder);
            
            // send the frame to the receiver
            fCollaborationChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(GDICommandReceived);
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

        /// <summary>
        /// This method is registered with the Channel to received any frames of data
        /// that come from the network.  The ea.Frame property contains a BufferChunk
        /// that has the data in it from the sender.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        private void GDICommandReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            fChunkDecoder.ReceiveData(ea.Frame);
        }
    }
}
