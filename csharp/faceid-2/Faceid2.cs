public class FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }

    // TODO: implement equality and GetHashCode() methods

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != typeof(FacialFeatures))
            return false;

        return EyeColor == ((FacialFeatures)obj).EyeColor && PhiltrumWidth == ((FacialFeatures)obj).PhiltrumWidth;
    }

    public override int GetHashCode()
    {
        int textSum = EyeColor.Sum(c => (int)c);
        int philtrum = (int)(Math.Round(PhiltrumWidth, 4) * 10000);
        return ((philtrum & 0xffff) << 16) | (textSum & 0xffff);
    }


}

public class Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }
    // TODO: implement equality and GetHashCode() methods
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != typeof(Identity))
            return false;

        return Email == ((Identity) obj).Email && FacialFeatures.Equals(((Identity)obj).FacialFeatures);
    }

    public override int GetHashCode()
    {
        return ((Email.GetHashCode() & 0xffff) << 16) | (FacialFeatures.GetHashCode() & 0xffff);
    }

}

public class Authenticator
{
    readonly HashSet<Identity> registered = [];

    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {
        return faceA.Equals(faceB);
    }

    public bool IsAdmin(Identity identity)
    {
        const string ADMIN_EMAIL = "admin@exerc.ism";
        FacialFeatures ff = new("green", 0.9m);
        return identity.Email.Equals(ADMIN_EMAIL) && identity.FacialFeatures.Equals(ff);
    }
    public bool Register(Identity identity)
    {
        if (IsRegistered(identity)) // Dogfood your code.
            return false;
        return registered.Add(identity); 
    }

    public bool IsRegistered(Identity identity)
    {
        return registered.Contains(identity);
    }

    public static bool AreSameObject(Identity identityA, Identity identityB)
    {
        // Should mention Object.ReferenceEquals in the learning text.
        return Object.ReferenceEquals(identityA, identityB);
    }
}
