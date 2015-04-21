using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceChat
{
    public class MessageEventArgs : EventArgs
    {
        public string msg;
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service1 : IService1
    {
        public static event MessageEventHandler MessageEvent;
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        ISampleClientContract callback = null;

        MessageEventHandler MessageHandler = null;

        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<ISampleClientContract>();
            MessageHandler = new MessageEventHandler(PriceChangeHandler);
            MessageEvent += MessageHandler;
        }

        public void Unsubscribe()
        {
            MessageEvent -= MessageHandler;
        }

        public void PublishMsg(string msg)
        {
            MessageEventArgs e = new MessageEventArgs();
            e.msg = msg;
            MessageEvent(this, e);
        }

        public void PriceChangeHandler(object sender, MessageEventArgs e)
        {
            callback.sendMessage(e.msg);
        }
    }
}
