using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTakeScreenshot : MonoBehaviour
{
    [SerializeField]
    private string path;
    [SerializeField]
    private string shotName;
    [SerializeField, Range(1, 5)]
    private int size = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            string newPath = path + "screenshot " + shotName + ".png";
            //newPath += System.Guid.NewGuid().ToString() + ".png";

            ScreenCapture.CaptureScreenshot(newPath, size);
        }
    }
}
