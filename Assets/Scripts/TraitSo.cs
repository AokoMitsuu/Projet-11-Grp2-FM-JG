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