using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private NPC[] _npcs = new NPC[4];
    [SerializeField] private int _traitsCount = 3;
    [SerializeField] private int _seed;
    [SerializeField] private bool _randomSeed;

    [Space(10), Header("PROFILE")]
    [SerializeField] private RectTransform _profileContainer;
    [SerializeField] private GameObject _profilePrefab;

    [Space(10), Header("TRAITS")]
    [SerializeField] private List<TraitSo> _possibleTraits;
    [SerializeField] private TraitsContainer _traitContainer;
    [SerializeField] private GameObject _traitPrefab;

    [Space(10), Header("PORTRAIT")]
    [SerializeField] private List<PortraitSpriteSO> _basePortraits;


    public int CurrentSeed => _seed;

    private void Awake()
    {
        if (_randomSeed) _seed = Random.Range(0, 10000000);

        Random.InitState(_seed);
    }

    private void Start()
    {
        GenerateTrait();
        GenerateNPCs();
    }

    public void Regenerate(int seed)
    {
        _seed = seed;
        Random.InitState(_seed);

        GenerateTrait();
        GenerateNPCs();
    }

    private void GenerateNPCs()
    {
        foreach (Transform child in _profileContainer.transform)
        {
            Destroy(child.gameObject);
        }

        List<PortraitSpriteSO> portraits = new(_basePortraits);

        for (int i = 0; i < _npcs.Length; i++)
        {
            string name = $"NPC {i + 1}";
            List<TraitSo> traits = GetRandomTraits();

            PortraitSpriteSO sprite = portraits[Random.Range(0, portraits.Count)];
            portraits.Remove(sprite);

            NPC npc = new NPC(name, traits, sprite);
            var profile = Instantiate(_profilePrefab, _profileContainer);
            profile.GetComponent<ProfileController>().Init(npc);
            _npcs[i] = npc;
        }
    }

    private void GenerateTrait()
    {
        _traitContainer.Clear();

        foreach (var trait in _possibleTraits)
        {
            TraitController traitController = Instantiate(_traitPrefab).GetComponent<TraitController>();
            _traitContainer.AddUI(traitController.gameObject);
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