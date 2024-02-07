using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private GameObject _traitsContainer;
    [SerializeField] private GameObject _portraitContainer;
    [SerializeField] private TMP_Text _sumaryText;
    [SerializeField] private TMP_Text _traitCountText;

    [SerializeField] private GameObject _traitPrefab;
    [SerializeField] private GameObject _portraitBasePrefab;

    private NPC _npc;

    public NPC Npc => _npc;

    public void Init(NPC npc)
    {
        _npc = npc;
        _npc.OnTraitsChange += UpdateUI;

        UpdateUI();
    }

    public void AddTrait(TraitSo trait)
    {
        _npc.AddTrait(trait);
    }

    private void UpdateUI()
    {
        foreach (Transform child in _traitsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var trait in _npc.Traits)
        {
            TraitController traitController = Instantiate(_traitPrefab, _traitsContainer.transform).GetComponent<TraitController>();
            traitController.Init(trait, _npc);
        }

        _traitCountText.text = _npc.Traits.Count.ToString() + "/" + NPC.MAX_TRAITS_COUNT;

        _sumaryText.text = _npc.Name + " : " + _npc.Summary;

        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        foreach (Transform child in _portraitContainer.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject portraitBase = Instantiate(_portraitBasePrefab, _portraitContainer.transform);
        portraitBase.GetComponent<SpriteAnimator>().SetSprite(Npc.BaseSprite);
    }
}
