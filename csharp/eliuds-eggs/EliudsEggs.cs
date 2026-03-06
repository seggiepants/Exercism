public static class EliudsEggs
{
    public static int EggCount(int encodedCount)
    {
        int eggs = 0;
        int encoded = encodedCount;
        while (encoded != 0)
        {
            eggs += encoded & 0b1;
            encoded >>= 1;            
        }
        return eggs;
    }
}
