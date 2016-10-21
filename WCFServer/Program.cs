using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFTest;

namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HostService), new Uri(string.Format("{0}://{1}/{2}", Constants.Protocal, Constants.Url, Constants.Path))))
            {
                var binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IHostService), binding, Constants.Address);
                host.Open();
                foreach (var serviceEndpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("Endpoint:{0}", serviceEndpoint.ListenUri.AbsoluteUri);
                    Console.WriteLine("ContractType:{0}", serviceEndpoint.Contract.ContractType.FullName);
                    Console.WriteLine("-----------------------");
                }

                Console.WriteLine("Service Started");
                ConsoleKey k;
                while ((k = Console.ReadKey(true).Key) != ConsoleKey.Q)
                {
                    foreach (var item in HostService.CallbackDictionary)
                    {
                        var result = item.Value.Notificate(123);
                        Console.WriteLine("{0}:{1}", item.Key, result);
                    }

                    Thread.Sleep(100);
                }
            }

            Console.WriteLine("Goodbye.");
        }
    }



    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class HostService : IHostService
    {
        public static Dictionary<string, ICallback> CallbackDictionary = new Dictionary<string, ICallback>();

        private ICallback _callBack;
        private string _id;

        public string GetProperty()
        {
            return OperationContext.Current.SessionId;
        }

        public void UnSubscribe()
        {
            if (!CallbackDictionary.ContainsKey(_id))
            {
                return;
            }

            CallbackDictionary.Remove(_id);

            Console.WriteLine("UnSubscribe {0}", _id);
            // Dispose callback
        }

        public void Subscribe()
        {
            _id = OperationContext.Current.SessionId;
            _callBack = OperationContext.Current.GetCallbackChannel<ICallback>();

            CallbackDictionary[_id] = _callBack;
            ((IChannel)_callBack).Closed += (sender, args) =>
            {
                UnSubscribe();
            };

            ((IChannel)_callBack).Faulted += (sender, args) =>
            {
                UnSubscribe();
            };

            Console.WriteLine("subscribe {0}", _id);
        }
    }
}
