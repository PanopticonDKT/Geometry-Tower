using UnityEngine;
using System.Collections;

public class BildBtn : MonoBehaviour
{
    public GameObject Bild;
    [Header("Retraction")]
    bool a = true;
    float speed = 0.1f;
    public float time = 0.1f;
    public GameObject Pnl;


    public void BildVdBtn()
    {
        Instantiate(Bild, Vector3.zero, Quaternion.identity);
    }
    public void Retraction()
    {
        if(a){
            StartCoroutine(RetractIn());
            a = false;
        }else{
            StartCoroutine(RetractOn());
            a = true;
        }

    }


     IEnumerator RetractIn(){
        for (int i = 0; i < 20; i++) {
            Pnl.transform.position -= Vector3.right*0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator RetractOn(){
        for (int i = 0; i < 20; i++) {
            Pnl.transform.position += Vector3.right*0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
