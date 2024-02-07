using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatedSprite", menuName = "CustomSO/AnimatedSprite")]
public class AnimatedSpriteSO : ScriptableObject
{
    [SerializeField] private List<Sprite> _sprites;

    public List<Sprite> Sprites => _sprites;
}
