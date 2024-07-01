namespace TurnJam2
{
    public static class Locator
    {
        public static void ProvideGameManager(GameManager gm)
        {
            GameManager = gm;
        }

        public static void ProvideAudioHandler(AudioHandler audioHandler) => AudioHandler = audioHandler;

        public static GameManager GameManager { get; private set; }
        public static AudioHandler AudioHandler { get; private set; }
    }
}