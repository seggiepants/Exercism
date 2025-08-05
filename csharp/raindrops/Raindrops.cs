public static class Raindrops
{
    public static string Convert(int number)
    {
        string sounds = "";
        if (number % 3 == 0)
        {
            sounds += "Pling";            
        }
        if (number % 5 == 0)
        {
            sounds += "Plang";            
        }
        if (number % 7 == 0)
        {
            sounds += "Plong";
        }
        return sounds.Length > 0 ? sounds : number.ToString();
    }
}