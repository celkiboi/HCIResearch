using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

[Serializable]
public class HighScores
{
    [SerializeField]
    List<int> keyboardScores;
    [SerializeField]
    List<int> controllerScores;
    [SerializeField]
    List<int> faceScores;
    [SerializeField]
    List<int> colorScores;

    public IList<int> KeyboardScores { get { return keyboardScores.AsReadOnly(); } }
    public IList<int> ControllerScores { get { return controllerScores.AsReadOnly(); } }
    public IList<int> FaceScores { get { return faceScores.AsReadOnly(); } }
    public IList<int> ColorScores { get { return colorScores.AsReadOnly(); } }

    public static HighScores Instance = LoadOrCreate();

    private HighScores() 
    {
        keyboardScores = new();
        controllerScores = new();
        faceScores = new();
        colorScores = new();
    }

    static HighScores LoadOrCreate()
    {
        if (File.Exists("highScores.json"))
        {
            string json = File.ReadAllText("highScores.json");
            return JsonUtility.FromJson<HighScores>(json);
        }
        else
            return new();
    }

    public void AddKeyboardScore(int score)
    {
        keyboardScores.Add(score);
        Save();
    }

    public void AddControllerScore(int score)
    {
        controllerScores.Add(score);
        Save();
    }

    public void AddFaceScore(int score)
    {
        faceScores.Add(score);
        Save();
    }

    public void AddColorScore(int score)
    {
        colorScores.Add(score);
        Save();
    }

    void Save()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText("highScores.json", json);
    }
}