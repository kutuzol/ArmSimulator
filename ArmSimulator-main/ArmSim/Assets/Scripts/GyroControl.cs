using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    // скрипт прикреплен к руке
    // создаем переменную, отвечающую за присутствие гироскопа на устройстве
    private bool _IsGyro;
    // переменная, которая будет захватывать все входные данные гироскопа
    private Gyroscope _gyro;

    private void Start() {
        // Запускаем функцию проверки присутствия гироскопа на устройстве
        _IsGyro = EnabledGyroscope();
    }

    private void Update() {
       // в случае если на устройстве есть гироскоп, изменяем вращение
       // руки в соответствии с изменеием вращения устройства
        if(_IsGyro){
            // так как координаты в игровом пространстве и координаты
            // передаваемые гироскопом разные, то приводим данные гироскопа
            // к корректным для управления рукой
            Quaternion deviceRotation = ReadGyroscopeRotation();
            // устанавливаем вращению руки вращение телефона
            transform.rotation = deviceRotation;
        }
    }
    
    // функция доступности гироскопа
    private bool EnabledGyroscope(){
        // обращаемся к системным характеристикам мобильного устройства,
        // и если на мобильом устройстве есть гироскоп, то
        // устанавливаем его состояние, как включенное, и передаем в
        // переменную ссылку на него для более удобного считывания данных
        if(SystemInfo.supportsGyroscope){
            _gyro = Input.gyro;
            _gyro.enabled = true;
            
            return true;
        }   
        // иначе функция говорит о том, что на устройстве гироскоп отсутствует
        else return false;
    }

    // функция переводящая считанные данные гироскопа в данные корректные
    // для левосторонней системы координат в Unity
    private Quaternion ReadGyroscopeRotation()
    {
        return new Quaternion(0.5f,0.5f,-0.5f,0.5f) * Input.gyro.attitude * new Quaternion(0,0,1,0);
    }

}

