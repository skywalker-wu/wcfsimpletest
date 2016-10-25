using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WCFCommon
{
    [DataContract]
    public class Data
    {
        [DataMember]
        public string D;

        [DataMember]
        public string IV;

        [DataMember]
        public string Key;
    }

    public class ClientMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
            

            Console.WriteLine("ClientMessageInspector.AfterReceiveReply");
        }
        public object BeforeSendRequest(ref Message message, IClientChannel channel)
        {
            ////var bufferCopy = request.CreateBufferedCopy(int.MaxValue);

            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms);
            message.WriteMessage(xw);
            xw.Flush();
            string body = Encoding.UTF8.GetString(ms.ToArray());
            xw.Close();

            ////Memory stream that contains the message
            //MemoryStream stream = new MemoryStream();
            ////Create an XmlWriter to serialize the message into a byte array
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = System.Text.Encoding.UTF8;
            //XmlWriter writer = XmlWriter.Create(stream, settings);
            ////Copy the message into a buffer 
            ////Note: This call changes the original message's state
            //MessageBuffer buffer = message.CreateBufferedCopy(int.MaxValue);
            ////Create a copy of the message
            //message = buffer.CreateMessage();
            ////Serialize the message to the XmlWriter 
            //message.WriteMessage(writer);
            ////Recreate the message 
            //message = buffer.CreateMessage();
            ////Flush the contents of the writer so that the stream gets updated
            //writer.Flush();
            //stream.Flush();
            //Convert the stream to an array
            byte[] data = Encoding.UTF8.GetBytes(body);

            using (Aes symmetricKey = Aes.Create())
            {
                symmetricKey.GenerateIV();

                var edata = EncryptionUtils.EncryptWithSymmetryKey(data, symmetricKey.Key, symmetricKey.IV);
                Data d = new Data()
                {
                    D = Convert.ToBase64String(edata),
                    IV = Convert.ToBase64String(symmetricKey.IV),
                    Key = Convert.ToBase64String(symmetricKey.Key)
                };

                var t = Message.CreateMessage(message.Version, null, d);
                t.Properties.CopyProperties(message.Properties);
                t.Headers.CopyHeadersFrom(message);
                message = t;
            }

            //message = ChangeString(message);
            //Encrypt
            Console.WriteLine("ClientMessageInspector.BeforeSendRequest");
            return null;
        }

        public Message ChangeString(Message oldMessage)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms);
            oldMessage.WriteMessage(xw);
            xw.Flush();
            string body = Encoding.UTF8.GetString(ms.ToArray());
            xw.Close();

            //body = body.Replace(from, to);
            

            ms = new MemoryStream(Encoding.UTF8.GetBytes(body));
            XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
            Message newMessage = Message.CreateMessage(xdr, int.MaxValue, oldMessage.Version);
            newMessage.Properties.CopyProperties(oldMessage.Properties);
            return newMessage;
        }
    }

    public class ServerMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("ServerMessageInspector.AfterReceiveRequest");
            
            var data = request.GetBody<Data>();
            var IV = Convert.FromBase64String(data.IV);
            var Key = Convert.FromBase64String(data.Key);
            var d = Convert.FromBase64String(data.D);
            var planData = new byte[d.Count()];

            int length = EncryptionUtils.DecryptInternal(d, Key, IV, planData);
            
            var body = Encoding.UTF8.GetString(planData, 0, length);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(body));
            XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
            Message newMessage = Message.CreateMessage(xdr, int.MaxValue, request.Version);
            newMessage.Properties.CopyProperties(request.Properties);
            request = newMessage;


            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(dd));
            //XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
            //Message newMessage = Message.CreateMessage(xdr, int.MaxValue, request.Version);
            //newMessage.Properties.CopyProperties(request.Properties);
            //request = newMessage;
            
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.ConformanceLevel = ConformanceLevel.Fragment;
            //using (var newStream = new MemoryStream(planData))
            //{
            //    newStream.Position = 0;
            //    XmlReader reader = XmlReader.Create(newStream, settings);
            //    reader.MoveToContent();

            //    var t = Message.CreateMessage(request.Version, null, reader);
            //    t.Headers.CopyHeadersFrom(request);
            //    t.Properties.CopyProperties(request.Properties);
            //    request = t;
            //}
            
            
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("ServerMessageInspector.BeforeSendReply");
        }
    }
}
