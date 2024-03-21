using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    void Start()
    {
        // 시작 시 GameData 의 내역을 Debug.Log 로 보여 준다.

        Debug.Log("Game Name : " + gameData.gameName);
        Debug.Log("Game Score : " + gameData.gameScore);
        Debug.Log("is Game Active : " + gameData.isGameActive);

    }

    
}
