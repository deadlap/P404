using System;
using UnityEngine;
using UnityEngine.UI;

public class PageChanger : MonoBehaviour
{
    [SerializeField] string pageType;
    [SerializeField] float pageFlipDuration;
    [SerializeField] float cooldownTime;

    [SerializeField] Image nextArrow;
    [SerializeField] Image prevArrow;
    
    bool onCooldown;
    int indexCorrection = 1;


    void OnEnable()
    {
        BookEvents.PageTurning += PageTurning;
    }

    void OnDisable()
    {
        BookEvents.PageTurning -= PageTurning;
    }

    void Update()
    {
        if (BookPageHandler.Instance.currentPageIndex == BookPageHandler.Instance.firstPageIndex)
        {
            prevArrow.enabled = false;
            nextArrow.enabled = true;
        }

        if (BookPageHandler.Instance.currentPageIndex + indexCorrection == BookPageHandler.Instance.bookLeftPages.Count)
        {
            prevArrow.enabled = true;
            nextArrow.enabled = false;
        }
        else if(BookPageHandler.Instance.currentPageIndex != BookPageHandler.Instance.firstPageIndex && BookPageHandler.Instance.currentPageIndex != BookPageHandler.Instance.bookLeftPages.Count)
        {
            prevArrow.enabled = true;
            nextArrow.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("FingerTip") || onCooldown) return;
        switch (pageType)
        {
            case "next":
                if (BookPageHandler.Instance.currentPageIndex + indexCorrection == BookPageHandler.Instance.bookLeftPages.Count)
                {
                    print("on last page");
                    onCooldown = false;
                    break;
                }
                BookEvents.OnPageTurning();
                BookEvents.OnSpellChosen("");
                Invoke(nameof(NextPage), pageFlipDuration);
                break;
            case "prev":
                if (BookPageHandler.Instance.currentPageIndex == BookPageHandler.Instance.firstPageIndex)
                {
                    print("on first page");
                    onCooldown = false;
                    break;
                }
                BookEvents.OnPageTurning();
                BookEvents.OnSpellChosen("");
                Invoke(nameof(PrevPage), pageFlipDuration);
                break;
        }
    }

    void NextPage()
    {
        BookEvents.OnNextPage();
    }

    void PrevPage()
    {
        BookEvents.OnPrevPage();
    }

    void PageTurning()
    {
        onCooldown = true;
        Invoke(nameof(ResetCooldown), cooldownTime);
    }
    
    void ResetCooldown()
    {
        onCooldown = false;
    }
}
