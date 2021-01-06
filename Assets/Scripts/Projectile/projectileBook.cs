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

    public Projectile castProjectile(string projectileName)
    {
        Projectile projectile = Array.Find(projectiles, x => x.MyName == projectileName);

        castingBar.color = projectile.MyBarColor;

        castingBar.fillAmount = 0;

        currentProjectile.text = projectile.MyName;

        projectileRoutine = StartCoroutine(Progress(projectile));

        fadeRoutine = StartCoroutine(FadeBar());

        return projectile;
    }

    private IEnumerator Progress(Projectile projectile)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / projectile.MyCastTime;

        float progress = 0.0f;

        while (progress  <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (projectile.MyCastTime - timePassed).ToString("F2");

            if (projectile.MyCastTime - timePassed < 0)
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
       Projectile projectile =  Array.Find(projectiles, x => x.MyName == projectileName);

        return projectile;
    }
}
