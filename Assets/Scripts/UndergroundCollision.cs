using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameover)
        {
            if (other.CompareTag("Object"))
            {
                Destroy(other.gameObject);
                Level.instance.objectsInScene--;
                UIManager.instance.UpdateLevelProgress();
                Magnet.instance.RemoveFromMagnetField(other.attachedRigidbody);
                if (Level.instance.objectsInScene==0)
                {
                    Game.isGameover = true;
                    UIManager.instance.ShowCompletedText();
                    Level.instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                }

            }
            if (other.CompareTag("Obstacle"))
            {
                Game.isGameover = true;
               
                Camera.main.transform.DOShakePosition(1f, .2f, 20, 90).OnComplete(() =>
                   {
                       Level.instance.Resetlevel();
                   });
            }

        }
      
    }
    
    void NestlevelGame()
    {
        Level.instance.LoadNextlevel();
    }
}
