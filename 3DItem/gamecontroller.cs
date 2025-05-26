using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    private CreateCharacter characterCreator;
    public TextMeshProUGUI itemCountText;
    private player playerScript;
    public int maxItemCount = 8;

    public float timeLeft = 75f;
    public TextMeshProUGUI timerText;
    public Transform player;
    public float interactDistance = 3f;
    public string storageTag = "Storage";

    private bool isGameOver = false;

    public Transform targetObject;
    public float raiseHeight = 1.3f; 
    public float raiseDuration = 4f;
    private bool hasRaisedObject = false;

    void Start()
    {
        playerScript = player.GetComponent<player>();
        characterCreator = new CreateCharacter();
    }

    void Update()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;
        UpdateTimerUI();
        UpdateItemCountUI();

        if (timeLeft <= 30f && !hasRaisedObject)
        {
            StartCoroutine(RaiseObjectSmoothly());
            hasRaisedObject = true;
        }

        if (timeLeft <= 1f)
        {
            Clear();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckStorageInteraction();
        }
    }

    void UpdateTimerUI()
    {
        timeLeft = Mathf.Clamp(timeLeft, 0f, 75f);
        int seconds = Mathf.FloorToInt(timeLeft);
        timerText.text = seconds.ToString("00");
    }

    void CheckStorageInteraction()
    {
        GameObject[] storages = GameObject.FindGameObjectsWithTag(storageTag);
        foreach (GameObject storage in storages)
        {
            float distance = Vector3.Distance(player.position, storage.transform.position);
            if (distance <= interactDistance)
            {
                Clear();
                return;
            }
        }
    }

    void Clear()
    {
        isGameOver = true;
        Debug.Log("Clear!");
        timerText.text = "CLEAR!";

        GameManager.Instance.player = characterCreator.CharacterFarmed("Player", true);
        GameManager.Instance.doctor = characterCreator.CharacterFarmed("Doctor", Convert.ToBoolean(Storage.inst.white));
        GameManager.Instance.engineer = characterCreator.CharacterFarmed("Engineer", Convert.ToBoolean(Storage.inst.yellow));
        GameManager.Instance.guard = characterCreator.CharacterFarmed("Guard", Convert.ToBoolean(Storage.inst.blue));

        if (Convert.ToBoolean(Storage.inst.white)) GameManager.Instance.doctor.isAlive = true;
        if (Convert.ToBoolean(Storage.inst.yellow)) GameManager.Instance.engineer.isAlive = true;
        if (Convert.ToBoolean(Storage.inst.blue)) GameManager.Instance.guard.isAlive = true;

        GameManager.Instance.RefreshCharacterList();

        GameManager.Instance.itemData.foodN = Storage.inst.food;
        GameManager.Instance.itemData.waterN = Storage.inst.water;
        GameManager.Instance.itemData.gameKitN = Storage.inst.bordgame;
        GameManager.Instance.itemData.medKitN = Storage.inst.medicine;
        GameManager.Instance.itemData.bookN = Storage.inst.book;
        GameManager.Instance.itemData.mapN = Storage.inst.map;
        GameManager.Instance.itemData.researchN = Storage.inst.research;
        GameManager.Instance.itemData.batteryN = Storage.inst.battery;
        GameManager.Instance.itemData.oxyentankN = Storage.inst.oxygentank;
        GameManager.Instance.itemData.fixtoolN = Storage.inst.fixtool;

        SceneManager.LoadScene("PickPropertyScene");
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        timerText.text = "GAME OVER";
    }

    void UpdateItemCountUI()
    {
        int currentCount = playerScript.GetTotalItemCount();
        itemCountText.text = $"{currentCount}/{maxItemCount}";
    }
    
    System.Collections.IEnumerator RaiseObjectSmoothly()
    {
        if (targetObject == null) yield break;

        Vector3 startPos = targetObject.position;
        Vector3 endPos = startPos + new Vector3(0, raiseHeight, 0);
        float elapsed = 0f;

        while (elapsed < raiseDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / raiseDuration);
            targetObject.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}