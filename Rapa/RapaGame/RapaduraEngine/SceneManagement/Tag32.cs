using System.Collections.Generic;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public class Tag32
{
    #region properties

    public static byte TotalTags { get; private set; }
    
    public int Id { get; }

    private uint Value { get; }
    
    public string Name { get; private set; }
    
    #endregion
    
    #region constuctors

    public Tag32(string tagName)
    {
        Id = TotalTags;
        Value = 1U << TotalTags;
        Name = tagName;

        TagIdList[Id] = this;
        TagNameList[tagName] = this;
        TotalTags++;
    }

    #endregion

    #region methodes

    public static implicit operator uint(Tag32 tag) => tag.Value;
    
    public static Tag32 GetTag(string tag) => TagNameList[tag]; //Tag32.GetTag(name) to get the direct value

    #endregion
    
    #region fields

    private static readonly Tag32[] TagIdList = new Tag32[32];
    
    private static readonly Dictionary<string, Tag32> TagNameList = new();

    #endregion
}