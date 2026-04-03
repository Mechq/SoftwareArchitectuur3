using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;

namespace AvansDevOps.User;

public class ProductOwner : User
{
    public ProductOwner(string name, string email, string password, int phoneNumber) : base(name, email, password, phoneNumber) {}

}