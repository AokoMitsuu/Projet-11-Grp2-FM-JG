using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[Serializable]
public class NPC
{
    public string Name => _name;
    [SerializeField] private string _name;

    public List<TraitSo> Traits => _traits;
    [SerializeField] private List<TraitSo> _traits;

    public string Summary => _summary;
    [SerializeField] private string _summary;

    private List<TraitSo> _oldTraits;
    
    public void Update()
    {
        if (_traits.Count > _oldTraits.Count)
        {
            RemoveConflicts(_traits[_traits.Count - 1].ConflictTrait);
        }
        else if (_traits.Count == _oldTraits.Count)
        {
            for (int i = 0; i < _traits.Count; i++)
            {
                if (_traits[i] != _oldTraits[i])
                {
                    RemoveConflicts(_traits[i].ConflictTrait);
                    return;
                }
            }
        }
    }

    public NPC(string name, List<TraitSo> traits)
    {
        _name = name;
        _traits = traits;
        _oldTraits = new List<TraitSo>(_traits);
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

    private void RemoveConflicts(List<TraitSo> conflictTraits)
    {
        var conflicts = _traits.Intersect(conflictTraits).ToList();

        if (conflicts.Any())
        {
            foreach (var conflict in conflicts)
            {
                _traits.Remove(conflict);
            }
        }
        _oldTraits = new List<TraitSo>(_traits);
    }
}