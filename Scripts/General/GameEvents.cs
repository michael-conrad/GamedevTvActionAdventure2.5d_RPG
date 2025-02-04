using System;

namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public static class GameEvents
{
    public static event Action OnStartGame;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
}
