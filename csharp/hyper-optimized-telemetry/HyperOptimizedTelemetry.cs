using System.ComponentModel;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        byte[] buffer = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
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
