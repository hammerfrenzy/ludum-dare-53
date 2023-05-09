using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<Text> names;
    [SerializeField]
    private List<Text> formattedTimes;

    private string publicLeaderboardKey = 
        "bcc27b2b66db037bcc67809b4c277dd854f8f56ef0ce30906ab1bc4157ce7a4a";

    //private string secretLeaderboardKey =
    //    "2fa9c9e1a0d7f883ee9adbb2fcda2e39d77c53ba56834a9e54c97967b18f30c96ecec47117415ce2363cb88a1921bc54960b897916830d8bb8ce869119fdb722dacc163af79e2a217b0dc1c8e121a537c60993b554457e9a0e49eb3557e44e54b454064191bc855dce11be9e83d43f004ee90c77c0995fd083c8916082eafe7e";

    private bool isInAscendingOrder = true;

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, isInAscendingOrder, ((msg) =>{

            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;

            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                formattedTimes[i].text = msg[i].Extra;
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int time, string formattedTime)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, time, formattedTime,
            ((msg => {
                GetLeaderBoard();
            })));
    }
}
