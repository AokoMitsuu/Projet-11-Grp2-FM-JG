using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraitController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _interactablePanel;

    private TraitSo _trait;
    private NPC _linkedNpc;

    private bool _isInteractable = true;

    public TraitSo Trait => _trait;
    public NPC LinkedNpc => _linkedNpc;
    public bool IsInfiniteSource => _linkedNpc == null;
    public bool IsInteractable
    {
        get => _isInteractable;
        set {
            _isInteractable = value;
            _interactablePanel.SetActive(!value);
        }
    }

    private void Awake()
    {
        IsInteractable = true;
    }

    public void Init(TraitSo trait, NPC linkedNpc = null)
    {
        _trait = trait;
        UpdateUI();

        _linkedNpc = linkedNpc;
    }

    public void UpdateUI()
    {
        _text.text = _trait.Name;
    }
}
