using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class TraitsManager
{
    public List<TraitSo> AvailableTraits;

    public TraitsManager(List<TraitSo> availableTraits)
    {
        AvailableTraits = availableTraits;
    }

    public TraitSo GetRandomTraitAvoidingGroupConflicts(List<TraitSo> selectedTraits)
    {
        List<TraitSo> conflictTraits = new();

        foreach(var selected in selectedTraits)
        {
            conflictTraits.AddRange(selected.ConflictTrait);
            conflictTraits.Add(selected);
        }

        List<TraitSo> potentialTraits = AvailableTraits.Except(conflictTraits).ToList();

        if (potentialTraits.Count > 0)
        {
            return potentialTraits[UnityEngine.Random.Range(0, potentialTraits.Count)];
        }

        return null;
    }
}