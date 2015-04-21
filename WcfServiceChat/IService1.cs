using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceChat
{
    [ServiceContract(SessionMode = SessionMode.Required,CallbackContract = typeof(ISampleClientContract))]
    public interface IService1
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();
        [OperationContract(IsOneWay = false, IsTerminating = true)]
        void Unsubscribe();
        [OperationContract(IsOneWay = true)]
        void PublishMsg(string msg);
    }

    [ServiceContract]
    public interface ISampleClientContract
    {
        [OperationContract(IsOneWay = true)]
        void sendMessage(string msg);
    }
}
