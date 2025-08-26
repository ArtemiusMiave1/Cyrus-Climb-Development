using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollectableCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableCounter;
    [SerializeField] private int collectableTotal;

    public static int collectableCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        collectableCounter.text = collectableCount + "/" + collectableTotal;
    }
}