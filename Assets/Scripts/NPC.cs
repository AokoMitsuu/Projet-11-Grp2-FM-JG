using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        _summary = $"Je suis {_name}.\n";
        int index = 0;

        List<TraitSo> traits = _traits.Where(trait => trait.Tag == ETag.Other || trait.Tag == ETag.Positif || trait.Tag == ETag.PassionPositif).ToList();
        traits.Shuffle();
        List<TraitSo> negativeTraits = _traits.Where(trait => trait.Tag == ETag.Negatif || trait.Tag == ETag.PassionNegatif).ToList();
        negativeTraits.Shuffle();

        _summary += CreateSubSummary(traits, ref index, false);
        _summary += CreateSubSummary(negativeTraits, ref index, traits.Count > 0);
    }

    private string CreateSubSummary(List<TraitSo> traits, ref int index, bool isContradiction)
    {
        var subSumary = "";
        foreach (TraitSo trait in traits)
        {
            SentenceParams sentenceParams = trait.SentenceParams[index];

            if (!sentenceParams.Sentence.Contains("$Contradiction"))
                subSumary += "$Contradiction";

            if (!sentenceParams.Sentence.Contains("$Complementary"))
                subSumary += "$Complementary";

            if (index == 0)
                subSumary += sentenceParams.Sentence;
            else
                subSumary += sentenceParams.Sentence.ToLower();

            if (isContradiction && traits.First() == trait)
            {
                subSumary = subSumary.Replace("$Contradiction", sentenceParams.ContradictionPrefixSentence[Random.Range(0, sentenceParams.ContradictionPrefixSentence.Count)] + " ");
            }
            else if (traits.First() != trait)
            {
                subSumary = subSumary.Replace("$Complementary", sentenceParams.ComplementaryPrefixSentence[Random.Range(0, sentenceParams.ComplementaryPrefixSentence.Count)].ToLower() + " ");
            }

            if (traits.Last() == trait)
            {
                subSumary += ". ";
            }
            else
            {
                subSumary += " ";
            }

            subSumary = ReplaceVariables(subSumary, sentenceParams.SentenceVariables);
            subSumary = subSumary.Replace("$Contradiction", "");
            subSumary = subSumary.Replace("$Complementary", "");
            index++;
        }

        return subSumary;
    }

    private static string ReplaceVariables(string input, List<SentenceVariables> variable)
    {
        string pattern = @"\$(v\d+)";

        string result = Regex.Replace(input, pattern, match =>
        {
            string indexStr = match.Value.Substring(2);
            int index;
            if (int.TryParse(indexStr, out index) && index > 0 && index <= variable.Count)
            {
                var test1 = variable[index - 1];
                var test2 = test1.Variables;
                var test3 = test2[Random.Range(0, test2.Count)];
                return variable[index - 1].Variables[Random.Range(0, variable[index - 1].Variables.Count)];
            }
            else
            {
                return match.Value;
            }
        });

        return result;
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