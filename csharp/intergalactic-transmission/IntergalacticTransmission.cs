public static class IntergalacticTransmission
{
    public static byte[] GetTransmitSequence(byte[] message)
    {
        List<byte> ret = new();
        Queue<Byte> bits = new();
        foreach(byte value in message)
        {
            int temp = value;
            for(int i = 0; i < 8; i++)
            {
                bits.Enqueue((byte)((temp & 0b10000000) != 0 ? 0b1 : 0b0));
                temp = temp << 1;
            }
            while (bits.Count >= 7)
            {
                int ones = 0;
                int result = 0;
                for(int i = 0; i < 7; i++)
                {
                    byte bit = bits.Dequeue();
                    if (bit == 1)
                        ones++;
                    result = (result << 1) | bit;
                }
                result = ones % 2 == 0 ? result << 1 : (result << 1) | 1;
                ret.Add((byte)result);
            }
        }
        if (bits.Count > 0)
        {
            // Add the last byte
            int extraZeros = 7 - bits.Count;
            int result = 0;
            int ones = 0;
            while(bits.Count > 0)
            {
                int bit = bits.Dequeue();
                if (bit == 1)
                    ones++;
                result = (result << 1) | bit;
            }
            if (extraZeros > 0)
                result = result << extraZeros;

            result = ones % 2 == 1 ? (result << 1) | 1 : (result << 1) | 0;
            ret.Add((byte)result);
        }
        return ret.ToArray<byte>();
    }

    public static byte[] DecodeSequence(byte[] receivedSeq)
    {
        if (receivedSeq.Length == 0)
            return [];

        List<byte> ret = new();
        Queue<Byte> bits = new();
        foreach(byte value in receivedSeq)
        {
            int temp = value;
            byte[] tempBits = { 0, 0, 0, 0, 0, 0, 0, 0};
            int ones = 0;
            for(int i = 0; i < 8; i++)
            {
                int bit = temp & 0b1;
                tempBits[i] = (byte)bit;
                if (bit != 0) 
                    ones++;
                temp = temp >> 1;
            }
            if (ones % 2 != 0)
                throw new ArgumentException("Parity Error");
            
            for (int i = 7; i >= 1; i--)
            {
                bits.Enqueue((byte)tempBits[i]);

            }
        }
        while (bits.Count >= 8)
        {
            int result = 0;
            for(int i = 0; i < 8; i++)
            {
                byte bit = bits.Dequeue();
                result = (result << 1) | bit;
            }
            ret.Add((byte)result);
        }
        
        return ret.ToArray<byte>();
    }
}
