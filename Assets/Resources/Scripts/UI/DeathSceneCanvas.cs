using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSceneCanvas : MonoBehaviour
{
    [SerializeField]
    private Button caveButton;

    private void Awake()
    {
        caveButton.onClick.AddListener(OnCaveButtonClick);
    }

    private void OnCaveButtonClick()
    {
        GoToCave();
    }

    private void GoToCave()
    {
        SceneManager.LoadScene("GameScene");
    }

}
