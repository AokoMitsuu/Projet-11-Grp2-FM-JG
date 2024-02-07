using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookPage
{
    [SerializeField] private GameObject _page;
    [SerializeField] private Bookmark _bookmark;

    public void SetSelected(bool isSelected)
    {
        _page.SetActive(isSelected);
        _bookmark.SetSelected(isSelected);
    }
}

public class Book : MonoBehaviour
{
    [SerializeField] private List<BookPage> _pageList;

    private void Awake()
    {
        SelectPage(0);
    }

    public void SelectPage(int pageIndex)
    {
        foreach (BookPage page in _pageList)
        {
            page.SetSelected(false);
        }

        _pageList[pageIndex].SetSelected(true);
    }
}
