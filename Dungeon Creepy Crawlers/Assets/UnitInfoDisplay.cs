using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInfoDisplay : MonoBehaviour
{
    public string unitName;
    public Sprite unitImage;

    public Image unitImageObject;
    public TextMeshProUGUI unitNameText;
    public string[] abilities;

    private void OnEnable()
    {
        unitNameText.text = unitName;
        unitImageObject.sprite = unitImage;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
