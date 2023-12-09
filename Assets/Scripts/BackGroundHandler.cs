using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundHandler : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private Quaternion startPos = new Quaternion();
    private Vector3 finalPos = new Vector3(0, 0, 0);

    public IEnumerator RotateBackground()
    {
        startPos = background.transform.rotation;
        Quaternion qFinalPos = Quaternion.Euler(finalPos.x,finalPos.y,finalPos.z);
        yield return new WaitForSeconds(2f);
        
        while (background.transform.rotation != qFinalPos)
        {
            background.transform.rotation = Quaternion.Lerp(background.transform.rotation,qFinalPos,1f*Time.deltaTime);
            yield return null;
        }
        
        GetComponent<GameManager>().ChangeStateTo(GameState.LoadNextWave);
    }
}
