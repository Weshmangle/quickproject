using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAbout : MonoBehaviour
{
    protected float valueAnimation = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(valueAnimation < .25)
            valueAnimation += Time.deltaTime;
        else
            transform.localScale = Vector3.one;
    }

    public void Click()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1.2f,1.2f,1);
        valueAnimation = 0;
    }
}
