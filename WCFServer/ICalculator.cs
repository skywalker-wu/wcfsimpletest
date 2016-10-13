using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFTest
{
    [ServiceContract(Namespace = "http://example.com/Command")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double n1, double n2);
        [OperationContract]
        double Subtract(double n1, double n2);
        [OperationContract]
        double Multiply(double n1, double n2);
        [OperationContract]
        double Divide(double n1, double n2);
    }

    public static class Constants
    {
        public const string Protocal = "net.tcp";
        public const string Url = "localhost:566";
        public const string Path = "WCFTest";
        public const string Address = "Calculate";
    }
}
