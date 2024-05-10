using LegacyApp.Business.Abstract;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LegacyApp.Business.Concrete;

public partial class UserCreditServiceClient : ClientBase<IUserCreditService>, IUserCreditService
{
    public UserCreditServiceClient() { }

    public UserCreditServiceClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    { }

    public UserCreditServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    { }

    public UserCreditServiceClient(Binding binding, EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    { }

    public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
    {
        return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
    }
}
