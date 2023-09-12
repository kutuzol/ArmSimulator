using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    // скрипт прикрепляется к кисти

    // публичный массив пальцев кисти, для более простого их определения
    public Transform[] Fingers;
    // переменные отвечающие за вращение пальцев
    Vector3 openRot1, openRot2;
    // переменные уведомляющие о следующем: сжимается ли кисть и держит ли 
    // какой-нибудь объект
    public bool _isClench = false;
    public bool _isHold = false;

    // Для симуляции хватки rotation пальцев меняется только по оси Z
    // Для Finger[0]: rotation увеличивается от -150 to -42
    // Для Finger[1]: rotation уменьшается от 148.95 to 41
    // данные вычисленны экспериментально
    void Start()
    {
        // запоминаем начальное вращение каждого пальца
        openRot1 = Fingers[0].rotation.eulerAngles;
        openRot2 = Fingers[1].rotation.eulerAngles;

        // Особености местных эйлеровых углов
        openRot1.z -= 180;
        openRot2.z -= 180;
    }

    void Update()
    {
        // Если есть нажатие на экран, то пальцы сжимаются
        if(Input.touchCount > 0){
            // если рука ничего не держит и при этом палец лежит на экране
            if(Input.GetTouch(0).phase == TouchPhase.Stationary && !_isHold){
                // то если вращение каждого из пальцев возможно
                // меняется их вращение по оси z
                if (openRot1.z > -151 && openRot1.z < -42){
                    openRot1.z += 80 * Time.deltaTime;
                }
                if (openRot2.z > 41 && openRot2.z < 150){
                    openRot2.z -= 80 * Time.deltaTime;
                } 
                // устанавливается флаг о сжатии руки
                _isClench = true;         
            }
            // если рука что-то держит, то для более реалистичного удержания
            // ставятся следующие углы вращения пальцев по оси z
            if(_isHold)
            {
                openRot1.z = -130;
                openRot2.z = 130;
            }
        } // иначе пальцы разжимаются
        else{
            _isClench = false;
            if (openRot1.z > -149){
                openRot1.z -= 90 * Time.deltaTime;
            }
            if (openRot2.z < 148){
                openRot2.z += 90 * Time.deltaTime;
            }   
        }
        // устанавливаем изменившеесе вращение пальцев
        Fingers[0].localRotation = Quaternion.Euler(openRot1);
        Fingers[1].localRotation = Quaternion.Euler(openRot2);    
    }
    

}
