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
    public const string COMP = "$comp";
    public const string CONT = "$cont";

    public Action OnTraitsChange;

    public string Name => _name;
    [SerializeField] private string _name;

    public List<TraitSo> Traits => _traits;
    [SerializeField] private List<TraitSo> _traits;

    public string Summary => _summary;
    [SerializeField] private string _summary;

    public PortraitSpriteSO BaseSprite => _basePortrait;
    private PortraitSpriteSO _basePortrait;
    
    public NPC(string name, List<TraitSo> traits, PortraitSpriteSO basePortrait)
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

            if (!sentenceParams.Sentence.Contains(CONT))
                subSumary += CONT + "1 ";

            if (!sentenceParams.Sentence.Contains(COMP))
                subSumary += COMP + "1 ";

            if (index == 0)
                subSumary += sentenceParams.Sentence;
            else
                subSumary += sentenceParams.Sentence.ToLower();

            if (isContradiction && traits.First() == trait)
            {
                subSumary = ReplaceCont(subSumary, sentenceParams.ContradictionPrefixSentence);
            }
            else if (traits.First() != trait)
            {
                subSumary = ReplaceComp(subSumary, sentenceParams.ComplementaryPrefixSentence);
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

            for (int i = 0; i < 10; i++)
            {
                subSumary = subSumary.Replace(CONT + i + " ", "");
                subSumary = subSumary.Replace(COMP + i + " ", "");
            }

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

    private static string ReplaceCont(string input, List<Sentence> sentences)
    {
        string pattern = @"\$(cont\d+)\s?";
        var matches = Regex.Matches(input, pattern).Cast<Match>().ToList();

        // Sélection aléatoire d'un seul $comp pour le remplacer
        if (matches.Count > 0)
        {
            var selectedMatch = matches[Random.Range(0, matches.Count)];

            input = Regex.Replace(input, Regex.Escape(selectedMatch.Value), match =>
            {
                // Ajustement pour extraire correctement le numéro après "comp"
                string indexStr = match.Value.Substring(5).Trim(); // Supprime "comp" et espace potentiel
                int index;
                if (int.TryParse(indexStr, out index) && index > 0 && index <= sentences.Count && sentences.Count > 0 && sentences[index - 1].Sentences.Count > 0)
                {
                    var selectedSentence = sentences[index - 1].Sentences[Random.Range(0, sentences[index - 1].Sentences.Count)];
                    return selectedSentence + (match.Value.EndsWith(" ") ? " " : ""); // Ajoute un espace si l'original en avait un
                }
                else
                {
                    return match.Value.Trim();
                }
            }, RegexOptions.None);

            // Suppression des autres $comp, y compris l'espace potentiel après
            input = Regex.Replace(input, pattern, "");
        }

        return input;
    }

    private static string ReplaceComp(string input, List<Sentence> sentences)
    {
        string pattern = @"\$(comp\d+)\s?";
        var matches = Regex.Matches(input, pattern).Cast<Match>().ToList();

        // Sélection aléatoire d'un seul $comp pour le remplacer
        if (matches.Count > 0)
        {
            var selectedMatch = matches[Random.Range(0, matches.Count)];

            input = Regex.Replace(input, Regex.Escape(selectedMatch.Value), match =>
            {
                // Ajustement pour extraire correctement le numéro après "comp"
                string indexStr = match.Value.Substring(5).Trim(); // Supprime "comp" et espace potentiel
                int index;
                if (int.TryParse(indexStr, out index) && index > 0 && index <= sentences.Count && sentences.Count > 0 && sentences[index - 1].Sentences.Count > 0)
                {
                    var selectedSentence = sentences[index - 1].Sentences[Random.Range(0, sentences[index - 1].Sentences.Count)];
                    return selectedSentence + (match.Value.EndsWith(" ") ? " " : ""); // Ajoute un espace si l'original en avait un
                }
                else
                {
                    return match.Value.Trim();
                }
            }, RegexOptions.None);

            // Suppression des autres $comp, y compris l'espace potentiel après
            input = Regex.Replace(input, pattern, "");
        }

        return input;
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