using AvansDevOps.ProjectManagement;

namespace AvansDevOps.User;

public class ProductOwner : User
{
    private List<ProjectComposite> _projects = [];
    public ProductOwner(string name, string email, string password, int phoneNumber) : base(name, email, password, phoneNumber) {}

}