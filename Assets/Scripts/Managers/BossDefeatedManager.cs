using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeatedManager : MonoBehaviour
{

    public bool defeated = false;
    bool called = false;
    public void Update()
    {
        if (called) return;

        if (defeated)
        {
            called = true;
            StartCoroutine("WaitSomeTimeAndEndGame");
        }
        
    }

    IEnumerator WaitSomeTimeAndEndGame()
    {
        Debug.Log("Boss died");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
}
