using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{

    public bool cardInSlot = false;
    public bool cardCanSnap = true;
    public bool isZoomed = false;

    public SnapToMe CurrentCardSlot = null;
    Vector3 scaleChange = new Vector3(0.4f,0.8f,0.12f);

    CharacterData _thisCardData;

    [SerializeField]
    TMP_Text _name;
    [SerializeField]
    TMP_Text _cost;
    [SerializeField]
    TMP_Text _power;
    [SerializeField]
    TMP_Text _health;
    [SerializeField]
    TMP_Text _description;
    [SerializeField]
    Image _image;
    Rigidbody rig = null;

    bool _shouldBeKinematic = false;
    private GameObject _hologramObject = null;

    public bool test = false;

    private static int _leftSatelliteCards, _wonderCards, _rightSatelliteCards = 0;

    private void Update()
    {
        if (rig != null)
            rig.isKinematic = _shouldBeKinematic;
        if(cardInSlot)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }

    public void ChangeKin(bool isKinematic){
        _shouldBeKinematic = isKinematic;
        gameObject.name = "test";
        rig = GetComponent<Rigidbody>();
        //rig.useGravity = false;
        rig.isKinematic = isKinematic;     
    }

    public void PopulateCard(CharacterData cardData)
    {
        _thisCardData = cardData;
        _name.text = _thisCardData.name.ToString();
        _cost.text = _thisCardData.mana.ToString();
        _power.text = _thisCardData.attack.ToString();
        _health.text = _thisCardData.health.ToString();
        _description.text = _thisCardData.description;
        _image.sprite = _thisCardData.artwork;
        _hologramObject = _thisCardData.hologram;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Location" && !cardInSlot)
        {
            if (other.gameObject.name == "Left satellite")
            {
                
                //GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                PositionCard(other.transform, _leftSatelliteCards);
                _leftSatelliteCards++;
            }
            else if (other.gameObject.name == "Right satellite")
            {
                
                //GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                PositionCard(other.transform, _rightSatelliteCards);
                _rightSatelliteCards++;
            }
            else if (other.gameObject.name == "Wonder")
            {
                
                //GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                PositionCard(other.transform, _wonderCards);
                _wonderCards++;
            }
        }
    }

    void PositionCard(Transform playZone, int currentCardCount)
    {
        Vector3 cardOffset = new Vector3(.15f, 0, 0);
        Vector3 firstcardSlot = (playZone.position - cardOffset * 2.5f + new Vector3(0, 0.1f, 0));
        transform.position = firstcardSlot + cardOffset * currentCardCount;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
