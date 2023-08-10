using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ScaleOnPoint : MonoBehaviour
{
    GameObject Card;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);
    Vector3 initialScale = new Vector3(0.05f,0.1f,0.015f);
    bool sizedUp = false;


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

        private void SizeOriginalCard(Transform tran){
        tran.localScale = initialScale;
    }

    void OnTriggerEnter(Collider trigCol){
        if(trigCol.GetComponent<CardBehaviour>() != null && trigCol.gameObject.name == "Card" && trigCol.GetComponent<CardBehaviour>().cardInSlot){
            SizeUpCard(trigCol.transform);
            sizedUp = true;
        }
    }

        void OnTriggerExit(Collider trigCol){
        if(trigCol.GetComponent<CardBehaviour>() != null && trigCol.gameObject.name == "Card" && trigCol.GetComponent<CardBehaviour>().cardInSlot){
            SizeDownCard(trigCol.transform);
            sizedUp = false;

        }
        if(Card.GetComponent<CardBehaviour>() != null && !Card.GetComponent<CardBehaviour>().cardInSlot){
            SizeOriginalCard(trigCol.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
