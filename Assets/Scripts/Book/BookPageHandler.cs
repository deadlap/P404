using System.Collections.Generic;
using UnityEngine;

public class BookPageHandler : MonoBehaviour
{
    public static BookPageHandler Instance; 
    [SerializeField] public List<GameObject> bookLeftPages;
    [SerializeField] List<GameObject> bookRightPages;

    public int firstPageIndex = 0;

    public int currentPageIndex;


    void Awake()
    {
        Instance = this;
    }

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
        var indexCorrection = 1;
        if (currentPageIndex + indexCorrection == bookLeftPages.Count) return;
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
