using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> bookLeftPages;
    [SerializeField] List<GameObject> bookRightPages;

    int firstPageIndex = 0;

    int currentPageIndex;

    void OnEnable()
    {
        BookEvents.NextPage += NextPage;
        BookEvents.PrevPage += PrevPage;
    }

    void OnDisable()
    {
        BookEvents.NextPage -= NextPage;
        BookEvents.PrevPage -= PrevPage;
    }

    void Start()
    {
        for (int i = 0; i < bookLeftPages.Count; i++)
        {
            bookLeftPages[i].SetActive(false);
            bookRightPages[i].SetActive(false);
        }
        currentPageIndex = firstPageIndex;
        bookLeftPages[firstPageIndex].SetActive(true);
        bookRightPages[firstPageIndex].SetActive(true);
    }
    

    void NextPage()
    {
        print("NEXT PAGE");
        if(currentPageIndex + 1 == bookLeftPages.Count) return;
        bookLeftPages[currentPageIndex].SetActive(false);
        bookRightPages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        bookLeftPages[currentPageIndex].SetActive(true);
        bookRightPages[currentPageIndex].SetActive(true);
    }
    
    void PrevPage()
    {
        print("PREV PAGE");
        if (currentPageIndex == firstPageIndex) return;
        bookLeftPages[currentPageIndex].SetActive(false);
        bookRightPages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        bookLeftPages[currentPageIndex].SetActive(true);
        bookRightPages[currentPageIndex].SetActive(true);
    }
}
