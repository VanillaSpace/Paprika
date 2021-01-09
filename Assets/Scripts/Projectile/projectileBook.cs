using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileBook : MonoBehaviour
{
    public static projectileBook instance;

    public static projectileBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<projectileBook>();
            }
            return instance;
        }

    }


    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text currentProjectile;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private CanvasGroup canvaGroup;

    [SerializeField]
    private Projectile[] projectiles;

    private Coroutine projectileRoutine;

    private Coroutine fadeRoutine;
    
    void Start()
    {
       
    }

   
    void Update()
    {
        
    }

    public void Cast(ICastable castable)
    {
        castingBar.fillAmount = 0;

        castingBar.color = castable.MyBarColor;
                
        currentProjectile.text = castable.MyTitle;

        projectileRoutine = StartCoroutine(Progress(castable));

        fadeRoutine = StartCoroutine(FadeBar());

    }

    private IEnumerator Progress(ICastable castable)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / castable.MyCastTime;

        float progress = 0.0f;

        while (progress  <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (castable.MyCastTime - timePassed).ToString("F2");

            if (castable.MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }

        stopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvaGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void stopCasting()
    {
        if(fadeRoutine!= null)
        {
            StopCoroutine(fadeRoutine);
            canvaGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (projectileRoutine != null)
        {
            StopCoroutine(projectileRoutine);
            projectileRoutine = null;
        }
    }

    public Projectile GetProjectile(string projectileName)
    {
       Projectile projectile =  Array.Find(projectiles, x => x.MyTitle == projectileName);

        return projectile;
    }
}
