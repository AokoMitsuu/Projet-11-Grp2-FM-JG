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
    
    public NPC(string name, List<TraitSo> traits)
    {
        _name = name;
        _traits = traits;
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


        List<TraitSo> ageTraits = _traits.Where(trait => trait.Tag == ETag.Age).ToList();
        List<TraitSo> Traits = _traits.Where(trait => trait.Tag == ETag.Positif).ToList();
        List<TraitSo> negativeTraits = _traits.Where(trait => trait.Tag == ETag.Negatif).ToList();

        foreach (TraitSo trait in ageTraits)
        {
            _summary += trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)];
        }

        foreach (TraitSo trait in Traits)
        {
            if(Traits.First() != trait)
            {
                _summary = _summary.Substring(0, _summary.Length - 1);
                _summary += trait.ComplementaryPrefixSentece[Random.Range(0, trait.ComplementaryPrefixSentece.Count - 1)].ToLower() + " ";
                _summary += trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)].ToLower() + " ";
            }
            else
            {
                _summary += trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)];
            }
        }

        foreach (TraitSo trait in negativeTraits)
        {
            if (negativeTraits.First() != trait)
            {
                _summary = _summary.Substring(0, _summary.Length - 1);
                _summary += trait.ContradictionPrefixSentece[Random.Range(0, trait.ContradictionPrefixSentece.Count - 1)].ToLower() + " ";
                _summary += trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)].ToLower() + " ";
            }
            else
            {
                _summary += trait.SufixSentece[Random.Range(0, trait.SufixSentece.Count - 1)];
            }
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
        GenerateSummary();
    }
}