using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragMechanic : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    public bool isInplace = false;
    public bool isDragable = true;
    public Vector3 startPos;
    GameObject flowText;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        flowText = GameObject.Find("Flow Text");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(CardCounter.isPlayerTurn && GetComponent<CardAttributes>().owner == CardAttributes.Owner.Player1
           || !CardCounter.isPlayerTurn && GetComponent<CardAttributes>().owner == CardAttributes.Owner.Player2)
        {
            isDragable = true;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        else
        {
            isDragable = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        

        if(!isInplace)
        {
            canvasGroup.blocksRaycasts = true;
            transform.position = startPos;
        }
        else
        {
            CardCounter.isPlayerTurn = !CardCounter.isPlayerTurn;
            if(CardCounter.isPlayerTurn)
            {
                flowText.GetComponent<Animator>().SetTrigger("PlayerTurn");
                flowText.GetComponent<Text>().text = "Blue Turn";
            }
            else
            {
                flowText.GetComponent<Animator>().SetTrigger("RivalTurn");
                flowText.GetComponent<Text>().text = "Red Turn";
            }
            
            //Impelent anime

            if(CardCounter.isPlayerTurn)
            {
                foreach(CardAttributes card in FindObjectsOfType<CardAttributes>())
                {
                    if(card.owner == CardAttributes.Owner.Player1 && !card.GetComponent<DragMechanic>().isInplace)
                    {
                        card.GetComponent<DragMechanic>().canvasGroup.blocksRaycasts = true;
                    }
                }
            }
            else
            {
                foreach (CardAttributes card in FindObjectsOfType<CardAttributes>())
                {
                    if (card.owner == CardAttributes.Owner.Player2 && !card.GetComponent<DragMechanic>().isInplace)
                    {
                        card.GetComponent<DragMechanic>().canvasGroup.blocksRaycasts = true;
                    }
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
    }

}
