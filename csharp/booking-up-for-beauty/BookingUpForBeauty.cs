static class Appointment
{
    public static DateTime Schedule(string appointmentDateDescription)
    {
        if (!DateTime.TryParse(appointmentDateDescription, out DateTime result))
            return DateTime.Now;
        return result;
    }

    public static bool HasPassed(DateTime appointmentDate)
    {
        return appointmentDate < DateTime.Now;
    }

    public static bool IsAfternoonAppointment(DateTime appointmentDate)
    {
        DateTime beginTime = new(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, 12, 0, 0);
        DateTime endTime = new(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, 18, 0, 0);
        return appointmentDate >= beginTime && appointmentDate < endTime;
    }

    public static string Description(DateTime appointmentDate)
    {
        return $"You have an appointment on {appointmentDate}.";
    }

    public static DateTime AnniversaryDate()
    {
        return new DateTime(DateTime.Now.Year, 9, 15);
    }
}
