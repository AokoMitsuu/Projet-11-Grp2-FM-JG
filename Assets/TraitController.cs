using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TraitController : MonoBehaviour
{
    public TraitSo Trait => _trait;

    [SerializeField] private TMP_Text _text;
    private TraitSo _trait;

    public void Init(TraitSo trait)
    {
        _trait = trait;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _text.text = _trait.Name;
    }
}
