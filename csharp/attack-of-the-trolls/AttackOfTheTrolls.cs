// This is more of a bit operation exercise than a flag attribute one.
// You don't need any attributes for the account type and just [Flags] for the Permissions enumeration.
// The real trick is ~ is the bitwise negate operation to flip all of a values bits from 1 -> 0 or vice versa.
// then you can use | and & to do bitwise and/or operations.
// WARNING: It looks like you can make permissions that don't exist in the Permissions enumeration. Not sure if the compiler checks or not.

// TODO: define the 'AccountType' enum
enum AccountType
{
    Guest,
    User,
    Moderator
}

// TODO: define the 'Permission' enum
[Flags]
enum Permission
{
    Read =   0b00000001,
    Write =  0b00000010,
    Delete = 0b00000100,
    All =    0b00000111,
    None =   0b00000000
}

static class Permissions
{
    public static Permission Default(AccountType accountType)
    {
        switch (accountType)
        {
            case AccountType.Guest:
                return Permission.Read;
            case AccountType.User:
                return Permission.Read | Permission.Write;
            case AccountType.Moderator:
                return Permission.Read | Permission.Write | Permission.Delete;
        }
        return Permission.None;
    }

    public static Permission Grant(Permission current, Permission grant)
    {
        return current | grant;
    }

    public static Permission Revoke(Permission current, Permission revoke)
    {
        return current & ~revoke;
    }

    public static bool Check(Permission current, Permission check)
    {
        return (current & check) == check;
    }
}
