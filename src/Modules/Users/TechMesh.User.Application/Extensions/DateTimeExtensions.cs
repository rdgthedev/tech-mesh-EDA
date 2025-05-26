namespace TechMesh.User.Application.Extensions;

public static class DateTimeExtensions
{
    public static bool IsBeforeToday(this DateTime dateTime)
        => dateTime.Date < DateTime.UtcNow.Date;

    public static bool IsAfterToday(this DateTime dateTime)
        => dateTime.Date > DateTime.UtcNow.Date;
}