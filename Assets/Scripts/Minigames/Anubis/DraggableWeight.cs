using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWeight : MonoBehaviour, IDragHandler, IEndDragHandler
{
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
            if (collider.CompareTag("Pan"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPan = collider;
                }
            }
        }

        // Reparent the weight if dropped on a pan
        if (closestPan != null)
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

        // Notify the puzzle manager to update weights
        JackalPuzzleManager puzzleManager = FindObjectOfType<JackalPuzzleManager>();
        if (puzzleManager != null)
        {
            puzzleManager.UpdateWeightTotals();
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
