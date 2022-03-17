using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpeaker : MonoBehaviour
{
    protected int state = 0;
    [SerializeField] public Sprite [] sprites = new Sprite[3];

    protected float valueAnimation = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(valueAnimation < .25)
            valueAnimation += Time.deltaTime;
        else
            transform.localScale = Vector3.one;
    }

    public void Click()
    {
        state = (state + 1) % 3;
        GetComponent<Image>().sprite = sprites[state];

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1.2f,1.2f,1);
        valueAnimation = 0;

        switch (state)
        {
            case 0:
                AudioManager.Instance.SetOffMusic();
                break;
            case 1:
                AudioManager.Instance.SetMiddleMusic();
                break;
            case 2:
                AudioManager.Instance.SetOnMusic();
                break;
        }
    }
}
