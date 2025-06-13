public class Orm
{
    private Database database;

    public Orm(Database database)
    {
        this.database = database;
    }

    public void Write(string data)
    {
        using (this.database)
        {
            this.database.BeginTransaction();
            this.database.Write(data);
            this.database.EndTransaction();
        }
    }

    public bool WriteSafely(string data)
    {
        using (this.database)
        {
            try
            {
                this.database.BeginTransaction();
                this.database.Write(data);
                this.database.EndTransaction();
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        return true;
    }
}
