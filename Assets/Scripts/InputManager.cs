using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Player Player;
    public static InputManager Instance;

    public Building CurrentBuilding;

    private bool MenuOpened = false;
    public Menu Menu;

    private bool isPaused;


    // Get Event From Input Manager
    public void OnMove(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }

        float axis = action.ReadValue<float>();
        Player.Direction = axis;
    }

    public void OnInteract(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }

    
        if (action.ReadValue<float>() > 0)
        {
            if (MenuOpened)
            {
                Menu.Interact();
                return;
            }

            if (CurrentBuilding)
            {
                CurrentBuilding.Interact();
                return;
            }
        }
    }

    public void OnMenu(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }

        if (action.ReadValue<float>() > 0)
        {
            MenuOpened = !MenuOpened;

            this.Menu.GameObject().SetActive(MenuOpened);
        }
    }

    public void OnPause(InputAction.CallbackContext action)
    {
        if (action.ReadValue<float>() > 0)
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0 : 1f;
        }
    }

    public void Start()
    {
        Instance = this;
    }
}