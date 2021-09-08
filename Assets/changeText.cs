using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class changeText : MonoBehaviour
{
    public string[] Texts;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Text T = GetComponent<Text>();
        T.text = Texts[Random.Range(0, Texts.Length)];
    }
}
