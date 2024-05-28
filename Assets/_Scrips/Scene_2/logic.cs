using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class logic : MonoBehaviour
{ 
    public TextMeshProUGUI[] textInBoards, textInSlides;
    public Button button;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public CinemachineVirtualCamera virtualCamera;

    public float startTime = -1f;
    public int score = 0;
    private int wrongCount;

    public string[] correctAnswers = { "SAI", "ĐÚNG", "ĐÚNG", "SAI", "SAI" };

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void HandleCollision(Collider2D collision, TextMeshProUGUI draggedText)
    {
        int dropZoneIndex = -1;

        switch (collision.tag)
        {
            case "1":
                dropZoneIndex = 0;
                break;
            case "2":
                dropZoneIndex = 1;
                break;
            case "3":
                dropZoneIndex = 2;
                break;
            case "4":
                dropZoneIndex = 3;
                break;
            case "5":
                dropZoneIndex = 4;
                break;
        }

        if (dropZoneIndex != -1)
        {
            textInBoards[dropZoneIndex].SetText(draggedText.text);

            if (draggedText.text.Trim().Equals(correctAnswers[dropZoneIndex], System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("true");
            }
            else
            {
                wrongCount++;
                Debug.Log("false");
            }
        }
    }

    private void OnButtonClick()
    {
        int dem = 0;
        foreach (TextMeshProUGUI text in textInBoards)
        {
            if (text.text.Trim().Equals(correctAnswers[dem]))
            {
                score++;
            }
            dem++;
        }

        float elapsedTime = CalculateElapsedTime();
        int roundedDuration = Mathf.RoundToInt(elapsedTime);
        textInSlides[0].text = score.ToString() + "/5";
        textInSlides[1].text = wrongCount.ToString();
        textInSlides[2].text = roundedDuration.ToString();

        virtualCamera.Priority = 2;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void SetStartTime(float time)
    {
        startTime = time; // Thiết lập startTime từ script DragAndDrop
    }

    public float CalculateElapsedTime()
    {
        return Time.time - startTime; // Tính thời gian đã trôi qua từ startTime đến thời điểm hiện tại
    }
}
