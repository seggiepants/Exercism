public static class Languages
{
    // I missed the part where you wanted GetExistingLanguages to return a 
    // preset list. I thought we were going to have a static list shared 
    // by all users of the class.
    private static List<string> _languages = new List<string>() { "C#", "Clojure", "Elm" };
    public static List<string> NewList()
    {
        return new List<string>();
    }

    public static List<string> GetExistingLanguages()
    {
        return _languages;
    }

    public static List<string> AddLanguage(List<string> languages, string language)
    {
        languages.Add(language);
        return languages;
    }

    public static int CountLanguages(List<string> languages)
    {
        return languages.Count;
    }

    public static bool HasLanguage(List<string> languages, string language)
    {
        return languages.Contains(language);
    }

    public static List<string> ReverseList(List<string> languages)
    {
        languages.Reverse();
        return languages;        
    }

    public static bool IsExciting(List<string> languages)
    {
        if (languages.Count < 1)
            return false;
        else if (languages[0] == "C#")
            return true;
        else if ((languages.Count == 2 || languages.Count == 3) && languages[1] == "C#")
            return true;
        return false;
    }

    public static List<string> RemoveLanguage(List<string> languages, string language)
    {
        bool removed = languages.Remove(language);
        return languages;
    }

    public static bool IsUnique(List<string> languages)
    {
        HashSet<string> set = new(languages);
        return set.Count == languages.Count;
    }
}
