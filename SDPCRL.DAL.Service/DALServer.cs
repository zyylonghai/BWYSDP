using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using SDPCRL.DAL.BUS;

namespace SDPCRL.DAL.Service
{
    public class DALServer : IService
    {
        TcpChannel channel;
        public void Star()
        {
            channel = new TcpChannel(8085);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(DALBus), "DALServer", WellKnownObjectMode.SingleCall);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
