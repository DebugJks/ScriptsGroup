using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingManager : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Image loadingBar;
    [SerializeField] Text loadText;


    WaitForSeconds seconds = new WaitForSeconds(3f);

    void Start()
    {
        StartCoroutine(LoadTyping("로딩중 . . ."));
        StartCoroutine(LoadSceneProcess());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.2f)
            {
                loadingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.2f, 1f, timer);
                if(loadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    IEnumerator LoadTyping(string textString)
    {
        loadText.text = null;
        loadText.DOText(textString,1.5f);
        yield return seconds;
        StartCoroutine(LoadTyping("로딩중 . . ."));
    }

}
