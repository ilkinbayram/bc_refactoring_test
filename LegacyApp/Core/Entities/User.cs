using LegacyApp.Core.Entities.Abstract;

namespace LegacyApp.Core.Entities
{
    public class User : IEntity
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public bool HasCreditLimit { get; set; }
        public int CreditLimit { get; set; }
    }
}
