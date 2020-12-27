using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    private Image content;
    
    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;

    public float MyMaxValue { get; set; }

    private float currentValue;

    public float myCurrentValue 
    {
      get 
        { 
            return currentValue; 
        }

      set 
        {
            //our current value will never go over max value
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            // our current value will never go negative
            else if (value < 0 )
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
            statValue.text = currentValue + " / " + MyMaxValue;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        //this will make the animation smoother when increasing and decreasing
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

           // content.fillAmount = currentFill;
    }

    public void Initialize(float maxValue, float currentValue)
    {
        MyMaxValue = maxValue;
        myCurrentValue = currentValue;
    }
}
