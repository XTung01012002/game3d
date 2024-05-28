using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Animator character;
    public Button[] buttons;
    public GameObject[] grids;
    public TMP_Text[] texts;
    public AudioClip[] audios;
    public AudioSource audioSource;
    public CinemachineVirtualCamera virtualCamera;

    private float startTime = 0f;
    private bool isDraggingFirstWord = false;

    private int totalScore = 0;
    private int gridIndex = 0;
    private int buttonClickCount = 0;
    private int mistakeScore = 0;




    private string[] seedArray = { "hai hạt nảy mầm", "hai hạt không nảy mầm", "một hạt nảy mầm và một hạt không nảy mầm" };

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        foreach (TMP_Text txt in texts)
        {
            txt.text = "";
        }
    }


    private void OnButtonClick()
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        
        if (!clickedButton.interactable)
        {
            Debug.Log("Button đã được nhấn");
            return;
        }

        clickedButton.interactable = false;

        if (isDraggingFirstWord && buttonClickCount < 3) // keo ki tự đầu tiên và đủ 3 button
        {
            buttonClickCount++;
            totalScore += ProcessGameLogic();

            if (buttonClickCount == 3)
            {
                //Debug.Log("Hi " + totalScore);
                float duration = Time.time - startTime;
                int roundedDuration = Mathf.RoundToInt(duration);

                //Debug.Log("Thời gian từ lúc bắt đầu kéo đến khi nhấn đủ 3 button: " + duration + " giây")
                mistakeScore = 3 - totalScore;
                texts[0].text = totalScore.ToString() + "/3";
                texts[1].text = mistakeScore.ToString() + "/3";
                texts[2].text = roundedDuration.ToString();

                virtualCamera.Priority = 2;
                audioSource.clip = audios[2];
                audioSource.Play();
            }
        }
    }

    public void OnBeginDragFirstWord()
    {
        if (!isDraggingFirstWord)
        {
            startTime = Time.time;
            isDraggingFirstWord = true;
        }
    }

    private int ProcessGameLogic() // kiểm tra đúng sai
    {
        string results = "";

        foreach (Transform child in grids[gridIndex].transform)
        {
            TMP_Text textComponent = child.GetComponentInChildren<TMP_Text>();

            if (textComponent != null)
            {
                string txt = textComponent.text;
                results += txt + " ";
            }
        }

        gridIndex++;
        results = results.TrimEnd();
        results = results.Replace("\n", "").Replace("\r", "");
        Debug.Log(results);

        bool foundMatch = false;

        foreach (string item in seedArray)
        {
            if (results == item)
            {
                foundMatch = true;
                break;
            }
        }

        character.SetTrigger(foundMatch ? "reaction right" : "reaction wrong");
        print("Found match: " + foundMatch);
        int audioIndex = foundMatch ? 0 : 1;
        audioSource.clip = audios[audioIndex];
        audioSource.Play();

        return foundMatch ? 1 : 0;
    }
    
}
