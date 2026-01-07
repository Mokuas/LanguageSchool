
using LanguageSchoolBYT.Models;


public class CourseLanguageAspect
{
    private HashSet<CourseLanguage> _languages = new();

    public IReadOnlyCollection<CourseLanguage> Languages => _languages;

    public void AddLanguage(CourseLanguage language)
    {
        _languages.Add(language);
    }
}