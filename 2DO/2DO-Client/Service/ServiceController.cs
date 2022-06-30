// //////////////////////////////////////////////////////////////////////////////////////////////////////
// FileName: ServiceController.cs
// Author : Felix Wochele
// Created On : 21062022
// Description : Service related Stuff 
// //////////////////////////////////////////////////////////////////////////////////////////////////////


// btw. resharper trial with dhbw mail does not work properly ... created new acc with new trial ... cause of that some class has a header and some not 

using System.ServiceModel;
using ServiceReference1;

namespace _2DO_Client.Service;

public class ServiceController
{

    public ToDoServiceClient mToDoService { get; }
    private string serviceAddress = "http://localhost:8733/2DO-Service/Test";


    public ServiceController()
    {
        //WCF Connetction
        BasicHttpBinding binding = new BasicHttpBinding();
        //binding.Security.Mode = SecurityMode.Message;
        var address = new EndpointAddress(serviceAddress);
        mToDoService = new ToDoServiceClient(binding, address);
    }

}