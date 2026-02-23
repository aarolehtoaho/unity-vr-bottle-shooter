using UnityEngine;
using TMPro;

public class Gallow : MonoBehaviour
{
    public enum CharacterState { Tied, Dead, Alive }
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
        if (getCurrentScore() == 0)
        {
            CurrentState = CharacterState.Dead;
            setCharacterState(CurrentState);
            setPlankState(true);
            GameOver = true;
            setInfoText("Game Over!", Color.red);
        } else if (getCurrentScore() >= 100)
        {
            CurrentState = CharacterState.Alive;
            setCharacterState(CurrentState);
            setPlankState(false);
            GameOver = true;
            setInfoText("You Win!", Color.green);
        }
    }

    void setPlankState(bool isOpen)
    {
        PlankObjects[0].SetActive(!isOpen);
        PlankObjects[1].SetActive(isOpen);
    }

    void setCharacterState(CharacterState state)
    {
        for (int i = 0; i < CharacterObjects.Length; i++)
        {
            CharacterObjects[i].SetActive(i == (int)state);
        }
    }

    int getCurrentScore()
    {
        return int.Parse(ScoreText.text);
    }

    void setInfoText(string text, Color color)
    {
        InfoText.text = text;
        InfoText.color = color;
    }
}
