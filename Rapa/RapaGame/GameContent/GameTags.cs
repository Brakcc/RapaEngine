using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.GameContent;

public static class GameTags
{
    public static void Init()
    {
        Default = new Tag32("Default");
        Player = new Tag32("Player");
        Test = new Tag32("Test");
        Test2 = new Tag32("Test2");
    }

    public static Tag32 Default;
    
    public static Tag32 Player;

    public static Tag32 Test;
    
    public static Tag32 Test2;
}