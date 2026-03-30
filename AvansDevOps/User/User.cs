using AvansDevOps.ProjectManagement;

namespace AvansDevOps.User;

public abstract class User
{
    private String _name;
    private String _email;
    private String _password;
    private int _phoneNumber;
    

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

    public override string ToString(){return _name + " " + _email + " " + _password + " " + _phoneNumber;}
}