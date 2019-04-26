using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace RealTimeServerTime.Hubs
{
    public class RealTimeHub: Hub
    {
        public ChannelReader<string> StartTimer()
        {
            var channel = Channel.CreateUnbounded<String>();
            _ = WriteDates(channel.Writer);
            return channel.Reader;
        }

        private async Task WriteDates(ChannelWriter<string> writer)
        {
            try
            {
                while (true)
                {
                    await writer.WriteAsync(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss:fffffff"));
                    await Task.Delay(10);
                }
            }
            catch (Exception ex)
            {
                writer.TryComplete();
            }
            writer.TryComplete();
        }
    }
}
