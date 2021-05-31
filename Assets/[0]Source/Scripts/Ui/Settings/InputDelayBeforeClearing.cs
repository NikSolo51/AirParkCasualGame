using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDelayBeforeClearing : MonoBehaviour
{
    [SerializeField]private InputField inputField;
    [SerializeField] private ControlPeople _controlPeople;
    private float value;

    private void Start()
    {
        if (GetComponent<InputField>())
            inputField = GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate {ChangeTimeBeforeClear(); });
    }

    public void ChangeTimeBeforeClear()
    {
        if (float.TryParse(inputField.text, out value))
            value = float.Parse(inputField.text);
        _controlPeople.timeToClear = value;
    }
    
}
