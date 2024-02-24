using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0, 2f), SerializeField] private float scaleAmount = 1.05f;
    
    private Vector3 startPos;
    private Vector3 startScale;

    void Start()
    {
        this.startPos = transform.position;
        this.startScale = transform.localScale;
    }

    private IEnumerator animateCards(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < this.moveTime)
        {
            // Increment timer
            elapsedTime += Time.deltaTime;

            // Assess move direction
            if(startingAnimation)
            {
                endPosition = this.startPos + new Vector3(0f, this.verticalMoveAmount, 0f);
                endScale = this.startScale * this.scaleAmount;
            } else
            {
                endPosition = this.startPos;
                endScale = this.startScale;
            }

            // Calculate the step
            Vector3 lerpedPosition = Vector3.Lerp(transform.position, endPosition, (elapsedTime / moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            // Apply the changes
            transform.position = lerpedPosition;
            transform.localScale = lerpedScale;

            // Animation stuff - await next frame kind of
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Select the card
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Deselect the card
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(this.animateCards(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(this.animateCards(false));   
    }

    public void onClick() {
        Debug.Log("clicked");
    }
}

