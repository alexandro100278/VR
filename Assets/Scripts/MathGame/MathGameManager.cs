using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class MathGameManager : MonoBehaviour
{
    public enum MathLevel
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Mixed
    }

    [Header("Main UI")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text questionCounterText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text mistakesText;

    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text[] answerTexts;

    [Header("Level Select UI")]
    [SerializeField] private GameObject levelSelectPanel;

    [Header("Results UI")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMP_Text resultsTitleText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalMistakesText;
    [SerializeField] private TMP_Text accuracyText;

    [Header("Game Settings")]
    [SerializeField] private int questionsPerLevel = 10;

    private MathLevel currentLevel;

    private int currentQuestion;
    private int correctAnswer;

    private int levelCorrect;
    private int levelMistakes;

    private bool questionAnswered;
    private bool roundActive;

    private void Start()
    {
        ShowLevelSelect();
    }

    // ------------------------
    // LEVEL SELECT
    // ------------------------

    public void SelectAddition()
    {
        StartLevel(MathLevel.Addition);
    }

    public void SelectSubtraction()
    {
        StartLevel(MathLevel.Subtraction);
    }

    public void SelectMultiplication()
    {
        StartLevel(MathLevel.Multiplication);
    }

    public void SelectDivision()
    {
        StartLevel(MathLevel.Division);
    }

    public void SelectMixed()
    {
        StartLevel(MathLevel.Mixed);
    }

    private void StartLevel(MathLevel level)
    {
        currentLevel = level;

        currentQuestion = 0;
        levelCorrect = 0;
        levelMistakes = 0;

        questionAnswered = false;
        roundActive = true;

        levelSelectPanel.SetActive(false);
        resultsPanel.SetActive(false);

        SetGameUIActive(true);

        GenerateQuestion();
    }

    // ------------------------
    // QUESTION GENERATION
    // ------------------------

    private void GenerateQuestion()
    {
        if (!roundActive)
            return;

        questionAnswered = false;
        feedbackText.text = "";

        currentQuestion++;

        int numberA = 0;
        int numberB = 0;
        string symbol = "";

        MathLevel operationToUse = currentLevel;

        if (currentLevel == MathLevel.Mixed)
        {
            operationToUse = (MathLevel)Random.Range(0, 4);
        }

        switch (operationToUse)
        {
            case MathLevel.Addition:
                numberA = Random.Range(1, 21);
                numberB = Random.Range(1, 21);

                correctAnswer = numberA + numberB;
                symbol = "+";
                break;

            case MathLevel.Subtraction:
                numberA = Random.Range(5, 31);
                numberB = Random.Range(1, numberA + 1);

                correctAnswer = numberA - numberB;
                symbol = "-";
                break;

            case MathLevel.Multiplication:
                numberA = Random.Range(1, 13);
                numberB = Random.Range(1, 13);

                correctAnswer = numberA * numberB;
                symbol = "×";
                break;

            case MathLevel.Division:
                int divisor = Random.Range(1, 13);
                correctAnswer = Random.Range(1, 13);

                numberA = divisor * correctAnswer;
                numberB = divisor;

                symbol = "÷";
                break;
        }

        questionText.text =
            $"{numberA} {symbol} {numberB} = ?";

        GenerateAnswers();
        UpdateUI();
    }

    private void GenerateAnswers()
    {
        List<int> answers = new List<int>();

        answers.Add(correctAnswer);

        while (answers.Count < answerButtons.Length)
        {
            int offset = Random.Range(-10, 11);

            if (offset == 0)
                continue;

            int wrongAnswer = correctAnswer + offset;

            if (wrongAnswer < 0)
                continue;

            if (!answers.Contains(wrongAnswer))
            {
                answers.Add(wrongAnswer);
            }
        }

        ShuffleAnswers(answers);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int selectedAnswer = answers[i];

            answerTexts[i].text = selectedAnswer.ToString();

            answerButtons[i].onClick.RemoveAllListeners();

            answerButtons[i].onClick.AddListener(
                () => CheckAnswer(selectedAnswer)
            );
        }
    }

    private void ShuffleAnswers(List<int> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            int randomIndex = Random.Range(i, answers.Count);

            int temp = answers[i];
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }
    }

    // ------------------------
    // ANSWER CHECKING
    // ------------------------

    private void CheckAnswer(int selectedAnswer)
    {
        if (!roundActive || questionAnswered)
            return;

        if (selectedAnswer == correctAnswer)
        {
            questionAnswered = true;

            levelCorrect++;

            feedbackText.text = "Correcto!";

            UpdateUI();

            if (currentQuestion >= questionsPerLevel)
            {
                Invoke(nameof(CompleteLevel), 1f);
            }
            else
            {
                Invoke(nameof(GenerateQuestion), 1f);
            }
        }
        else
        {
            levelMistakes++;

            feedbackText.text = "Incorrecto. Intenta de Nuevo!";

            UpdateUI();
        }
    }

    // ------------------------
    // RESULTS
    // ------------------------

    private void CompleteLevel()
    {
        roundActive = false;

        SetGameUIActive(false);

        resultsPanel.SetActive(true);

        resultsTitleText.text =
            $"{GetLevelName()} Resultados";

        finalScoreText.text =
            $"Aciertos: {levelCorrect}/{questionsPerLevel}";

        finalMistakesText.text =
            $"Errores: {levelMistakes}";

        int totalAttempts =
            levelCorrect + levelMistakes;

        float accuracy = 0f;

        if (totalAttempts > 0)
        {
            accuracy =
                ((float)levelCorrect / totalAttempts) * 100f;
        }

        accuracyText.text =
            $"Exactitud: {accuracy:F1}%";
    }

    public void PlayAgain()
    {
        StartLevel(currentLevel);
    }

    public void ShowLevelSelect()
    {
        CancelInvoke();

        roundActive = false;

        levelSelectPanel.SetActive(true);
        resultsPanel.SetActive(false);

        SetGameUIActive(false);
    }

    // ------------------------
    // UI
    // ------------------------

    private void UpdateUI()
    {
        levelText.text =
            GetLevelName();

        questionCounterText.text =
            $"Pregunta {currentQuestion}/{questionsPerLevel}";

        scoreText.text =
            $"Aciertos: {levelCorrect}";

        mistakesText.text =
            $"Errores: {levelMistakes}";
    }

    private string GetLevelName()
    {
        switch (currentLevel)
        {
            case MathLevel.Addition:
                return "Sumas";

            case MathLevel.Subtraction:
                return "Restas";

            case MathLevel.Multiplication:
                return "Multiplicacion";

            case MathLevel.Division:
                return "Division";

            case MathLevel.Mixed:
                return "Mixto";

            default:
                return "";
        }
    }

    private void SetGameUIActive(bool active)
    {
        levelText.gameObject.SetActive(active);
        questionText.gameObject.SetActive(active);
        questionCounterText.gameObject.SetActive(active);
        feedbackText.gameObject.SetActive(active);
        scoreText.gameObject.SetActive(active);
        mistakesText.gameObject.SetActive(active);

        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(active);
        }
    }
}