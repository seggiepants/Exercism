public static class ErrorHandling
{
    public static void HandleErrorByThrowingException()
    {
        throw new Exception("Is this what you want?");
    }

    public static int? HandleErrorByReturningNullableType(string input)
    {
        bool success = int.TryParse(input, out int result);
        return success ? result : null;
    }

    public static bool HandleErrorWithOutParam(string input, out int result)
    {
        return int.TryParse(input, out result);        
    }

    public static void DisposableResourcesAreDisposedWhenExceptionIsThrown(IDisposable disposableObject)
    {
        // Looks like I should have had a using clause here
        // instead of a try/finally. Some actual instructions
        // of what the problem wanted sure could have cleared
        // that up.
        using (disposableObject)
        {
            throw new Exception("Is this what you want?");
        }
    }
}
