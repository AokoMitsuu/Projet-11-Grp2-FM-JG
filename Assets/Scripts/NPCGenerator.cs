using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private List<TraitSo> _possibleTraits;
    [SerializeField] private NPC[] _npcs = new NPC[6];
    [SerializeField] private int _traitsCount = 3;
    [SerializeField] private int _seed;
    [SerializeField] private bool _randomSeed;

    [SerializeField] private Transform _profileContainer;
    [SerializeField] private GameObject _profilePrefab;



    private void Start()
    {
        if (!_randomSeed)
            Random.InitState(_seed);

        GenerateNPCs();
    }

    private void Update()
    {
        foreach(var npc in _npcs)
        {
            npc.Update();
        }
    }

    private void GenerateNPCs()
    {
        for (int i = 0; i < _npcs.Length; i++)
        {
            string name = $"NPC {i + 1}";
            List<TraitSo> traits = GetRandomTraits();
            NPC npc = new NPC(name, traits);
            var profile = Instantiate(_profilePrefab, _profileContainer);
            profile.GetComponent<ProfileController>().Init(npc);
            _npcs[i] = npc;
        }
    }

    private List<TraitSo> GetRandomTraits()
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