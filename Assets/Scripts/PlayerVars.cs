public static class PlayerVars
{
    private static string characterModel;
    private static string name;

    public static string CharacterModel
    {
        get
        {
            return characterModel;
        }
        set
        {
            characterModel = value;
        }
    }

    public static string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
}