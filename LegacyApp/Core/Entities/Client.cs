using LegacyApp.Core.Entities.Abstract;
using LegacyApp.Core.Resources.ValueObjects;

namespace LegacyApp.Core.Entities;

public class Client : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ClientStatus ClientStatus { get; set; }
}
