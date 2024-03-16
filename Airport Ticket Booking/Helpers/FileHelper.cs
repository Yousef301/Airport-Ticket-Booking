namespace Airport_Ticket_Booking.Helpers;

public class FileHelper
{
    public static string ConcatPaths(string directoryPath, string fileName) => Path.Combine(directoryPath, fileName);
    
    
}