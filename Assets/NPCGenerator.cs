using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private List<TraitSo> _possibleTraits;
    [SerializeField] private NPC[] _npcs = new NPC[6];
    [SerializeField] private int _traitsCount = 3;

    void Start()
    {
        GenerateNPCs();
    }

    void GenerateNPCs()
    {
        for (int i = 0; i < _npcs.Length; i++)
        {
            string name = $"NPC {i + 1}";
            List<TraitSo> traits = GetRandomTraits();
            _npcs[i] = new NPC(name, traits);
            Debug.Log(_npcs[i]._summary);
        }
    }

    List<TraitSo> GetRandomTraits()
    {
        TraitsManager traitsManager = new TraitsManager(_possibleTraits);
        List<TraitSo> selectedTraits = new List<TraitSo>();

        while (selectedTraits.Count < _traitsCount)
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