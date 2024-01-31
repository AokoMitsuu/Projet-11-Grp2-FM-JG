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

    public List<string> PrefixSentece => _prefixSentence;
    [SerializeField] private List<string> _prefixSentence;

    public List<string> ComplementaryPrefixSentece => _complementaryPrefixSentece;
    [SerializeField] private List<string> _complementaryPrefixSentece;

    public List<string> ContradictionPrefixSentece => _contradictionPrefixSentece;
    [SerializeField] private List<string> _contradictionPrefixSentece;

    public List<string> SufixSentece => _suffixSentence;
    [SerializeField] private List<string> _suffixSentence;

    public ETag Tag => _tag;
    [SerializeField] private ETag _tag;
}

public enum ETag
{
    Positif,
    Negatif,
    Age
}