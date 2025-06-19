using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public delegate void EventWinGame();
    public static EventWinGame eventWinGame;

    public delegate void EventLoseGame();
    public static EventLoseGame eventLoseGame;
}
