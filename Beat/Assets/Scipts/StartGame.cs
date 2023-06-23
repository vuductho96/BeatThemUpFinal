using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private string sceneName;
    public TextMeshProUGUI textMeshPro;
    public Image image;

    private void Start()
    {
        StartBlink();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadScene();
        }
    }

    private void StartBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            textMeshPro.enabled = !textMeshPro.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void LoadScene()
    {
        textMeshPro.enabled = false;
        image.enabled = true;
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        SceneManager.LoadScene("MainScene");
    }
}
