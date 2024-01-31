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

    public List<TraitSo> ContradictionTrait => _contradictionTrait;
    [SerializeField] private List<TraitSo> _contradictionTrait;

    public List<TraitSo> ComplementaryTrait => _complementaryTrait;
    [SerializeField] private List<TraitSo> _complementaryTrait;

    public List<string> PrefixSentece => _prefixSentence;
    [SerializeField] private List<string> _prefixSentence;

    public List<string> SufixSentece => _suffixSentence;
    [SerializeField] private List<string> _suffixSentence;
}

public enum ETag
{
    Positif,
    Negatif,
    Age
}