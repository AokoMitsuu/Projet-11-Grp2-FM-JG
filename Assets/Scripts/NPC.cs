using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class NPC
{
    public const int MAX_TRAITS_COUNT = 3;

    public Action OnTraitsChange;

    public string Name => _name;
    [SerializeField] private string _name;

    public List<TraitSo> Traits => _traits;
    [SerializeField] private List<TraitSo> _traits;

    public string Summary => _summary;
    [SerializeField] private string _summary;

    public AnimatedSpriteSO BaseSprite => _basePortrait;
    private AnimatedSpriteSO _basePortrait;
    
    public NPC(string name, List<TraitSo> traits, AnimatedSpriteSO basePortrait)
    {
        _name = name;
        _traits = traits;
        _basePortrait = basePortrait;
        GenerateSummary();
        OnTraitsChange?.Invoke();
    }

    public bool AddTrait(TraitSo trait)
    {
        if (_traits.Contains(trait)) return false;
        if (_traits.Count + 1 > MAX_TRAITS_COUNT) return false;

        _traits.Add(trait);

        RemoveConflicts(_traits[^1].ConflictTrait);

        GenerateSummary();
        OnTraitsChange?.Invoke();

        return true;
    }

    public void RemoveTrait(TraitSo trait)
    {
        _traits.Remove(trait);

        GenerateSummary();
        OnTraitsChange?.Invoke();
    }

    private void GenerateSummary()
    {
        // G�n�rer un r�sum� bas� sur les traits
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
    }
}