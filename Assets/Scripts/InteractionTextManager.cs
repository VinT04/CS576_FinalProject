using UnityEngine;
using UnityEngine.UI;

public class InteractionTextManager : MonoBehaviour
{
    public static InteractionTextManager Instance;

    [SerializeField] private Text interactionText; // Text for interaction messages
    [SerializeField] private Image blackScreen;    // Black screen UI image
    [SerializeField] private Text loadingText; // Text for interaction messages

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowText(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    public void HideText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    public void ShowLoadingScreen(string message)
    {
        if (blackScreen != null)
        {
            // Enable the black screen
            blackScreen.color = new Color(0, 0, 0, 1); // Fully opaque black
            blackScreen.gameObject.SetActive(true);

            // Show loading message (optional)
            if (loadingText != null)
            {
                loadingText.text = message;
                loadingText.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Black screen not assigned.");
        }
    }

    public void HideLoadingScreen()
    {
        if (blackScreen != null)
        {
            // Disable the black screen
            blackScreen.color = new Color(0, 0, 0, 0); // Fully transparent
            blackScreen.gameObject.SetActive(false);

            // Hide loading message
            if (loadingText != null)
            {
                loadingText.text = "";
                loadingText.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Black screen not assigned.");
        }
    }

    public void FadeInLoadingScreen(float duration, string message = "")
    {
        StartCoroutine(FadeIn(duration, message));
    }

    public void FadeOutLoadingScreen(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    private System.Collections.IEnumerator FadeIn(float duration, string message)
    {
        if (blackScreen != null)
        {
            Color screenColor = blackScreen.color;
            screenColor.a = 100; // Start fully transparent
            blackScreen.color = screenColor;
            blackScreen.gameObject.SetActive(true);

            if (loadingText != null)
            {
                loadingText.text = message;
                loadingText.gameObject.SetActive(true);
            }

            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                screenColor.a = Mathf.Lerp(1, 1, timer / duration); // Gradually increase alpha
                blackScreen.color = screenColor;
                yield return null;
            }

            screenColor.a = 1; // Ensure fully opaque
            blackScreen.color = screenColor;
        }
    }

    private System.Collections.IEnumerator FadeOut(float duration)
    {
        if (loadingText != null)
            {
                loadingText.text = "";
                loadingText.gameObject.SetActive(false);
        }
        
        if (blackScreen != null)
        {
            Color screenColor = blackScreen.color;
            screenColor.a = 1; // Start fully opaque
            blackScreen.color = screenColor;

            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                screenColor.a = Mathf.Lerp(1, 0, timer / duration); // Gradually decrease alpha
                blackScreen.color = screenColor;
                yield return null;
            }

            screenColor.a = 0; // Ensure fully transparent
            blackScreen.color = screenColor;
            blackScreen.gameObject.SetActive(false);
        }
    }
}
