using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake(){
        graphContainer=transform.Find("graphContainer").GetComponent<RectTransform>();
        CreateCircle(new Vector2(100,100));
    }

    private void CreateCircle(Vector2 anchoredPosition){
        GameObject gameObject= new GameObject("circle",typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite=circleSprite;
        RectTransform rectTransform= gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition=anchoredPosition;
        rectTransform.sizeDelta= new Vector2(11,11);
        rectTransform.anchorMin= new Vector2(0,0);
        rectTransform.anchorMax= new Vector2(0,0);
    }
}

