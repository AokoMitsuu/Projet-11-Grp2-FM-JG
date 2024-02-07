using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitsContainer : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private List<GameObject> _pages;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    [Header("Params")]
    [SerializeField] private int _traitsPerPages;

    private int _currentPageIndex = 0;

    private void Awake()
    {
        foreach (GameObject page in _pages)
        {
            page.SetActive(false);
        }
        _pages[_currentPageIndex].SetActive(true);
        UpdateButton();
    }

    public void AddUI(GameObject UIObject)
    {
        GameObject page = GetFirstAvailablePage();
        UIObject.transform.SetParent(page.transform, false);
    }

    public GameObject GetFirstAvailablePage()
    {
        foreach (GameObject page in _pages)
        {
            if (page.transform.childCount < _traitsPerPages) 
                return page;
        }

        return null;
    }

    public void Clear()
    {
        foreach (GameObject page in _pages)
        {
            for (int i = page.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(page.transform.GetChild(i).gameObject);
            }
        }
    }

    private void UpdateButton()
    {
        _leftButton.interactable = _currentPageIndex > 0;
        _rightButton.interactable = _currentPageIndex < _pages.Count - 1;
    }

    public void GoToNextPage()
    {
        _pages[_currentPageIndex].SetActive(false);
        _currentPageIndex++;
        _pages[_currentPageIndex].SetActive(true);
        UpdateButton();
    }

    public void GoToPreviousPage()
    {
        _pages[_currentPageIndex].SetActive(false);
        _currentPageIndex--;
        _pages[_currentPageIndex].SetActive(true);
        UpdateButton();
    }
}
