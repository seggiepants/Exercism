using System.Diagnostics.CodeAnalysis;
using Microsoft.Testing.Extensions.TrxReport.Abstractions;
using Xunit.Sdk;

// You could have put in the description that you already defined Incident, Foul, Injury and Manager. I made a class for each
// only to be told it is a duplicate.

public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum)
    {
        switch (shirtNum)
        {
            case 1:
                return "goalie";
            case 2:
                return "left back";
            case 3:
            case 4:
                return "center back";
            case 5:
                return "right back";
            case 6:
            case 7:
            case 8:
                return "midfielder";
            case 9:
                return "left wing";
            case 10:
                return "striker";
            case 11:
                return "right wing";
            default:
                return "UNKNOWN";
        }
    }

    public static string AnalyzeOffField(object report)
    {
        switch (report)
        {
            case int attendance:
                return $"There are {attendance} supporters at the match.";
            case string message:
                return message;
            case Injury injury:
                return $"Oh no! {injury.GetDescription() ?? ""} Medics are on the field.";
            case Incident incident:
                return incident.GetDescription() ?? "";
            case Manager manager:
                if (manager.Club is null)
                {
                    return manager.Name;
                }
                return $"{manager.Name} ({manager.Club})";
            default:
                return "";
        }
    }
}
