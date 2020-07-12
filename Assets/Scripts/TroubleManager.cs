using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TroubleManager : MonoBehaviour {

    [SerializeField] private Slider progressBar;
    [SerializeField] private float timeElapsed;
    [SerializeField] LightEffect lightEffect;
    public float timeBeforeNightEnd;

    [SerializeField] public bool shouldSpawnTrouble = true;
    public bool gameStarted = false;

    [SerializeField] private int timeBetweenDifficultyIncrease;
    [SerializeField] private int currentMinTroubleDelay;
    [SerializeField] private int currentMaxTroubleDelay;

    [SerializeField] public List<List<Troublemaker>> typesOfTroubles = new List<List<Troublemaker>>();
    private readonly float[] m_weights = new float[2];
    
    [SerializeField] int weightTroubleGuy;
    private GameObject[] troubleGuyObjects;
    [SerializeField] List<Troublemaker> troubleGuys;

    [SerializeField] int weightTroubleMachine;
    private GameObject[] troubleMachineObjects;
    [SerializeField] List<Troublemaker> troubleMachines;

    [SerializeField] private List<Troublemaker> availableTroubles;
    [SerializeField] private List<Troublemaker> selectedList;

    void Start() {
        m_weights[0] = weightTroubleGuy;
        m_weights[1] = weightTroubleMachine;

        troubleGuyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject troubleGuyObject in troubleGuyObjects) {
            var troubleGuy = troubleGuyObject.GetComponent<Troublemaker>();
            troubleGuys.Add(troubleGuy);
        }

        troubleMachineObjects = GameObject.FindGameObjectsWithTag("Machine");
        foreach (GameObject troubleMachineObject in troubleMachineObjects) {
            var troubleMachine = troubleMachineObject.GetComponent<Troublemaker>();
            troubleMachines.Add(troubleMachine);
        }

        typesOfTroubles.Add(troubleGuys);
        typesOfTroubles.Add(troubleMachines);

        StartCoroutine(StartTrouble());
        StartCoroutine(IncreaseDifficultyOverTime());
    }

    void Update() {
        timeElapsed = progressBar.value;

        if (timeElapsed == 22) {
            lightEffect.lightSwitchingDelay = 1f;
        }
        else if (timeElapsed == 39) {
            lightEffect.lightSwitchingDelay = 0.5f;
        }
        else if (timeElapsed == 50) {
            lightEffect.lightSwitchingDelay = 0.25f;
        }
        else if (timeElapsed == 77) {
            lightEffect.ShutOffLights();
            lightEffect.lightSwitchingDelay = 1f;
        }
        else if (timeElapsed == 91) {
            lightEffect.lightSwitchingDelay = 0.25f;
        }
        else if (timeElapsed == 242) {
            lightEffect.lightSwitchingDelay = 0.5f;
        }
        else if (timeElapsed == 250) {
            lightEffect.lightSwitchingDelay = 0.25f;
        }
        else if (timeElapsed == 265) {
            lightEffect.lightSwitchingDelay = 0.1f; 
        }
        else if (timeElapsed >= timeBeforeNightEnd) {
            End();
        }
    }

    private IEnumerator IncreaseDifficultyOverTime() {
        while (true) {
            yield return new WaitForSeconds(timeBetweenDifficultyIncrease);
            currentMinTroubleDelay = Mathf.Max(3, currentMinTroubleDelay - timeBetweenDifficultyIncrease);
            currentMaxTroubleDelay = Mathf.Max(9, currentMaxTroubleDelay - timeBetweenDifficultyIncrease);

            weightTroubleGuy++;
            weightTroubleMachine++;

            Debug.Log($"<color=green>[Increasing difficulty] Min: {currentMinTroubleDelay} Max: {currentMaxTroubleDelay}, Weights {weightTroubleGuy}, {weightTroubleMachine}</color>", this);
        }
    }

    private int GetRandomWeightedIndex() {
        var weightSum = m_weights.Sum();
        var randomIndex = Random.Range(0, weightSum);

        if (randomIndex <= m_weights[0]) {
            return 0;
        }

        if (randomIndex <= m_weights[0] + m_weights[1]) {
            return 1;
        }

        return 0;
    }

    private Troublemaker GetTrouble() {
        selectedList = typesOfTroubles[GetRandomWeightedIndex()];

        foreach (Troublemaker trouble in selectedList) {
            if (trouble.transform.gameObject.activeInHierarchy) {
                availableTroubles.Add(trouble);
            }
        }

        var troubleToSpawn = Random.Range(0, availableTroubles.Count);

        return availableTroubles[troubleToSpawn];
    }

    private IEnumerator StartTrouble() {
        while (shouldSpawnTrouble) {
            if (gameStarted) {
                var currentDelay = Random.Range(currentMinTroubleDelay, currentMaxTroubleDelay);
                yield return new WaitForSeconds(currentDelay);

                var trouble = GetTrouble();
                trouble.isCausingTrouble = true;
            }

            yield return null;
        }
    }

    void End() {
        Menu.Instance.EndGame();
    }
}
