using UnityEngine;

[CreateAssetMenu]
public class FeedbackData : ScriptableObject
{
    [TextArea(5,7)]
    public string FeedbackText1;
    [TextArea(5,7)]
    public string FeedbackText2;
}
