using System.Text;

public class Robot
{
    private static HashSet<string> robots = new();
    private string name;
    Random random;

    public Robot()
    {
        random = new();
        name = GenerateName();
        Robot.robots.Add(name);
    }

    private string GenerateName()
    {
        int charA = (int)'A';
        int char0 = (int)'0';
        if (Robot.robots.Contains<string>(this.name))
            robots.Remove(this.name);

        string newName;
        do
        {
            StringBuilder sb = new();
            for (int i = 0; i < 2; i++)
            {
                sb.Append((char)(this.random.Next(26) + charA));
            }
            for (int i = 0; i < 3; i++)
            {
                sb.Append((char)(this.random.Next(10) + char0));
            }
            newName = sb.ToString();
        } while (Robot.robots.Contains<string>(newName));
        return newName;
    }
    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public void Reset()
    {
        this.name = GenerateName();
        Robot.robots.Add(this.name);
    }
}