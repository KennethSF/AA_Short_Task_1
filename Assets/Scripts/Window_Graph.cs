﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;

    private void Awake(){
        graphContainer=transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        List<int>valueList=new List<int>(){10,45,84,46,75,32,41,96,74,66};
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) { 
        GameObject gameObject= new GameObject("circle",typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite=circleSprite;
        RectTransform rectTransform= gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition=anchoredPosition;
        rectTransform.sizeDelta= new Vector2(11,11);
        rectTransform.anchorMin= new Vector2(0,0);
        rectTransform.anchorMax= new Vector2(0,0);
        
        return gameObject;
    }

    private void ShowGraph(List<int> valueList){
        float graphHeigth=graphContainer.sizeDelta.y; 
        float yMaximun=100f;
        float xSize=100f;

        GameObject lasCircleGameObject=null;
        for(int i=0;i< valueList.Count;i++){
            float xPosition=xSize + i*xSize-65;
            float yPosition=(valueList[i]/yMaximun)*graphHeigth;
            GameObject circleGameObject=CreateCircle(new Vector2(xPosition,yPosition));
            if(lasCircleGameObject!=null){
                CreateDotConnection(lasCircleGameObject.GetComponent<RectTransform>().anchoredPosition,circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lasCircleGameObject=circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, 20f);
            labelX.GetComponent<Text>().text = i.ToString();
        }

    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB){
        GameObject gameObject = new GameObject("dotConnection",typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color= new Color(1,1,1,.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir= (dotPositionB-dotPositionA).normalized;
        float distance =Vector2.Distance(dotPositionA,dotPositionB);
        rectTransform.anchorMin= new Vector2(0,0);
        rectTransform.anchorMax= new Vector2(0,0);
        rectTransform.sizeDelta= new Vector2(distance,3f);
        rectTransform.anchoredPosition=dotPositionA + dir *distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
