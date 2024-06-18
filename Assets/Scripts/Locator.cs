using UnityEngine;

public static class Locator
{
    private static GameManager _gameManager;

    public static void ProvideGameManager(GameManager gm) { _gameManager = gm; }
    public static GameManager GameManager { get => _gameManager; }

}
