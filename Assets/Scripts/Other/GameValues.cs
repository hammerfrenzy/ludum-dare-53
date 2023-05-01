public class GameValues
{
    public static bool IsWin { get; set; }
    public static float TimeInAir { get; set; }

    public static void FakeForGameOver()
    {
        IsWin = false;
        TimeInAir = 473;
    }

    public static void FakeForVictory()
    {
        IsWin = true;
        TimeInAir = 814;
    }
}