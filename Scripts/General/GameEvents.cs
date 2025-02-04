using System;

namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public class GameEvents
{
    public static Action OnStartGame;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
}
