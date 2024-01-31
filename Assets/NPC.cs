using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class NPC
{
    [SerializeField] private string _name;
    public List<TraitSo> _traits;
    public string _summary;

    public NPC(string name, List<TraitSo> traits)
    {
        _name = name;
        _traits = traits;
        GenerateSummary();
    }

    private void GenerateSummary()
    {
        // Générer un résumé basé sur les traits
        _summary = $"Je suis {_name}. Mes traits principaux sont: \n";
        foreach(TraitSo trait in _traits)
        {
            _summary += trait.Description;
            if (_traits.Last() != trait)
                _summary += "\n";
        }
    }
}