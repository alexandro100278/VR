using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text[] answerTexts;

    private int correctAnswer;
    private int score;

    private void Start()
    {
        score = 0;
        UpdateScore();
        GenerateQuestion();
    }

    private void GenerateQuestion()
    {
        feedbackText.text = "";

        int numberA = Random.Range(1, 11);
        int numberB = Random.Range(1, 11);

        correctAnswer = numberA + numberB;

        questionText.text = $"{numberA} + {numberB} = ?";

        int correctButton = Random.Range(0, answerButtons.Length);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answer;

            if (i == correctButton)
            {
                answer = correctAnswer;
            }
            else
            {
                do
                {
                    answer = correctAnswer + Random.Range(-5, 6);
                }
                while (answer == correctAnswer || answer < 0);
            }

            answerTexts[i].text = answer.ToString();

            int selectedAnswer = answer;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(
                () => CheckAnswer(selectedAnswer)
            );
        }
    }

    private void CheckAnswer(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            feedbackText.text = "Correct!";
            score++;
            UpdateScore();

            Invoke(nameof(GenerateQuestion), 1f);
        }
        else
        {
            feedbackText.text = "Try again!";
        }
    }

    private void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }
}