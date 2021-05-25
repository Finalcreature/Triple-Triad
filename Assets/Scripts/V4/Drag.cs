using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector3 startPos;
    public Card.ActualCard card;
    GameObject preview;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        preview = GameObject.Find("Preview");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            canvasGroup.blocksRaycasts = true;
            transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;

       
        for (int i = 0; i < preview.transform.childCount; i++)
        {
            preview.transform.GetChild(i).GetComponent<Text>().text = card.GetNums(i).ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
