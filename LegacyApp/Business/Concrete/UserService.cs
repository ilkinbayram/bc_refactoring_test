using LegacyApp.Business.Abstract;
using LegacyApp.Core.Entities;
using LegacyApp.Core.Repositories.Abstracts;
using LegacyApp.Core.Resources.Constant;
using LegacyApp.DataAccess.Abstract;

namespace LegacyApp.Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IClientRepository _clientRepository;
    private readonly IUserCreditService _userCreditService;

    public UserService(IUserDal userDal, IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        _userDal = userDal ?? throw new ArgumentNullException(nameof(userDal));
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _userCreditService = userCreditService ?? throw new ArgumentNullException(nameof(userCreditService));
    }

    public bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
    {
        if (!ValidateUserInput(firstname, surname, email, dateOfBirth))
            return false;

        var client = _clientRepository.GetById(clientId);
        if (client == null)
            return false;

        var user = CreateUser(firstname, surname, email, dateOfBirth, client);
        if (!ValidateUserAge(dateOfBirth) || !CheckCreditPolicy(user))
            return false;

        _userDal.AddUser(user);
        return true;
    }

    private bool ValidateUserInput(string firstname, string surname, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
            return false;
        if (email.Contains("@") && !email.Contains("."))
            return false;
        return true;
    }

    private bool ValidateUserAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            age--;
        return age >= 21;
    }

    private bool CheckCreditPolicy(User user)
    {
        switch (user.Client.Name)
        {
            case ClientNames.VeryImportantClient:
                user.HasCreditLimit = false;
                return true;
            case ClientNames.ImportantClient:
                user.HasCreditLimit = true;
                var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                user.CreditLimit = creditLimit * 2;
                break;
            default:
                user.HasCreditLimit = true;
                user.CreditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                break;
        }
        return user.HasCreditLimit && user.CreditLimit >= 500;
    }

    private User CreateUser(string firstname, string surname, string email, DateTime dateOfBirth, Client client)
    {
        return new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            Firstname = firstname,
            Surname = surname
        };
    }
}