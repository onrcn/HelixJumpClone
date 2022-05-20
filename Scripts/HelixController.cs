using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRot;

    public Transform topTransform;
    public Transform bottomTransform;

    public GameObject helixLevelPrefab;

    public List<StageBehaviour> allStages = new List<StageBehaviour>();
    private float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();

    void Awake() 
    {
        startRot = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (bottomTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }
    void Update()
    {
        if(Input.GetMouseButton(0))    
        {
            Vector2 currTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = currTapPos;
            }

            float delta = lastTapPos.x - currTapPos.x;
            lastTapPos = currTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if(Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        StageBehaviour stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if (stage == null)
        {
            Debug.LogError("Stage " + stageNumber + " not found");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
    
        // Reset rotation
        transform.localEulerAngles = startRot;

        foreach(GameObject go in spawnedLevels)
        {
            Destroy(go);
        }
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.levels[i].partCount; 
            List<GameObject> disableParts = new List<GameObject>();

            while (disableParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disableParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart);
                }
            }
            
            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform child in level.transform)
            {
                child.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if(child.gameObject.activeInHierarchy)
                    leftParts.Add(child.gameObject);
            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }

}