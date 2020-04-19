using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripInfo : MonoBehaviour
{
    public int CurrentStageNum;
    public List<StageInfo> Stages;
    public Transform CharacterHolder;
    public StageInfo CurrentStageInfo;
    public GameManager gameManager;
    public StageManager stageManager;

    public void Start()
    {
        Restart();
    }

    public void Restart()
    {
        foreach(Transform t in CharacterHolder)
        {
            Destroy(t.gameObject);
        }
        stageManager.FirstStart = true;
        CurrentStageNum = 0;
        GenerateStage(true);
    }

    public void GenerateStage(bool useAllSpawns)
    {
        CurrentStageInfo = Stages[CurrentStageNum];

        int count = 0;
        foreach(SpawnInfo si in CurrentStageInfo.Spawns)
        {
            GameObject spawnGO = Instantiate(si.prefabEnemy, CharacterHolder, true);
            spawnGO.transform.position = si.SpawnLocation.position + new Vector3(count, 0f, 0f);
            spawnGO.transform.localScale = Vector3.zero;
            LeanTween.scale(spawnGO, Vector3.one, 0.5f).setEaseInOutCirc();
        }
    }

    private static Vector3 RandomPointInBox(Vector3 center, float size)
    {
        return center + new Vector3(
           (Random.value - 0.5f) * size,
           0f,
           (Random.value - 0.5f) * size
        );
    }

    public void NextStage()
    {
        CurrentStageNum++;
        GenerateStage(true);
    }
}
