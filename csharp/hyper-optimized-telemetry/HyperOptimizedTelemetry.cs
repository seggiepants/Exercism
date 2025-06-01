using System.ComponentModel;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        byte[] buffer = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        // Couple of problems
        // 1. Our supposedly hyper optimized routine is always sending 9 bytes. You could have made it shorter by just sending longs without the header bit.
        // 2. Converting from the buffer back to a long is using things like BitConverter.ToUInt16 for shorts. However, the length of a short may differ over time or system. So this only works as long as UInt16 == UShort which is not guaranteed.
        // 3. Tests mark you wrong if you don't follow their exact pattern. However, I think my first draft was returning valid data just not exactly the type desired. I was opting for unsigned values for positive numbers at first. The tests should test it works instead of works the way I wrote it. UShort and Short can both hold 5 and neither is a wrong answer.

        if (reading > uint.MaxValue && reading <= long.MaxValue)
        {
            buffer[0] = 256 - sizeof(long);
            byte[] bits = BitConverter.GetBytes((long)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading > int.MaxValue && reading <= uint.MaxValue)
        {
            buffer[0] = sizeof(uint);
            byte[] bits = BitConverter.GetBytes((uint)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading > ushort.MaxValue & reading <= int.MaxValue)
        {
            buffer[0] = 256 - sizeof(int);
            byte[] bits = BitConverter.GetBytes((int)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading >= ushort.MinValue & reading <= ushort.MaxValue)
        {
            buffer[0] = sizeof(ushort);
            byte[] bits = BitConverter.GetBytes((ushort)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading < ushort.MinValue & reading >= short.MinValue)
        {
            buffer[0] = 256 - sizeof(short);
            byte[] bits = BitConverter.GetBytes((short)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading >= int.MinValue & reading < ushort.MinValue)
        {
            buffer[0] = 256 - sizeof(int);
            byte[] bits = BitConverter.GetBytes((int)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        else if (reading >= long.MinValue && reading < int.MinValue)
        {
            buffer[0] = 256 - sizeof(long);
            byte[] bits = BitConverter.GetBytes((long)reading);
            for (int i = 0; i < bits.Length; i++)
                buffer[i + 1] = bits[i];
        }
        return buffer;
    }

    public static long FromBuffer(byte[] buffer)
    {
        bool signed = buffer[0] > sizeof(ulong);
        int numBytes = signed ? byte.MaxValue - buffer[0] + 1 : buffer[0];

        if (numBytes > 8)
            return 0;
        
        if (signed)
        {
            if (numBytes == sizeof(short))
            {
                return (long)BitConverter.ToInt16(buffer, 1);
            }
            else if (numBytes == sizeof(int))
            {
                return (long)BitConverter.ToInt32(buffer, 1);
            }
            else
            {
                return (long)BitConverter.ToInt64(buffer, 1);
            }
        }
        else
        {
            if (numBytes == sizeof(ushort))
            {
                return (long)BitConverter.ToUInt16(buffer, 1);
            }
            else if (numBytes == sizeof(uint))
            {
                return (long)BitConverter.ToUInt32(buffer, 1);
            }
            else
            {
                return (long)BitConverter.ToUInt64(buffer, 1);
            }
        }
    }
}
