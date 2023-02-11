using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUiText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private string fixedText;
    public void OnSliderValueChaned(float numricValue)
    {
        text.text = $"{fixedText}: {numricValue}";
    }
}
