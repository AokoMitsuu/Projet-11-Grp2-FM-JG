using System.Collections.Generic;
using System.Linq;

public class TraitsManager
{
    private List<TraitSo> _availableTraits;

    public TraitsManager(List<TraitSo> availableTraits)
    {
        _availableTraits = availableTraits;
    }

    public TraitSo GetRandomTraitAvoidingGroupConflicts(List<TraitSo> selectedTraits)
    {
        List<TraitSo> conflictTraits = new();

        foreach(var selected in selectedTraits)
        {
            conflictTraits.AddRange(selected.ConflictTrait);
            conflictTraits.Add(selected);
        }

        List<TraitSo> potentialTraits = _availableTraits.Except(conflictTraits).ToList();

        if (potentialTraits.Count > 0)
        {
            return potentialTraits[UnityEngine.Random.Range(0, potentialTraits.Count)];
        }

        return null;
    }
}