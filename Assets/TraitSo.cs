using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "traitSo", menuName = "Trait/TraiSo")]
public class TraitSo : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    public string Description => _description;
    [SerializeField] private string _description;

    public List<TraitSo> ConflictTrait => _conflictTrait;
    [SerializeField] private List<TraitSo> _conflictTrait;
}
