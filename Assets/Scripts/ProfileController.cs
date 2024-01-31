using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _traitsText;
    [SerializeField] private TMP_Text _sumaryText;

    private NPC _npc;

    public void Init(NPC npc)
    {
        _npc = npc;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _nameText.text = _npc.Name;

        _traitsText.text = "";
        foreach (var trait in _npc.Traits)
        {
            _traitsText.text += "- " + trait.Name;

            if (_npc.Traits.Last() != trait)
                _traitsText.text += "\n";
        }

        _sumaryText.text = _npc.Summary;
    }
}
