using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneRooms : MonoBehaviour
{
    public enum State { New, Quit, Skin, Help, Option, Reset };
    public State state;

    public GameObject ui, skinCamera;
    public HomeSceneUI sceneUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BirdMove.Player.UseCursor(true);
            switch (state)
            {
                case State.New: ui.SetActive(true); break;
                case State.Quit: ui.SetActive(true); break;
                case State.Skin: skinCamera.SetActive(true); ui.SetActive(true); break;
                case State.Help: ui.SetActive(true); break;
                case State.Option: ui.SetActive(true); break;
                case State.Reset: ui.SetActive(true); break;
            }
        }
    }
}
