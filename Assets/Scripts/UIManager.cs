using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] int sceneOffset;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] Image progressFillImage;
    [SerializeField] TMP_Text completedText;

    [SerializeField] Image fadePanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    private void Start()
    {
        Game.isMoving = false;
        FadeToLevel();
    }
    public void SetProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
        UpdateLevelProgress();
    }
    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.instance.objectsInScene / Level.instance.totalObject);
        //  progressFillImage.fillAmount = val;
        progressFillImage.DOFillAmount(val, .4f);
    }
    public void ShowCompletedText()
    {
        completedText.gameObject.SetActive(true);
        completedText.transform.localScale = Vector3.zero;
        completedText.transform.DOScale(1, .4f).SetEase(Ease.OutBack);
        completedText.DOFade(1, .4f).From(0);
    }
    public void FadeToLevel()
    {
        fadePanel.DOFade(0, 2f).From(1).OnComplete(() =>
        {
            Game.isMoving = true;
        });
    }
}
