using System.CodeDom.Compiler;
using System.ServiceModel;

namespace LegacyApp.Business.Abstract;

[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "LegacyApp.IUserCreditService")]
public interface IUserCreditService
{
    [OperationContract(Action = "http://totally-real-service.com/IUserCreditService/GetCreditLimit")]
    int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
}
