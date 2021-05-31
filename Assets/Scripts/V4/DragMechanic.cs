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
    LevelManager levelManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        flowText = GameObject.Find("Flow Text");
        
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;   //this will allow the mouse ray to hit the slot behind it
    }

    //Move the card if it's their turn
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
        //Check if the card was placed in a valid slot 
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
                flowText.GetComponent<Image>().sprite = levelManager.flowText[0];
            }
            else
            {
                flowText.GetComponent<Animator>().SetTrigger("RivalTurn");
                flowText.GetComponent<Image>().sprite = levelManager.flowText[1];
            }
            StartCoroutine(DisableCursor(2f));


        }
    }

    IEnumerator DisableCursor(float seconds)
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(seconds);
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
    }

}
