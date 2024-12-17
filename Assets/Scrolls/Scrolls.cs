using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class Scrolls : MonoBehaviour
{
    public TMP_Text title_text;
    public TMP_Text scroll_text;
    public GameObject canvas;
    public GameObject[] scroll_arr;
    public Mummy mummy;
    internal int index = 0;

    private string[] titles = {
        "Unification of Egypt",
        "Great Pyramid of Giza",
        "Egyptian Hieroglyphs",
        "Importance of the Nile",
        "Pharaohs as Gods",
        "Mummification Process",
        "The Rosetta Stone",
        "Ra: The Sun God",
        "Advanced Medicine",
        "Queen Hatshepsut"
    };

    string[] facts = new string[]
    {
        "Ancient Egypt was unified under its first pharaoh, Narmer, around 3100 BCE. This marked the beginning of the dynastic period in Egyptian history.",
        "The Great Pyramid of Giza, built for Pharaoh Khufu, is one of the Seven Wonders of the Ancient World. It was constructed around 2560 BCE and remains a marvel of engineering.",
        "Egyptians used a writing system called hieroglyphs, consisting of over 700 symbols. These symbols were used for religious texts, official inscriptions, and record-keeping.",
        "The Nile River was the lifeblood of Egypt, providing fertile land and transportation. Annual flooding of the Nile enriched the soil, enabling agriculture to thrive.",
        "Pharaohs were considered divine rulers and intermediaries between the gods and the people. They were believed to maintain Ma'at, or cosmic balance, in the kingdom.",
        "The ancient Egyptians practiced mummification to preserve bodies for the afterlife. This involved removing internal organs and wrapping the body in linen.",
        "The Rosetta Stone, discovered in 1799, helped scholars decode Egyptian hieroglyphs. It contained the same text in Greek, Demotic, and hieroglyphic scripts.",
        "Ra, the sun god, was one of the most important deities in the Egyptian pantheon. He was believed to travel across the sky each day in a solar boat.",
        "Egyptian medicine was highly advanced, with knowledge of surgery, anatomy, and remedies. They used natural ingredients like honey and herbs to treat ailments.",
        "Queen Hatshepsut was one of Egypt's few female pharaohs, ruling during the 15th century BCE. She established trade networks and commissioned impressive building projects."
    };

    private string filename = "";
    private string filename2 = "";

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/scores.txt";
        filename2 = Application.dataPath + "/visited.txt";

        StreamReader sr = new StreamReader(filename);
        string line = sr.ReadLine();
        if (line != null)
        {
            try
            {
                index = Int32.Parse(line);
            }
            catch (FormatException)
            {
                index = 0;
            }
        }
        else
        {
            index = 0;
        }

        HashSet<string> visited_names = new HashSet<string>();
        StreamReader sr2 = new StreamReader(filename2);
        string line2 = sr2.ReadLine();
        while (line2 != null)
        {
            Debug.Log(line2);
            visited_names.Add(line2);
            line2 = sr2.ReadLine();
        }
        foreach (GameObject s in scroll_arr)
        {
            if (visited_names.Contains(s.name))
            {
                s.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Scroll"))
        {
            title_text.text = titles[index];
            scroll_text.text = facts[index];
            index += 1;
            canvas.SetActive(true);
            Destroy(other.gameObject);
            mummy.upgradeDifficult();

            StreamWriter sw = new StreamWriter(filename, false);
            sw.WriteLine("" + index);
            sw.Close();

            StreamWriter sw2 = new StreamWriter(filename2, true);
            sw2.WriteLine(other.gameObject.name);
            sw2.Close();
        }
    }
}
