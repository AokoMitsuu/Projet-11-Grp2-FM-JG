using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class NPC
{
    public Action OnTraitsChange;

    public string Name => _name;
    [SerializeField] private string _name;

    public List<TraitSo> Traits => _traits;
    [SerializeField] private List<TraitSo> _traits;

    public string Summary => _summary;
    [SerializeField] private string _summary;

    private List<TraitSo> _oldTraits;
    

    public NPC(string name, List<TraitSo> traits)
    {
        _name = name;
        _traits = traits;
        _oldTraits = new List<TraitSo>(_traits);
        GenerateSummary();
    }

    public void AddTraits(TraitSo trait)
    {
        if (_traits.Contains(trait))
            return;

        _traits.Add(trait);

        RemoveConflicts(_traits[_traits.Count - 1].ConflictTrait);
    }

    private void GenerateSummary()
    {
        // Générer un résumé basé sur les traits
        _summary = $"Je suis {_name}.\n";
        foreach(TraitSo trait in _traits)
        {
            _summary += trait.PrefixSentece[Random.Range(0, trait.PrefixSentece.Count - 1)] + trait.Name.ToLower() + trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)];
        }

        OnTraitsChange?.Invoke();
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
        GenerateSummary();
    }
}