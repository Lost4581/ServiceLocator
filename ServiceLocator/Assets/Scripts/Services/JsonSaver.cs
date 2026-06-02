using System.IO;
using UnityEngine;

public class JsonSaver : ISaver
{
    private readonly Score _score;

    public JsonSaver(Score score)
    {
        _score = score;
    }

    public void SaveScore(string path = null)
    {
        if (string.IsNullOrEmpty(path))
            path = Path.Combine(Application.persistentDataPath, "score.json");

        var data = new ScoreData { value = _score.Value };
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
        Debug.Log($"[JsonSaver] Сохранено: {_score.Value} в {path}");
    }

    [System.Serializable]
    private class ScoreData
    {
        public int value;
    }
}