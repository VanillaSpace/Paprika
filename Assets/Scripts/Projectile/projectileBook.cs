using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text dartName;

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

    public Projectile castProjectile(int index)
    {
        castingBar.color = projectiles[index].MyBarColor;

        castingBar.fillAmount = 0;

        dartName.text = projectiles[index].MyName;

        projectileRoutine = StartCoroutine(Progress(index));

        fadeRoutine = StartCoroutine(FadeBar());

        return projectiles[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / projectiles[index].MyCastTime;

        float progress = 0.0f;

        while (progress  <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (projectiles[index].MyCastTime - timePassed).ToString("F2");

            if (projectiles[index].MyCastTime - timePassed < 0)
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
}
