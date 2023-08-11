using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{

    public bool cardInSlot = false;
    public bool isZoomed = false;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SizeUpCard(Transform tran){
        if(!isZoomed){
            tran.localScale += scaleChange;
            isZoomed = true;
        }
    }

    public void SizeDownCard(Transform tran){
        if(isZoomed){
            tran.localScale -= scaleChange;
            isZoomed = false;
        }
    }

        public void SizeDownCardOutside(Transform tran){
        if(isZoomed){
            tran.localScale -= scaleChange*0.2f;
            isZoomed = false;
        }
    }


}
