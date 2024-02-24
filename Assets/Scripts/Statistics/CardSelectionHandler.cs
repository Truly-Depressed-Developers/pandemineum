using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float _verticalMoveAmount = 30f;
    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2f), SerializeField] private float _scaleAmount = 1.05f;
    
    private Vector3 _startPos;
    private Vector3 _startScale;


    void Start()
    {
        this._startPos = transform.position;
        this._startScale = transform.localScale;
    }

    private IEnumerator animateCards(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < this._moveTime)
        {
            // Increment timer
            elapsedTime += Time.deltaTime;

            // Assess move direction
            if(startingAnimation)
            {
                endPosition = this._startPos + new Vector3(0f, this._verticalMoveAmount, 0f);
                endScale = this._startScale * this._scaleAmount;
            } else
            {
                endPosition = this._startPos;
                endScale = this._startScale;
            }

            // Calculate the step
            Vector3 lerpedPosition = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

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
}

