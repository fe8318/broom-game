using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class InstructionUIManager : MonoBehaviour
{
    public GameObject InstructionUI;
    // Start is called before the first frame update
    void Start()
    {
        if (UIStats.playerSeenInstruction) InstructionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseInstructionUI();
    }

    public void CloseInstructionUI()
    {
        Debug.Log("CloseInstructionUI P1");
        UIStats.playerSeenInstruction = true;
        Debug.Log("CloseInstructionUI P2");
        InstructionUI.SetActive(false);
        Debug.Log("CloseInstructionUI P3");
    }

}
