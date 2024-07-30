using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/*
    This class IS NOT used to manage game settings directly, 
    rather this class is supposed to be used to store settings
    (be serialized)

    The correct usage is to add any field/property that need to be saved
    then use SettingsAtribute with the argument of the corresponding
    name. USE ONLY STATIC FIELDS IN YOUR CODE. NON STATIC FIELDS AND
    ANY KIND OF PROPERTIES ARE NOT SUPPORTED

    For example I might have jumpTreshold, JumpThreshold...
    anywhere in the code base. I might choose to add it to this class
    with any name (might choose to use unreadable alias, or non standard
    naming ways). Upon loading the game, using reflection all properties
    will be restored to their states. Upon visiting Initial game menu
    settings.json will be saved.
*/

[Serializable]
public class GameSettingsFile
{
    // used in MainMenu/ThresholdCameraSelector.cs
    [SerializeField]
    int jumpThreshold = 280;
    [SerializeField]
    int duckThreshold = 160;
    [SerializeField]
    bool displayThresholdBars = true;

    //used in MainMenu/SettingsManager.cs
    [SerializeField]
    int faceDetectionFrequency = 18;
    [SerializeField]
    int colorDetectionPixelsToSkip = 1;

    //used in MainMenu/MenuCameraColorSelector.cs
    [SerializeField]
    Color selectedColor = new(0, 0, 0);

    //used in MotionScripts/ColorDetector.cs
    [SerializeField]
    Vector3 colorTolerance = new(0.1f, 0.1f, 0.1f);

    public static GameSettingsFile Instance { get; private set; } = LoadOrCreate();

    private GameSettingsFile() { }

    static GameSettingsFile LoadOrCreate()
    {
        if (File.Exists("settings.json"))
        {
            string json = File.ReadAllText("settings.json");
            var _this = JsonUtility.FromJson<GameSettingsFile>(json);
            _this.RestoreValuesToFields();
            return _this;
        }
        else
        {
            return new();
        }
    }

    public void Save()
    {
        SyncValues();
        string json = JsonUtility.ToJson(this);
        File.WriteAllText("settings.json", json);
    }

    // restores values for any field with SettingsAttribute,
    // across the codebase
    internal void RestoreValuesToFields()
    {
        IEnumerable<FieldInfo> fieldsWithSettingsAttribute = SettingsAttribute.GetAllMembers();
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo originalField in fieldsWithSettingsAttribute) 
        {
            foreach (FieldInfo field in fields)
            {
                if (field.Name == originalField.GetAttribute<SettingsAttribute>().SettingsName)
                {
                    originalField.SetValue(null, field.GetValue(this));
                }
            }
        }
    }

    internal void SyncValues()
    {
        IEnumerable<FieldInfo> fieldsWithSettingsAttribute = SettingsAttribute.GetAllMembers();
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo originalField in fieldsWithSettingsAttribute)
        {
            foreach (FieldInfo field in fields) 
            {
                if (field.Name == originalField.GetAttribute<SettingsAttribute>().SettingsName)
                {
                    field.SetValue(this, originalField.GetValue(null));
                }
            }
        }
    }
}
