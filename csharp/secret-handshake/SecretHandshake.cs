public static class SecretHandshake
{
    public static string[] Commands(int commandValue)
    {
        int reverseFlag = 0b10000;
        Dictionary<int, string> actions = new()
        {
            [0b00001] = "wink",
            [0b00010] = "double blink",
            [0b00100] = "close your eyes",
            [0b01000] = "jump",
        };
        bool reverse = (commandValue & reverseFlag) != 0;
        var commands = (from pair in actions
                        where (pair.Key & commandValue) != 0
                        select pair.Value);
        if (reverse)
            commands = commands.Reverse<string>();

        return commands.ToArray<string>();

    }
}
