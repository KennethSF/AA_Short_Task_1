using System;
using System.Collections;
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
        var arraySize = 1000;
        int[] BubbleArray = new int[10];
        int[] SelectionArray = new int[10];
        
        int[] BArray;  
        int[] SArray;
        
        for (int i = 0; i < 10; i++)
        {
            BArray = SArray  = ArrayGenerator(arraySize);

            BubbleArray[i] = BubbleSort(BArray);
            SelectionArray[i] = SelectionSort(SArray);
            
            arraySize += 1000;
        }
        NormalizeArray(ref BubbleArray);
        NormalizeArray(ref SelectionArray);

        
        ShowGraph(BubbleArray);
        ShowGraph(SelectionArray);
    }

    private void NormalizeArray(ref int[] array){
        for(int i=0;i<10;i++){
            array[i]=( array[i]*100 ) /1000;
            if(array[i]>100)
                array[i]=100;
        }
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

    private void ShowGraph(int[] valueList){
        float graphHeigth=graphContainer.sizeDelta.y; 
        float yMaximun=100f;
        float xSize=100f;

        GameObject lasCircleGameObject=null;
        for(int i=0;i< valueList.Length;i++){
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
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
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

    private static int[] ArrayGenerator(int arraySize) 
    {
        var minNum = 0;
        var maxNum = 9999;
        int[] newArray = new int[arraySize];
        
        //float number = System.Random.Range (0f, 3f);
        System.Random randNum = new System.Random();
        for (var i = 0; i < newArray.Length; i++)
        {
            newArray[i] = randNum.Next(minNum, maxNum);
        }

        return newArray;
    }
        
        
    private static int SelectionSort(int[] arr)
    {
        var n = arr.Length;
        
        int temp, smallest;
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (var i = 0; i < n - 1; i++) {
            smallest = i;
            for (int j = i + 1; j < n; j++) {
                if (arr[j] < arr[smallest]) {
                    smallest = j;
                }
            }
            temp = arr[smallest];
            arr[smallest] = arr[i];
            arr[i] = temp;
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        return Convert.ToInt32(elapsedMs);
    }

    private static int BubbleSort(int[] arr)
    {
        int temp;
        //Sorting method
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int j = 0; j <= arr.Length - 2; j++) {
            for (int i = 0; i <= arr.Length - 2; i++) {
                if (arr[i] > arr[i + 1]) {
                    temp = arr[i + 1];
                    arr[i + 1] = arr[i];
                    arr[i] = temp;
                }
            }
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        return Convert.ToInt32(elapsedMs);
    }
}
