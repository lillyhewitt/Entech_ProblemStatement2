using System;

public class ExceptionHandlingMiddleware
{
    public static void Execute(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private static void HandleException(Exception ex)
    {
        // check if exception is an ArgumentException
        if (ex is ArgumentException)
        {
            Console.WriteLine($"BadRequest: {ex.Message}");
        }
        // check if exception is a custom NullException
        else if (ex is NullReferenceException) // Use NullReferenceException or a custom one
        {
            Console.WriteLine("NotFound: Status 404 error.");
        }
        // handle InvalidOperationException
        else if (ex is InvalidOperationException)
        {
            Console.WriteLine("BadRequest: Sequence contains no elements. Enter a different range.");
        }
        // handle FormatException
        else if (ex is FormatException)
        {
            Console.WriteLine($"BadRequest: {ex.Message}");
        }
        // handle all other exceptions (general 500 error)
        else
        {
            Console.WriteLine("InternalServerError: Sorry, something went wrong. Status 500 error.");
        }
    }
}
