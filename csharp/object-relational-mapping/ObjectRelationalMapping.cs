public class Orm : IDisposable
{
    private Database database;

    public Orm(Database database)
    {
        this.database = database;
    }

    void IDisposable.Dispose()
    {
        this.database.Dispose();
    }


    public void Begin()
    {
        if (database.DbState != Database.State.Closed)
            throw new InvalidOperationException("Database not in closed state.");
        database.BeginTransaction();
    }

    public void Write(string data)
    {
        try
        {
            database.Write(data);
        }
        catch (InvalidOperationException)
        {
            database.Dispose();
        }
    }

    public void Commit()
    {
        try
        {
            database.EndTransaction();
        }
        catch (InvalidOperationException)
        {
            database.Dispose();
        }
    }
}
