using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGame : MonoBehaviour
{
    [Header("Fish")]
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timerMultiplier = 3f;

    float fishSpeed;
    [SerializeField] float smooth = 1f;

    [Header("Hook")]
    [SerializeField] Transform hook;
    [SerializeField] RectTransform hookRT;
    float hookPos;
    [SerializeField] float hookSize;
    [SerializeField] float hookPower = 0.5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDecay = 0.1f;

    [Header("Progress Bar")]
    [SerializeField] Transform progBarContainer;

    [Header("Fish Prefab")]
    [SerializeField] GameObject fishPrefab;
    [SerializeField] float dropSpread = 0.7f;
    [SerializeField] int dropCount = 1;
    [SerializeField] Transform FishingArea;

    private void Start()
    {
        ResizeHook();
    }


    private void Update()
    {
        FishMovement();
        Hook();
        ProgressCheck();
    }

    private void ResizeHook() => hookRT.sizeDelta = new Vector2(35.5f, 35f);

    void Hook()
    {
        if(Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
            Debug.Log("Pressed primary button.");
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        hookPos += hookPullVelocity;   
        hookPos = Mathf.Clamp(hookPos, hookSize / 2, 1 - hookSize/2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPos);
    }

    void FishMovement()
    {
        fishTimer -= Time.deltaTime;
        if(fishTimer < 0)
        {
            fishTimer = Random.value * timerMultiplier;

            fishDestination = Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smooth);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);

    }

    void ProgressCheck()
    {
        Vector3 ls = progBarContainer.localScale;
        ls.y = hookProgress;
        progBarContainer.localScale = ls;

        float min = hookPos - hookSize / 2;
        float max = hookPos + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDecay * Time.deltaTime;
        }

        if (hookProgress >= 1f)
        {
            //spawn fish
            SpawnFish();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    void SpawnFish()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = FishingArea.position;
            position.x += dropSpread * Random.value - dropSpread / 2;
            position.y += dropSpread * Random.value - dropSpread / 2;
            position.z = 0f;

            GameObject go = Instantiate(fishPrefab);
            go.transform.position = position;
        }
    }
}
