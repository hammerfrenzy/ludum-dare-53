using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private InputField inputName;
    [SerializeField]
    private Text inputFormattedScore;
    [SerializeField]
    private Text inputTimeMilliseconds;

    public UnityEvent<string, int, string> submitScoreEvent;

    public void SubmitScore()
    {
        Debug.Log("From Score Manager: " + inputName.text + ", " + int.Parse(inputTimeMilliseconds.text) + ", " + inputFormattedScore.text );
        if (submitScoreEvent != null)
        {
            submitScoreEvent.Invoke(inputName.text, int.Parse(inputTimeMilliseconds.text), inputFormattedScore.text);
        }
    }
}
