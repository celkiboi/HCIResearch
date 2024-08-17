using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;
using Unity.VisualScripting;

public class SettingsHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TextMeshProUGUI helperText;
    [SerializeField]
    RawImage textBG;
    [SerializeField]
    string JSONTextID;

    IList<GameObject> canvasChildren;
    GameObject prefabParent;

    void Start()
    {
        canvasChildren = canvas.gameObject.GetChildren();
        prefabParent = this.gameObject.GetParent();
        helperText.text = SettingsTips[JSONTextID];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (GameObject child in canvasChildren) 
        {
            if (child == prefabParent) continue;
            child.SetActive(false);
        }
        textBG.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        foreach (GameObject child in canvasChildren)
        {
            child.SetActive(true);
        }
        textBG.gameObject.SetActive(false);
    }

    static Dictionary<string, string> settingsTips = null;
    static Dictionary<string, string> SettingsTips
    {
        get
        {
            if (settingsTips == null)
            {
                string path = Path.Combine(Application.streamingAssetsPath, "settingsHelper.json");
                string json = File.ReadAllText(path);
                settingsTips = Extensions.DeserializeJson(json);
            }
            return settingsTips;
        }
    }

    
}
