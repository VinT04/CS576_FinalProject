using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scrolls : MonoBehaviour
{
    public TMP_Text title_text;
    public TMP_Text scroll_text;
    public GameObject canvas;
    private int index = 0;

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

    // Start is called before the first frame update
    void Start()
    {

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
        }
    }
}