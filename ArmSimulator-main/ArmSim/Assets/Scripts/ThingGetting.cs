using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingGetting : MonoBehaviour
{
    // скрипт прикрепляется к интерактивным предметам, то есть к
    // префабу, на основе которого будут создавться все объекты

    // переменная ссылка на кисть
    private GameObject hand;
    void Start()
    {
        // находим на сцене кисть и прикрепляем ее к переменной hand
        hand = GameObject.Find("Wrist");
    }

    private void Update() {
        // объявляем переменную отвечающую за расстояние между кистью и объектом
        // и находим его как минимальное из расстояний: от кисти, от первого пальца
        // и от второго пальца
        float dist = Vector3.Distance(hand.transform.position, transform.position);
        dist = Mathf.Min(dist, Vector3.Distance(hand.transform.GetChild(0).GetChild(0).GetChild(0).position, transform.position));
        dist = Mathf.Min(dist, Vector3.Distance(hand.transform.GetChild(0).GetChild(1).GetChild(0).position, transform.position));
        
        // если рука находится вплотную к объекту и в этот момент она сжимается,
        // то есть готова взять объект, то устанавливаем кисть родителем этого объекта
        // а также меняем его позицию и вращение относительно руки, а еще в момент
        // удержания лишаем объект всех физических свойств
        if (dist <= 1.5f && hand.GetComponent<Holding>()._isClench && !hand.GetComponent<Holding>()._isHold)
        {
            hand.GetComponent<Holding>()._isHold = true;
            transform.SetParent(hand.transform);
            transform.localPosition = new Vector3(0f,1.3f,0f);
            transform.localRotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = true;
        } // иначе если рука ничего не удерживает возвращаем все свойства объекта к начальным
        else if(!hand.GetComponent<Holding>()._isClench){
            GetComponent<Rigidbody>().isKinematic = false;
            hand.GetComponent<Holding>()._isHold = false;
            transform.SetParent(null);
            
        }
    }
}
