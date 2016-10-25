using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFTest
{
    [ServiceContract(Namespace = "http://example.com/Command", CallbackContract = typeof(ICallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IHostService
    {
        [OperationContract(IsTerminating = true)]
        void UnSubscribe();
        [OperationContract(IsInitiating = true)]
        void Subscribe(int id);
        [OperationContract]
        string GetProperty();
    }

    [ServiceContract(Namespace = "http://example.com/Command")]
    public interface ICallback
    {
        [OperationContract]
        bool Notificate(int number);
    }


    public static class Constants
    {
        public const string Protocal = "net.tcp";
        public const string Url = "localhost";
        public const string Path = "WCFTest";
        public const string Address = "Calculate";
    }
}
