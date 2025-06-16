using System.Collections.ObjectModel;

public class Authenticator
{
    private class EyeColor
    {
        public const string Blue = "blue";
        public const string Green = "green";
        public const string Brown = "brown";
        public const string Hazel = "hazel";
        public const string Grey = "grey";
    }

    public Authenticator(Identity admin)
    {
        this.admin = admin;
    }

    private readonly Identity admin;

    private readonly IDictionary<string, Identity> developers
        = new ReadOnlyDictionary<string, Identity>(new Dictionary<string, Identity>
        {
            ["Bertrand"] = new Identity
            {
                Email = "bert@ex.ism",
                EyeColor = "blue"
            },

            ["Anders"] = new Identity
            {
                Email = "anders@ex.ism",
                EyeColor = "brown"
            }
        });

    public Identity Admin
    {
        get { return admin; }
        //set { admin = value; }
    }

    public IDictionary<string, Identity> GetDevelopers()
    {
        return developers;
    }
}

public struct Identity
{
    public string Email { get; set; }

    public string EyeColor { get; set; }

    public Identity(string Email, string EyeColor)
    {
        this.Email = Email;
        this.EyeColor = EyeColor;
    }
}
