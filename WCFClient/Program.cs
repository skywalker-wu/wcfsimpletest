using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using WCFTest;
using System.ServiceModel.Channels;
using Common;

namespace WCFClient
{
    class Program
    {
        static DuplexChannelFactory<IHostService> _factory;
        static IHostService _channel;
        static Guid _id = Guid.NewGuid();

        static void Main(string[] args)
        {
            Init();
            ConsoleKey k;
            while ((k = Console.ReadKey(true).Key) != ConsoleKey.Q)
            {
                switch (k){
                    case ConsoleKey.R:
                        Register();
                        break;

                    case ConsoleKey.U:
                        UnRegister();
                        Console.WriteLine("Bye!");
                        return;

                    default:
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void Init()
        {
            var binding = new TcpCustomBinding();
            Uri ServiceUri = new Uri(string.Format("{0}://{1}/{2}", Constants.Protocal, Constants.Url, Constants.Path));
            EndpointAddress ServiceAddress = new EndpointAddress(string.Format("{0}/{1}", ServiceUri.OriginalString, Constants.Address));
            var callback = new Callback();
            _factory = new DuplexChannelFactory<IHostService>(new InstanceContext(callback), binding, ServiceAddress);
            _factory.Credentials.UserName.UserName = "yaron";
            _factory.Credentials.UserName.Password = "1234";
            _channel = _factory.CreateChannel();
            callback.Channel = _channel;
        }

        static void Register()
        {
            _channel.Subscribe();
            Console.WriteLine("subscribe succeed");
        }

        static void UnRegister()
        {
            _channel.UnSubscribe();
            Console.WriteLine("unsubscribe succeed");
        }
    }

    public class Callback : ICallback
    {
        public IHostService Channel { set; get; }
        public bool Notificate(int number)
        {
            Console.WriteLine(number);

            Task.Run(() => Console.WriteLine(Channel.GetProperty()));

            return true;
        }
    }
}
