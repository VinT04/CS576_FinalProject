using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TMP_Text tutorial_text;
    private string[] popups = {
        "Welcome to Tomb of Eternal Pursuit\nPress any key to begin",
        "Press \"w\" to move forwards",
        "Press \"s\" to move backward",
        "Press \"a\" to move leftwards",
        "Press \"d\" to move rightwards",
        "Press left arrow to look left",
        "Press right arrow to look right",
        "Press shift while moving forwards to sprint",
        "Press the spacebar while sprinting to jump",
        "Use the mini map to check your surroundings\nPress any key to continue",
        "Read the scroll to get your next steps"
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
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool space = Input.GetKey(KeyCode.Space);

        if (timer > time_limit)
        {
            if (index == 0 && any)
            {
                next_prompt();
                time_limit = 5.0f;
            }
            else if (index == 1 && w)
            {
                next_prompt();
            }
            else if (index == 2 && s)
            {
                next_prompt();
            }
            else if (index == 3 && a)
            {
                next_prompt();
            }
            else if (index == 4 && d)
            {
                next_prompt();
            }
            else if (index == 5 && left)
            {
                next_prompt();
            }
            else if (index == 6 && right)
            {
                next_prompt();
            }
            else if (index == 7 && w && shift)
            {
                next_prompt();
            }
            else if (index == 8 && w && shift && space)
            {
                next_prompt();
            }
            else if (index >= 9 && any)
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
        bool ctrl = Input.GetKey(KeyCode.C);
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
