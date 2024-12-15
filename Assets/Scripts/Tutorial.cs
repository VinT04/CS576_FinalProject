using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TMP_Text tutorial_text;
    private string[] popups = {
        "Welcome to Tomb of Eternal Pursuit\nPress any key to begin",
        "Press \"w\" or the up arrow to move forwards",
        "Press \"s\" or the down arrow to move backward",
        "Press \"a\" or the left arrow to look left",
        "Press \"d\" or the right arrow to look right",
        "Press shift while moving forwards to sprint",
        "Press the spacebar while sprinting to jump",
        "Use the mini map to check your surroundings\nPress any key to continue",
        "Avoid being caught by the mummy\nPress any key to continue",
        "Now find your way to the pyramid to begin"
    };
    private int index = 0;
    private float timer = 0.0f;
    private float time_limit = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        tutorial_text.text = popups[index];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        bool any = Input.anyKey;
        bool shift = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        bool space = Input.GetKeyDown(KeyCode.Space);

        if (timer > time_limit)
        {
            if (index == 0 && any)
            {
                next_prompt();
                time_limit = 5.0f;
            }
            else if (index == 1 && up)
            {
                next_prompt();
            }
            else if (index == 2 && down)
            {
                next_prompt();
            }
            else if (index == 3 && left)
            {
                next_prompt();
            }
            else if (index == 4 && right)
            {
                next_prompt();
            }
            else if (index == 5 && up && shift)
            {
                next_prompt();
            }
            else if (index == 6 && up && shift && space)
            {
                next_prompt();
            }
            else if (index > 6 && index < 9 && any)
            {
                next_prompt();
            }
        }
    }

    private void next_prompt()
    {
        index += 1;
        if (index < popups.Length)
        {
            tutorial_text.text = popups[index];
        }
        timer -= time_limit;
    }

    IEnumerator tutorial_runner()
    {
        bool ctrl = Input.GetKeyDown(KeyCode.C);
        bool any = Input.anyKey;
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool space = Input.GetKey(KeyCode.Space);

        if (index == 0 && any)
        {
            yield return new WaitForSecondsRealtime(4);
            index += 1;
        }
        else if (index == 1 && up)
        {
            yield return new WaitForSecondsRealtime(4);
            index += 1;
        }
    }
}
