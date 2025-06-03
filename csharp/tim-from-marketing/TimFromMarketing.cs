using Xunit.Sdk;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        string dept = department ?? "OWNER";
        dept = dept.ToUpperInvariant();

        if (id is null)
            return $"{name} - {dept}";
        return $"[{id}] - {name} - {dept}";
    }
}
