using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    public List<TraitSo> PossibleTraits;
    public NPC[] npcs = new NPC[6];
    public int traitsCount = 3;

    void Start()
    {
        GenerateNPCs();
    }

    void GenerateNPCs()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            string name = $"NPC {i + 1}";
            List<TraitSo> traits = GetRandomTraits();
            npcs[i] = new NPC(name, traits);
            Debug.Log(npcs[i].Summary);
        }
    }

    List<TraitSo> GetRandomTraits()
    {
        TraitsManager traitsManager = new TraitsManager(PossibleTraits);
        List<TraitSo> selectedTraits = new List<TraitSo>();

        while (selectedTraits.Count < traitsCount)
        {
            TraitSo potentialTrait = traitsManager.GetRandomTraitAvoidingGroupConflicts(selectedTraits);
            if (potentialTrait)
            {
                selectedTraits.Add(potentialTrait);
            }
            else
            {
                break;
            }
        }

        return selectedTraits;
    }
}