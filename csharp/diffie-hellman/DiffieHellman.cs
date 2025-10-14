using System.Numerics;

public static class DiffieHellman
{
    static Random r = new();
    public static BigInteger PrivateKey(BigInteger primeP) 
    {
        int nBytes = primeP.GetByteCount();
        byte[] randBytes = new byte[nBytes];
        while (true)
        {
            for(int i = 0; i < nBytes;i++)
            {
                r.NextBytes(randBytes);
                randBytes[0] = (byte)(0x7F & randBytes[0]); // try to make it unsigned.
                BigInteger privateKey = new(randBytes);
                if ((BigInteger.Compare(privateKey, BigInteger.Zero) > 0) &&
                    (BigInteger.Compare(privateKey, primeP) < 0))
                {
                    return privateKey;
                }
            }
        }
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey) 
    {
        return BigInteger.ModPow(primeG, privateKey, primeP); 
    }

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey) 
    {
        return BigInteger.ModPow(publicKey, privateKey, primeP); 
    }
}