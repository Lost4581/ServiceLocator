using UnityEngine;

public class PlayerPrefsSaver : ISaver
{
    private readonly Score _score;
    private const string KEY = "Score";

    public PlayerPrefsSaver(Score score)
    {
        _score = score;
    }

    public void SaveScore(string path = null)
    {
        PlayerPrefs.SetInt(KEY, _score.Value);
        PlayerPrefs.Save();
        Debug.Log($"[PlayerPrefsSaver] Сохранено: {_score.Value}");
    }
}