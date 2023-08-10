using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ScaleOnPoint : MonoBehaviour
{
    GameObject Card;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);


    // Start is called before the first frame update
    void Start()
    {
    }

    private void onHover(Collider collider1){
        PointableUnityEventWrapper wrapper = collider1.GetComponent <PointableUnityEventWrapper>();
        // wrapper.WhenHover.AddListener(SizeUpCard);
        // wrapper.WhenUnhover.RemoveListener(ReduceCard);
    }

    private void SizeUpCard(Transform tran){
        tran.localScale += scaleChange;
    }

    private void SizeDownCard(Transform tran){
        tran.localScale -= scaleChange;
    }

    void OnTriggerEnter(Collider trigCol){
        if(trigCol.GetComponent<CardBehaviour>() != null && trigCol.gameObject.name == "Card" && trigCol.GetComponent<CardBehaviour>().cardInSlot){
            SizeUpCard(trigCol.transform);
        }
    }

        void OnTriggerExit(Collider trigCol){
        if(trigCol.GetComponent<CardBehaviour>() != null && trigCol.gameObject.name == "Card" && trigCol.GetComponent<CardBehaviour>().cardInSlot){
            SizeDownCard(trigCol.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
