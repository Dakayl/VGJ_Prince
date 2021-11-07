using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CardPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private Image background;
    public delegate void SwipeLeftHandler();
    public event SwipeLeftHandler SwipingLeft;
    public delegate void SwipeRightHandler();
    public event SwipeRightHandler SwipingRight;
    public delegate void NotSwipingHandler();
    public event NotSwipingHandler NotSwiping;
    public delegate void ExitHandler();
    public event ExitHandler OnRelease;
    public delegate void AskingForNewCardHandler();
    public event AskingForNewCardHandler OnNewCard;
    private Quaternion startCardRotation;
    private Vector3 startCardPosition;
    private Vector3 startMousePosition;
    private bool isDragged = false;
    private bool canBeDragged = true;
    private int maxDistance = 200;
    private int maxAngle = 50;
    private float cardFadeDuration = 0.7f;
    private Color32 descriptionColor;
    private Color32 titleColor;
    private Color32 backgroundColor;
    private Color32 empty = new Color32(0,0,0,0);


    public void OnPointerDown(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if(canBeDragged && !isDragged) {
            startMousePosition = Input.mousePosition;
            isDragged = true;
        }
        
    }

     public void OnPointerUp(PointerEventData eventData)
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        if(canBeDragged && isDragged) {
            if(OnRelease != null) OnRelease();
            isDragged = false;
        }

    }

    void Start()
    {
        startCardPosition = transform.position;
        startCardRotation = transform.rotation;
       
        StartCoroutine(this.Instantiate());
    }
    
    public IEnumerator Instantiate(){
        while( descriptionTMP == null) yield return null;;
        while( titleTMP == null) yield return null;;
        while( background == null) yield return null;;
        backgroundColor = background.color;
        titleColor = titleTMP.color;
        descriptionColor = descriptionTMP.color;
        yield break;

    }

    void Update()
    {
        if (isDragged)
        {
            float x = Input.mousePosition.x - startMousePosition.x;
            float angle = Mathf.Lerp(0, maxAngle, Mathf.Abs(x)/maxDistance);
            bool isMovingLeft = (x < 0);
            if(x > maxDistance) {
                x = maxDistance;
                if(SwipingRight != null) SwipingRight();
            } else {
                if(x <- maxDistance) {
                    x =- maxDistance;
                    if(SwipingLeft != null) SwipingLeft();
                } else {
                   if(NotSwiping != null) NotSwiping();
                }
            }            
            if(!isMovingLeft) {
                angle = - angle;
            }
            Vector3 distance = new Vector3(x, 0, 0);
            transform.position = startCardPosition + distance;
            transform.rotation =  startCardRotation * Quaternion.Euler(0, 0, angle);
        }    
    }

    public void RestartPosition() {
        transform.position = startCardPosition;
        transform.rotation = startCardRotation;
    }


    public void Die(bool isLeft){
        canBeDragged = false;
        isDragged = false;
        Vector3 distance = new Vector3(maxDistance, 0, 0);
        if(isLeft) {
            transform.position = startCardPosition - distance;
            transform.rotation =  startCardRotation * Quaternion.Euler(0, 0, maxAngle);
        } else {
            transform.position = startCardPosition + distance;
            transform.rotation =  startCardRotation * Quaternion.Euler(0, 0, -maxAngle);   
        }
          StartCoroutine(this.CardFade(isLeft));
       
    }

     public IEnumerator CardFade(bool isLeft)
    {
        float currentTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition;
 
        if(isLeft){
            endPosition = startPosition - new Vector3(400, 250, 0);
        } else {
            endPosition = startPosition - new Vector3(-400, 250, 0);
        }
        while (currentTime < cardFadeDuration)
        {
            currentTime += Time.deltaTime; 
            float ratio = currentTime / cardFadeDuration;           
            float angle = Mathf.Lerp(maxAngle, 120, ratio);
            if(!isLeft) {
                angle = - angle;
            }
            background.color = Color32.Lerp(backgroundColor, empty, ratio);
            titleTMP.color = Color32.Lerp(titleColor, empty ,ratio);
            descriptionTMP.color = Color32.Lerp(descriptionColor, empty , ratio);
            transform.position = Vector3.Lerp(startPosition, endPosition , ratio);
            transform.rotation = startCardRotation * Quaternion.Euler(0, 0, angle);
            yield return null;
        }
        titleTMP.enabled = false;
        descriptionTMP.enabled = false;
        background.enabled = false;
        StartCoroutine(this.ResetWhole());
        yield break;
    }

    public IEnumerator ResetWhole(){
        yield return new WaitForSeconds(0.5f);
        this.RestartPosition();
        if(OnNewCard != null) OnNewCard();
        background.color = backgroundColor;
        titleTMP.color = titleColor;
        descriptionTMP.color = descriptionColor;
        canBeDragged = true;
        isDragged = false;
        titleTMP.enabled = true;
        descriptionTMP.enabled = true;
        background.enabled = true;
        
    }


    public string title
    {
        get { return titleTMP.text; }
        set {titleTMP.text = value; }
    }

     public string description
    {
        get { return descriptionTMP.text; }
        set {descriptionTMP.text = value; }
    }
}
