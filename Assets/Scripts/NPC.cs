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
        int index = 0;

        List<TraitSo> traits = _traits.Where(trait => trait.Tag == ETag.Other || trait.Tag == ETag.Positif || trait.Tag == ETag.PassionPositif).ToList();
        List<TraitSo> negativeTraits = _traits.Where(trait => trait.Tag == ETag.Negatif || trait.Tag == ETag.PassionNegatif).ToList();

        _summary += CreateSubSummary(traits, ref index, false);
        _summary += CreateSubSummary(negativeTraits, ref index, traits.Count > 0);

        OnTraitsChange?.Invoke();
    }

    private string CreateSubSummary(List<TraitSo> traits, ref int index, bool isContradiction)
    {
        var subSumary = "";
        foreach (TraitSo trait in traits)
        {
            if (isContradiction && traits.First() == trait)
            {
                subSumary += trait.ContradictionPrefixSentece[Random.Range(0, trait.ContradictionPrefixSentece.Count)] + " ";
            }
            else if (traits.First() != trait)
            {
                subSumary += trait.ComplementaryPrefixSentece[Random.Range(0, trait.ComplementaryPrefixSentece.Count)].ToLower();
            }

            if(index == 0)
                subSumary += trait.SufixSentece[index];
            else
                subSumary += trait.SufixSentece[index].ToLower();

            if (traits.Last() == trait)
            {
                subSumary += ". ";
            }
            else
            {
                subSumary += " ";
            }

            index++;
        }

        return subSumary;
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