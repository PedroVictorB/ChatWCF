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

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service1 : IService1
    {
        public static event MessageEventHandler MessageEvent;
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        ISampleClientContract callback = null;

        MessageEventHandler MessageHandler = null;

        //Clients call this service operation to subscribe.
        //A price change event handler is registered for this client instance.

        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<ISampleClientContract>();
            MessageHandler = new MessageEventHandler(PriceChangeHandler);
            MessageEvent += MessageHandler;
        }

        //Clients call this service operation to unsubscribe.
        //The previous price change event handler is deregistered.

        public void Unsubscribe()
        {
            MessageEvent -= MessageHandler;
        }

        //Information source clients call this service operation to report a price change.
        //A price change event is raised. The price change event handlers for each subscriber will execute.

        public void PublishMsg(string msg)
        {
            MessageEventArgs e = new MessageEventArgs();
            e.msg = msg;
            MessageEvent(this, e);
        }

        //This event handler runs when a PriceChange event is raised.
        //The client's PriceChange service operation is invoked to provide notification about the price change.

        public void PriceChangeHandler(object sender, MessageEventArgs e)
        {
            callback.sendMessage(e.msg);
        }
    }
}
