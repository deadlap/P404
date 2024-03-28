using System.Collections.Generic;
using UnityEngine;

public class GenerateIcon : MonoBehaviour
{
    [SerializeField] List<GameObject> iconList;
    void Awake()
    {
        Generate();
    }
    
    void Generate()
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            //Undskyld olga
            GameObject page = GameObject.Find($"Left Page ({i + 1})");
            GameObject icon = Instantiate(iconList[i], page.transform, false);
        }
    }
}
