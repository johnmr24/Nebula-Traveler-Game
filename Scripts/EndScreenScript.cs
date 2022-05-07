using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour
{
    [Header("Set In Inspecter")]
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Final Score: " + Main.S.score;
    }
}
