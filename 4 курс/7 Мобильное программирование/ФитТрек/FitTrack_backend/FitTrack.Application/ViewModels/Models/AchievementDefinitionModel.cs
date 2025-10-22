namespace FitTrack.Application.ViewModels.Models;

public class AchievementDefinitionModel
{
    public string Id { get; set; } = null!;    // PK  e.g., 'first_workout'
    public string NameKey { get; set; } = null!; // I18N key
    public string DescriptionKey { get; set; } = null!; // I18N key
}
