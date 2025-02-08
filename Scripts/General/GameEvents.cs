using System;

namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public static class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;

    public static event Action OnVictory;

    public static event Action<int> OnNewEnemyCount;

    public static void RaiseStartGame()
    {
        OnStartGame?.Invoke();
    }

    public static void RaiseEndGame()
    {
        OnEndGame?.Invoke();
    }

    public static void RaiseNewEnemyCount(int count)
    {
        OnNewEnemyCount?.Invoke(count);
    }

    public static void RaiseVictory()
    {
        OnVictory?.Invoke();
    }
}
