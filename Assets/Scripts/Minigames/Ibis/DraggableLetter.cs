using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableLetter : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool correct = false;
    private Vector3 originalPosition;
    private Transform originalParent;

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the weight's position to follow the mouse
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D closestPan = null;
        float closestDistance = float.MaxValue;

        // Get all colliders tagged as "Pan"
        foreach (Collider2D collider in FindObjectsOfType<Collider2D>())
        {
            if (collider.CompareTag("Slot"))
            {
                closestPan = collider;
            }
        }

        // Reparent the weight if dropped on a pan
        if (closestPan != null && correct)
        {
            Transform panTransform = closestPan.transform;
            transform.SetParent(panTransform); // Reparent to the pan
            transform.position = panTransform.position; // Snap to the pan
        }
        else
        {
            // Reset to original position if not dropped on a pan
            transform.position = originalPosition;
            transform.SetParent(originalParent);
        }

        IbisPuzzleManager puzzleManager = FindObjectOfType<IbisPuzzleManager>();
        if (puzzleManager != null)
        {
            if (correct)
            {
                puzzleManager.updateStatus("Correct");
            }
            else
            {
                puzzleManager.updateStatus("Incorrect");
            }
        }
    }


    private bool IsOverlapping(Collider2D panCollider)
    {
        Collider2D weightCollider = GetComponent<Collider2D>();
        if (weightCollider != null && panCollider != null)
        {
            return weightCollider.bounds.Intersects(panCollider.bounds);
        }
        return false;
    }
}
