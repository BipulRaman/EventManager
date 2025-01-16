using EventManager.App.Api.Extended.Constants;
using EventManager.App.Api.Extended.Models;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace EventManager.App.Api.Extended.Utilities;

public static class SchoolDataHelper
{
    public static List<School> GetSchools()
    {
        List<School> schools = new List<School>();
        string schoolDataFilePath = Directory.GetFiles(Directory.GetCurrentDirectory(), ExtendedConstants.SCHOOL_STATIC_DATA_FILE_NAME, SearchOption.AllDirectories).FirstOrDefault() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(schoolDataFilePath))
        {
            string serializedSchoolData = File.ReadAllText(schoolDataFilePath);
            schools = JsonSerializer.Deserialize<List<School>>(serializedSchoolData) ?? new List<School>();
        }

        return schools;
    }

    public static bool IsValidSchool(int uniqueId)
    {
        bool response = false;
        if (uniqueId > 0)
        {
            var schools = GetSchools();
            response = schools.Exists(s => s.UniqueId == uniqueId);
        }
        return response;
    }

    public static bool IsValidSchools(List<int> uniqueIds)
    {
        bool response = false;
        if (uniqueIds.Any())
        {
            var schools = GetSchools();
            response = uniqueIds.TrueForAll(id => schools.Exists(s => s.UniqueId == id));
        }
        return response;
    }

    public static string GetFullSchoolName(int uniqueId)
    {
        if (IsValidSchool(uniqueId))
        {
            var schools = GetSchools();
            var schoolInfo = schools.Find(s => s.UniqueId == uniqueId);
            return $"JNV {schoolInfo.SchoolName} {schoolInfo.StatePrefix}";
        }
        return null;
    }

    public static string GetFullSchoolsName(List<int> uniqueIds)
    {
        StringBuilder response = new();
        if (IsValidSchools(uniqueIds))
        {
            foreach (var uniqueId in uniqueIds)
            {
                if (uniqueIds.IndexOf(uniqueId) == 0)
                {
                    response.Append($"{GetFullSchoolName(uniqueId)}");
                }
                else
                {
                    response.Append($", {GetFullSchoolName(uniqueId)}");
                }
            }

            return response.ToString();
        }
        return null;
    }

    public static string GetFullSchoolsName(string ids)
    {
        return GetFullSchoolsName(SchoolIdsStringToList(ids));
    }

    private static string ToPascalCase(string str)
    {
        // Split the input sentence into words
        string[] words = str.Split(new char[] { ' ', '\t', '\n', '\r', '\v', '\f', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Create a StringBuilder to build the PascalCase sentence
        StringBuilder pascalCaseSentence = new StringBuilder();

        foreach (string word in words)
        {
            // Convert the first character of each word to uppercase and the rest to lowercase
            string formattedWord = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower());

            // Append the formatted word to the result
            pascalCaseSentence.Append(formattedWord);
        }

        return pascalCaseSentence.ToString();
    }

    public static List<int> SchoolIdsStringToList(string ids)
    {
        if (string.IsNullOrWhiteSpace(ids))
        {
            return new List<int>();
        }

        try
        {
            return ids.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(id => int.Parse(id.Trim()))
                      .ToList();
        }
        catch (FormatException)
        {
            // Handle the case where parsing fails
            return new List<int>();
        }
    }

}
