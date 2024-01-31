using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "traitSo", menuName = "Trait/TraiSo")]
public class TraitSo : ScriptableObject
{
    public string Name;
    public string Description;
    public List<TraitSo> ConflictTrait;
}
