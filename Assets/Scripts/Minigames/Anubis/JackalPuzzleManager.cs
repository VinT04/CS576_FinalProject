using UnityEngine;
using UnityEngine.UI;

public class JackalPuzzleManager : MonoBehaviour
{
    public Transform leftPan;
    public Transform rightPan;
    public Transform scalePivot;

    public Text statusMessage; // Legacy Text for status
    private int totalLeft = 0;
    private int totalRight = 0;
    private float rotationSpeed = 5f;

    private bool puzzleComplete = false;

    private void Start()
    {
        UpdateWeightTotals();
    }

    private void Update()
    {
        UpdateScaleRotation();
        CheckPuzzleCompletion();
    }

    public void UpdateWeightTotals()
    {
        totalLeft = CalculateWeight(leftPan);
        totalRight = CalculateWeight(rightPan);
        //Debug.Log($"total right and total left are {totalRight} and {totalLeft}");
    }

    private int CalculateWeight(Transform pan)
    {
        int total = 0;

        // Debug: Check all children of the pan
        //Debug.Log($"Calculating weight for {pan.name}. Total children: {pan.childCount}");

        foreach (Transform child in pan)
        {
            // Check if the parent object (child) has a Weight component
            Weight weight = child.GetComponent<Weight>();
            if (weight == null)
            {
                //Debug.LogWarning($"Child {child.name} in {pan.name} does not have a Weight component. Skipping.");
                continue; // Skip objects without the Weight component
            }

            //Debug.Log($"Adding weight {weight.weightValue} from {child.name} in {pan.name}");
            total += weight.weightValue;
        }

        //Debug.Log($"Total weight for {pan.name} is {total}");
        return total;
    }


    private void UpdateScaleRotation()
    {
        // Calculate the imbalance based on total weights
        float imbalance = totalLeft - totalRight;

        // Calculate the target rotation for the pivot
        float targetRotation = Mathf.Clamp(imbalance * 2f, -30f, 30f);
        scalePivot.localRotation = Quaternion.Lerp(
            scalePivot.localRotation,
            Quaternion.Euler(0, 0, targetRotation),
            Time.deltaTime * rotationSpeed
        );

        // Adjust the vertical positions of the pans
        float maxPanOffset = 30f; // Maximum vertical offset for the pans
        float leftPanOffset = Mathf.Clamp(-imbalance * 3f, -maxPanOffset, maxPanOffset);
        float rightPanOffset = Mathf.Clamp(imbalance * 3f, -maxPanOffset, maxPanOffset);

        // Update the positions of the pans
        leftPan.localPosition = new Vector3(leftPan.localPosition.x, leftPanOffset - 100, leftPan.localPosition.z);
        rightPan.localPosition = new Vector3(rightPan.localPosition.x, rightPanOffset - 100, rightPan.localPosition.z);
    }

    private void CheckPuzzleCompletion()
    {
        if (puzzleComplete) return;

        if (totalLeft == totalRight && totalLeft + totalRight == 24)
        {
            puzzleComplete = true;
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle balanced!");
        statusMessage.text = "Balanced!";
        statusMessage.color = Color.green;

        JackalPuzzleGenerator generator = FindObjectOfType<JackalPuzzleGenerator>();
        if (generator != null)
        {
            generator.CompletePuzzle();
        }
    }

    public void ResetPuzzle()
    {
        Debug.Log("Resetting puzzle...");
        puzzleComplete = false;
        statusMessage.text = "Imbalanced";
        statusMessage.color = Color.red;
    }
}
