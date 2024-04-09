using System;
using UnityEngine;

public class PageChanger : MonoBehaviour
{
    [SerializeField] string pageType;
    [SerializeField] float durationTime;
    
    //[SerializeField] float cooldownTime;
    
    bool onCooldown;

    BookPageHandler pageHandler = BookPageHandler.Instance;

    void OnEnable()
    {
        BookEvents.PageTurning += PageTurning;
    }

    void OnDisable()
    {
        BookEvents.PageTurning -= PageTurning;

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("FingerTip") || onCooldown) return;
        switch (pageType)
        {
            case "next":
                var indexCorrection = 1;
                if (BookPageHandler.Instance.currentPageIndex + indexCorrection == BookPageHandler.Instance.bookLeftPages.Count)
                {
                    print("on last page");
                    onCooldown = false;
                    break;
                }
                BookEvents.OnPageTurning();
                Invoke(nameof(NextPage), durationTime);
                break;
            case "prev":
                if (BookPageHandler.Instance.currentPageIndex == BookPageHandler.Instance.firstPageIndex)
                {
                    print("on first page");
                    onCooldown = false;
                    break;
                }
                BookEvents.OnPageTurning();
                Invoke(nameof(PrevPage), durationTime);
                break;
        }
    }

    void NextPage()
    {
        BookEvents.OnNextPage();
        onCooldown = false;
    }

    void PrevPage()
    {
        BookEvents.OnPrevPage();
        onCooldown = false;
    }

    void PageTurning()
    {
        onCooldown = true;
        Invoke(nameof(ResetCooldown), durationTime);
    }
    
    void ResetCooldown()
    {
        onCooldown = false;
    }
}
