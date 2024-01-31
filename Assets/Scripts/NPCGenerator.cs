using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private NPC[] _npcs = new NPC[6];
    [SerializeField] private int _traitsCount = 3;
    [SerializeField] private int _seed;
    [SerializeField] private bool _randomSeed;

    [Space(10), Header("PROFILE")]
    [SerializeField] private RectTransform _profileContainer;
    [SerializeField] private GameObject _profilePrefab;

    [Space(10), Header("TRAITS")]
    [SerializeField] private List<TraitSo> _possibleTraits;
    [SerializeField] private RectTransform _traitContainer;
    [SerializeField] private GameObject _traitPrefab;


    private void Start()
    {
        if (!_randomSeed)
            Random.InitState(_seed);

        //_profileContainer.sizeDelta = new Vector2(50 + ((_npcs.Length / 2) * 800), _profileContainer.sizeDelta.y);
        //_traitContainer.sizeDelta = new Vector2(_profileContainer.sizeDelta.x, 50 + (_possibleTraits.Count * 85));

        GenerateTrait();
        GenerateNPCs();
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

    private void GenerateTrait()
    {
        // clear container
        foreach (Transform child in _traitContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var trait in _possibleTraits)
        {
            TraitController traitController = Instantiate(_traitPrefab, _traitContainer).GetComponent<TraitController>();
            traitController.Init(trait);
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