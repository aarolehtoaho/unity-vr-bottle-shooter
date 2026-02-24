using UnityEngine;
using TMPro;

public class Gallow : MonoBehaviour
{
    public enum CharacterState { Tied, Hanged, Alive, Body }
    public CharacterState CurrentState = CharacterState.Tied;
    public TMP_Text ScoreText;
    public TMP_Text InfoText;

    private GameObject[] CharacterObjects;
    private GameObject[] PlankObjects;
    private bool GameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterObjects = new GameObject[3];
        CharacterObjects[0] = transform.Find("Char_Tied").gameObject;
        CharacterObjects[1] = transform.Find("Char_Dead").gameObject;
        CharacterObjects[2] = transform.Find("Char_Alive").gameObject;

        PlankObjects = new GameObject[2];
        PlankObjects[0] = transform.Find("Gallows_PLank").gameObject;
        PlankObjects[1] = transform.Find("Gallows_PLank_Open").gameObject;
    }

    void Update() {
        if (GameOver) return;
        if (GetCurrentScore() == -10 || CurrentState == CharacterState.Hanged)
        {
            CurrentState = CharacterState.Hanged;
            SetCharacterState(CurrentState);
            SetPlankState(true);
            GameOver = true;
            SetInfoText("Game Over!", Color.red);
        } else if (GetCurrentScore() >= 100)
        {
            CurrentState = CharacterState.Alive;
            SetCharacterState(CurrentState);
            SetPlankState(false);
            GameOver = true;
            SetInfoText("You Win!", Color.green);
        }
    }

    public void SetPlankState(bool isOpen)
    {
        PlankObjects[0].SetActive(!isOpen);
        PlankObjects[1].SetActive(isOpen);
    }

    public void SetCharacterState(CharacterState state)
    {
        CurrentState = state;
        // Body and Alive uses the same GameObject
        state = state == CharacterState.Body ? CharacterState.Alive : state;
        for (int i = 0; i < CharacterObjects.Length; i++)
        {
            if (i == (int)state)
            {
                CharacterObjects[i].SetActive(true);
            } else
            {
                CharacterObjects[i].SetActive(false);
            }
        }
    }

    public GameObject GetCharacterObject()
    {
        CharacterState state = CurrentState == CharacterState.Body ? CharacterState.Alive : CurrentState;
        return CharacterObjects[(int)state];
    }

    int GetCurrentScore()
    {
        return int.Parse(ScoreText.text);
    }

    void SetInfoText(string text, Color color)
    {
        InfoText.text = text;
        InfoText.color = color;
    }
}
