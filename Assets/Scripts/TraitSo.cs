using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "traitSo", menuName = "Trait/TraiSo")]
public class TraitSo : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    public List<TraitSo> ConflictTrait => _conflictTrait;
    [SerializeField] private List<TraitSo> _conflictTrait;

    public List<SentenceParams> SentenceParams => _sentenceParams;
    [SerializeField] private List<SentenceParams> _sentenceParams;

    public ETag Tag => _tag;
    [SerializeField] private ETag _tag;

    public List<PortraitSpriteSO> PortraitSprites => _portraitSprites;
    [SerializeField] private List<PortraitSpriteSO> _portraitSprites;

    public override string ToString()
    {
        string summary = string.Empty;

        summary += $"Nom : {_name}\n";

        if(_conflictTrait.Count > 0)
        {
            summary += $"Conflict : \n";

            foreach (var trait in _conflictTrait)
            {
                summary += $" - {trait.Name}\n";
            }
        }

        summary += $"Phrases : ";

        foreach (var sentence in _sentenceParams)
        {
            summary += $"\n\n   {sentence.Sentence}\n";

            if(sentence.ComplementaryPrefixSentence.Count > 0)
            {
                summary += $"\n       Connecteur logique positif : \n";

                for (int i = 0; i < sentence.ComplementaryPrefixSentence.Count; i++)
                {
                    summary += $"        - {i + 1}\n";

                    foreach (var complementary in sentence.ComplementaryPrefixSentence[i].Sentences)
                    {
                        summary += $"           - {complementary}\n";
                    }
                }
            }

            if (sentence.ContradictionPrefixSentence.Count > 0)
            {
                summary += $"\n       Connecteur logique negatif : \n";

                for (int i = 0; i < sentence.ContradictionPrefixSentence.Count; i++)
                {
                    summary += $"        - {i + 1}\n";

                    foreach (var contradiction in sentence.ContradictionPrefixSentence[i].Sentences)
                    {
                        summary += $"           - {contradiction}\n";
                    }
                }
            }

            if (sentence.SentenceVariables.Count > 0)
            {
                summary += $"\n       Variable : \n";

                for (int i = 0; i < sentence.SentenceVariables.Count; i++)
                {
                    summary += $"        - {i + 1}\n";

                    foreach (var variable in sentence.SentenceVariables[i].Variables)
                    {
                        summary += $"           - {variable}\n";
                    }
                }
            }
        }

        return summary;
    }
}

public enum ETag
{
    Positif,
    Negatif,
    Other,
    PassionPositif, 
    PassionNegatif
}

[Serializable]
public class SentenceParams
{
    public string Sentence;
    public List<Sentence> ComplementaryPrefixSentence;
    public List<Sentence> ContradictionPrefixSentence;
    public List<SentenceVariables> SentenceVariables;
}

[Serializable]
public class SentenceVariables
{
    public List<string> Variables;
}

[Serializable]
public class Sentence
{
    public List<string> Sentences;
}