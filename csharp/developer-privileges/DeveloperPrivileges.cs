using System.Security.Cryptography;

public class Authenticator
{
    private Identity _sysAdmin = new()
    {
        Email = "admin@ex.ism",
        FacialFeatures = new FacialFeatures() { EyeColor = "green", PhiltrumWidth = 0.9m },
        NameAndAddress = [ "Chanakya", "Mumbai", "India" ]
    };

    private Dictionary<string, Identity> _developers = new()
    {
        ["Bertrand"] = new Identity()
        {
            Email = "bert@ex.ism",
            FacialFeatures = new FacialFeatures() { EyeColor = "blue", PhiltrumWidth = 0.8m },
            NameAndAddress = ["Bertrand", "Paris", "France"]
        },
        ["Anders"] = new Identity()
        {
            Email = "anders@ex.ism",
            FacialFeatures = new FacialFeatures() { EyeColor = "brown", PhiltrumWidth = 0.85m },
            NameAndAddress = ["Anders", "Redmond", "USA"]},
    };
    // TODO: Implement the Authenticator.Admin property
    public Identity Admin
    {
        get
        {
            return _sysAdmin;
        }
    }

    // TODO: Implement the Authenticator.Developers property
    public IDictionary<string, Identity> Developers
    {
        get
        {
            return _developers;
        }
    }
}

//**** please do not modify the FacialFeatures class ****
public class FacialFeatures
{
    public required string EyeColor { get; set; }
    public required decimal PhiltrumWidth { get; set; }
}

//**** please do not modify the Identity class ****
public class Identity
{
    public required string Email { get; set; }
    public required FacialFeatures FacialFeatures { get; set; }
    public required IList<string> NameAndAddress { get; set; }
}
