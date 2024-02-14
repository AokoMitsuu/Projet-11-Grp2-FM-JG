using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortraitSpriteType
{
    Base,
    Eye,
    Eyebrows,
    Mouth,
    Special,
    Additional
}

[CreateAssetMenu(fileName = "PortraitSprite", menuName = "CustomSO/PortraitSprite")]
public class PortraitSpriteSO : ScriptableObject
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private PortraitSpriteType _type;

    public List<Sprite> Sprites => _sprites;
    public PortraitSpriteType Type => _type;
}
