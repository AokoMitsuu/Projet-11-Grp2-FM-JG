using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private NPCGenerator _generator;

    [SerializeField] private TMP_Text _currentSeedText;
    [SerializeField] private TMP_InputField _seedInputField;

    private void Start()
    {
        _currentSeedText.text = "" + _generator.CurrentSeed;
    }

    public void Regenerate()
    {
        try
        {
            _generator.Regenerate(int.Parse(_seedInputField.text));
            _currentSeedText.text = "" + _generator.CurrentSeed;
        }
        catch { }
    }
}
