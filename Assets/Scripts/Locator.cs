public static class Locator
{
    public static void ProvideGameManager(GameManager gm)
    {
        GameManager = gm;
    }

    public static GameManager GameManager { get; private set; }
}