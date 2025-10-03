using System.Text.Json;

// Use of SortedDictionary is just so the tests won't fail. 

public class RestApi
{
    class User
    {
        public string name { get; set; }
        public SortedDictionary<string, decimal> owes { get; set; }
        public SortedDictionary<string, decimal> owed_by { get; set; }
        public Decimal balance
        {
            get
            {
                return (from pair in owed_by select pair.Value).Sum() -
                    (from pair in owes select pair.Value).Sum();
            }
        }

        public User()
        {
            name = "";
            owes = new SortedDictionary<string, decimal>();
            owed_by = new SortedDictionary<string, decimal>();
        }
    }

    class Iou
    {
        public string lender { get; set; }
        public string borrower { get; set; }
        public Decimal amount { get; set; }

        public Iou()
        {
            lender = "";
            borrower = "";
            amount = 0;
        }
    }

    private List<User> database;
    public RestApi(string database)
    {
        List<User>? output = JsonSerializer.Deserialize<List<User>>(database);
        this.database = output == null ? new List<User>() : output;
    }

    public string Get(string url, string? payload = null)
    {
        switch (url)
        {
            case "/users":
                {
                    Dictionary<string, List<string>>? users = JsonSerializer.Deserialize<Dictionary<string, List<string>>?>(payload == null ? "{\"users\": []}" : payload);

                    if (users == null)
                    {
                        // All users if no list.
                        return JsonSerializer.Serialize<List<User>>(database);
                    }
                    else
                    {
                        // Just the ones in the list.
                        return JsonSerializer.Serialize<List<User>>((
                            from User user in database
                            where users["users"].Contains(user.name)
                            orderby user.name
                            select user).ToList<User>());
                    }
                }
        }
        return "";
    }

    public string Post(string url, string payload)
    {
        switch (url)
        {
            case "/add":
                {
                    Dictionary<string, string>? user = JsonSerializer.Deserialize<Dictionary<string, string>?>(payload == null ? "{\"users\": \"\"}" : payload);

                    if (user == null)
                    {
                        // No users if no list.
                        return "";
                    }
                    else
                    {
                        User? newUser = (from User u in database where u.name == user["user"] select u).FirstOrDefault<User?>();
                        if (newUser == null)
                        {
                            newUser = new User();
                            newUser.name = user["user"];
                            database.Add(newUser);
                        }
                        return JsonSerializer.Serialize<User>(newUser);
                    }
                }
            case "/iou":
                {
                    Iou? iou = JsonSerializer.Deserialize<Iou?>(payload == null ? "{}" : payload);
                    if (iou == null)
                        return "[]";

                    User? borrower = (from User user in database where user.name == iou.borrower select user).FirstOrDefault<User?>();
                    if (borrower == null)
                    {
                        borrower = new User();
                        borrower.name = iou.borrower;
                        database.Add(borrower);
                    }

                    User? lender = (from User user in database where user.name == iou.lender select user).FirstOrDefault<User?>();
                    if (lender == null)
                    {
                        lender = new User();
                        lender.name = iou.lender;
                        database.Add(lender);
                    }

                    if (!borrower.owes.ContainsKey(lender.name))
                    {
                        borrower.owes.Add(lender.name, iou.amount);
                    }
                    else
                    {
                        borrower.owes[lender.name] += iou.amount;
                    }

                    if (!lender.owed_by.ContainsKey(borrower.name))
                    {
                        lender.owed_by.Add(borrower.name, iou.amount);
                    }
                    else
                    {
                        lender.owed_by[borrower.name] -= iou.amount;
                    }

                    SimplifyDatabase();

                    // Just the ones in the list.
                    List<User> parties = (from User u in database
                                          where u.name == lender.name || u.name == borrower.name
                                          orderby u.name
                                          select u).ToList<User>();
                    return JsonSerializer.Serialize<List<User>>(parties);
                }
        }
        return "";
    }

    void SimplifyDatabase()
    {
        foreach (User user in database)
        {
            foreach (KeyValuePair<string, decimal> pair in user.owes.ToList())
            {
                if (user.owed_by.ContainsKey(pair.Key))
                {
                    // Owes and owed by.
                    Decimal delta = pair.Value - user.owed_by[pair.Key];
                    if (delta == 0)
                    {
                        // Remove the keys they are even.
                        user.owes.Remove(pair.Key);
                        user.owed_by.Remove(pair.Key);
                    }
                    else if (delta < 0)
                    {
                        // User owes remove, owedby = -delta.
                        user.owes.Remove(pair.Key);
                        user.owed_by[pair.Key] = -1 * delta;
                    }
                    else // delta > 0
                    {
                        // User owes delta, owedby removed
                        user.owes[pair.Key] = delta;
                        user.owed_by.Remove(pair.Key);
                    }
                }
            }
        }
    }
}
