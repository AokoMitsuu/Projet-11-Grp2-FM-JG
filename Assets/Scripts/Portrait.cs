using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    [Header("Portrait Image Slots")]
    // Mendatory
    [SerializeField] private SpriteAnimator _baseImage;
    [SerializeField] private SpriteAnimator _eyesImage;
    [SerializeField] private SpriteAnimator _eyebrowsImage;
    [SerializeField] private SpriteAnimator _mouthImage;

    // Optional
    [SerializeField] private SpriteAnimator[] _specials;
    [SerializeField] private GameObject _additionalImagesPrefab;
    private List<SpriteAnimator> _additionalImages = new();

    [Header("Portrait Default Sprites")]
    [SerializeField] private PortraitSpriteSO[] _defaultSprites;

    public void AddPortraitSprite(PortraitSpriteSO portraitSprite)
    {
        switch (portraitSprite.Type)
        {
            case PortraitSpriteType.Base:
                _baseImage.Sprite = portraitSprite;
                break;

            case PortraitSpriteType.Eye:
                _eyesImage.Sprite = portraitSprite;
                break;

            case PortraitSpriteType.Eyebrows:
                _eyebrowsImage.Sprite = portraitSprite;
                break;

            case PortraitSpriteType.Mouth:
                _mouthImage.Sprite = portraitSprite;
                break;

            case PortraitSpriteType.Special:
                for (int i = 0; i < _specials.Length; i++)
                {
                    if (_specials[i].Sprite == null)
                    {
                        _specials[i].Sprite = portraitSprite;
                        break;
                    }
                }
                break;

            case PortraitSpriteType.Additional:
                SpriteAnimator addSprite = Instantiate(_additionalImagesPrefab, transform).GetComponent<SpriteAnimator>();
                addSprite.Sprite = portraitSprite;
                _additionalImages.Add(addSprite);
                break;

            default:
                break;
        }
    }

    public void Clear()
    {
        // === Remove


        for (int i = 0; i < _specials.Length; i++)
        {
            _specials[i].Sprite = null;
        }

        foreach (SpriteAnimator img in _additionalImages)
        {
            Destroy(img.gameObject);
        }
        _additionalImages.Clear();

        // === Set defaults

        foreach (PortraitSpriteSO sprites in _defaultSprites)
        {
            AddPortraitSprite(sprites);
        }
    }

}
