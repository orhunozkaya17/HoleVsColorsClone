using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //singleton level
    public static Level instance;
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
    public int objectsInScene, totalObject;

    [SerializeField] Transform objectsParent;
    [SerializeField] ParticleSystem winFX;

    [SerializeField] Material groundMaterail;
    [SerializeField] Material objectMaterail;
    [SerializeField] Material obstacleMaterail;
    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] SpriteRenderer bgFadeSprite;
    [SerializeField] Image progressBar;

    [Header("Level collor")]
    [SerializeField] Color groundColor;
    [SerializeField] Color borderColor;
    [SerializeField] Color sideColor;



    [SerializeField] Color objectColor;
    [SerializeField] Color obtacleColor;
    
    
    
    [SerializeField] Color progressFillColor;
    
    
    [SerializeField] Color cameraBG;
    [SerializeField] Color fadeColor;
    
    
  
    void Start()
    {
        CountObjects();
        updateLevelColors();
    }
    

    // Update is called once per frame
    void CountObjects()
    {
        totalObject = objectsParent.childCount;
        objectsInScene = totalObject;
        UIManager.instance.SetProgressText();
    }
    public void LoadNextlevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
    public void PlayWinFx()
    {
        winFX.Play();
    }
    public void Resetlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void updateLevelColors()
    {
        groundMaterail.color = groundColor;
        objectMaterail.color = objectColor;
        obstacleMaterail.color = obtacleColor;
        groundBorderSprite.color = borderColor;
        groundSideSprite.color = sideColor;
        bgFadeSprite.color = fadeColor;
        progressBar.color = progressFillColor;
        Camera.main.backgroundColor = cameraBG;
    }
    private void OnValidate()
    {
        updateLevelColors();
    }
}
