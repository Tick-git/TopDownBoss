
public static class DataPaths
{
    private const string DataPath = "Data/";
    
    private const string CorePath = DataPath + "Core/";
    private const string GameplayPath = DataPath + "Gameplay/";

    public static class CoreData
    {
        public const string AudioPath = CorePath + "Audio/";
        public const string Vfx = CorePath + "Vfx/";
        public const string Input = CorePath + "Input/";
        public const string Pool = CorePath + "Pool/";
    }
    
    public static class GameplayData
    {
        public const string ComponentPath = GameplayPath + "Components/";
        public const string PlayerPaths = GameplayPath + "Components/";
    }
}
