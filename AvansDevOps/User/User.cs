using AvansDevOps.ProjectManagement;

namespace AvansDevOps.User;

public abstract class User
{
    private readonly String _name;
    private readonly String _email;
    private readonly String _password;
    private readonly int _phoneNumber;
    

    protected User(string name, string email, string password, int phoneNumber)
    {
        _name = name;
        _email = email;
        _password = password;
        _phoneNumber = phoneNumber;
    }

    public string GetName()
    {
        return _name;
    }
    
    public string GetEmail()
    {
        return _email;
    }

    public override string ToString(){return _name + " " + _email + " " + _password + " " + _phoneNumber;}
}