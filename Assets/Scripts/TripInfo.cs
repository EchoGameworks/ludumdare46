using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TripInfo : MonoBehaviour
{
    public int CurrentStageNum;
    public List<StageInfo> Stages;
    public Transform CharacterHolder;
    public StageInfo CurrentStageInfo;
    public GameManager gameManager;
    public StageManager stageManager;
    public UIOverlay uiOverlay;
    public CameraControl cameraControl;
    public Transform CameraResetPosition;

    public GameObject TestObject;
    public Transform TestPostitionTransform;

    public void Start()
    {
        Restart();
        //GameObject spawnGO = Instantiate(TestObject, CharacterHolder, true);
        //spawnGO.transform.position = TestPostitionTransform.position;
    }

    public void Restart()
    {
        uiOverlay.HideGameOver();
        uiOverlay.HideToolTip();
        foreach (Transform t in CharacterHolder)
        {
            Destroy(t.gameObject);
        }
        stageManager.FirstStart = true;
        CurrentStageNum = 0;
        GenerateStage(true);

        cameraControl.followTransform = CameraResetPosition;
        //LeanTween.move(stageManager.cam.gameObject, CameraResetPosition.position, 1f).setEaseInOutQuad();
    }

    public void GenerateStage(bool useAllSpawns)
    {
        CurrentStageInfo = Stages[CurrentStageNum];

        foreach (SpawnInfo si in CurrentStageInfo.Spawns)
        {
            int usedCount = Random.Range(si.MinNumber, si.MaxNumber);
            for(int i = 1; i <= usedCount; i++)
            {
                GameObject spawnGO = Instantiate(si.prefabEnemy, CharacterHolder, true);
                spawnGO.GetComponent<NavMeshAgent>().enabled = false;
                LeanTween.delayedCall(0.1f, () => spawnGO.GetComponent<NavMeshAgent>().enabled = true);
                spawnGO.transform.position = RandomPointInBox(si.SpawnLocation.position, 1f);
                spawnGO.transform.localScale = Vector3.zero;
                LeanTween.scale(spawnGO, Vector3.one, 0.5f).setEaseInOutCirc();
            }

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
