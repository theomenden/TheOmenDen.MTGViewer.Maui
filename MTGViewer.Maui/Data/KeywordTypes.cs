using TheOmenDen.Shared.Enumerations;

namespace MTGViewer.Maui.Data;

internal sealed record KeywordTypes(String Name, Int32 Id) : EnumerationBase<KeywordTypes>(Name, Id)
{
    public static readonly KeywordTypes AbilityWords = new(nameof(AbilityWords), 1);
    public static readonly KeywordTypes KeywordAbilities = new(nameof(KeywordAbilities), 2);
    public static readonly KeywordTypes KeywordActions = new(nameof(KeywordActions), 3);
}