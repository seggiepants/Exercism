public static class VariableLengthQuantity
{
    const uint bit8 = 0b10000000;
    const uint mask7bits = 0b01111111;
    public static uint[] Encode(uint[] numbers)
    {
        bool flag;
        List<uint> values = new();
        Stack<uint> current = new();
        foreach (uint number in numbers)
        {
            uint num = number; // Can't assign to number, loop variable.
            // Break it up into 7 bit sections.
            do
            {
                uint sevenBits = num & mask7bits;
                num = num >> 7;
                current.Push(sevenBits);

            } while (num != 0);

            // Process in reverse order
            do
            {
                uint value = current.Pop();
                flag = current.Count > 0; // false when no more values.
                values.Add(flag ? bit8 | value : value); // all but last get bit 8 set.

            } while (flag);
            
        }
        return values.ToArray<uint>();
    }

    public static uint[] Decode(uint[] bytes)
    {
        List<uint> values = new();        
        bool flag = false;
        uint value = 0;

        foreach (uint number in bytes)
        {
            flag = (number & bit8) == 0; // Stop decoding current number when bit 8 is set.
            value = value << 7; // Move current number over to make room.
            value = value | (number & mask7bits); // add on the next 7 bits.

            if (flag)
            {
                // Reset for next number.
                values.Add(value);
                value = 0;
            }
        }

        if (!flag)
        {
            // If we didn't end with the flag set there should have been more data.
            throw new InvalidOperationException("More data expected.");
        }

        return values.ToArray<uint>();
    }
}