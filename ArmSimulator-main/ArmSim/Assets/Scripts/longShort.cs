using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class longShort : MonoBehaviour
{
    // скрипт прикреплен к части руки - предплечью (часть между кистью и шарниром)

    // Нужна ссылка на кисть, то есть GameObject Wrist
    private GameObject wrist = null;
    // Для отслеживания перемещения пальца на экране, нужно знать его
    // начальное положение
    private Vector2 _startPos = Vector2.zero;
    // И в переменной будем определять вверх или вниз производится смещение
    private float _direct = 0f;
    // Также переменная отвечающая за скорость сжатия и выдвижения
    private float _speed = 0f;
    // и переменная отвечающая за максимальное отклонение при котором считывается свайп
    private float _deadZone = 0f;

    private void Start() {
        // находим на сцене кисть и устанавливаем ссылку на нее в wrist
        // для дальнейших преобразований
        wrist = GameObject.Find("Wrist");
        // также устанавливаем начальные значения для скорости изменения
        // и мертвой зоны, в пределах которой проведение пальцем не будет считаться
        // свайпом
        _speed = 5f;
        _deadZone = 20f;
    }

    private void Update() {
        // Когда палец проводится вниз по экрану, то scale уменьшается
        // localPosition смещается вниз вдвое медленнее уменьшения scale
        // и localPosition wrist также смещается вниз, то есть по оси y
        
        // если рука что-то держит, то скорость изменения длины руки уменьшаем
        // и увеличиваем мертвую зону, так как в момент держания предметов
        // палец по управлению должен находиться на экране, а поэтому без изменений
        // вероятно ложное срабатывание изменеия руки
        if(wrist.GetComponent<Holding>()._isHold)
        {
            _speed = 2.5f;
            _deadZone = 40f;
        }
        else{   // иначе возвращаем значения в стандартное положение
            _speed = 5f;
            _deadZone = 20f;
        }
        // определяем, производится ли свайп, и в какую сторону, или нет
        GetSwapDirection(_deadZone);
        // если свайп произведен вниз по экрану, то укорачиваем руку
        if(_direct > 0)
        {
            if(transform.localScale.y > 0.8){
                transform.localScale -= new Vector3(0f, _speed * Time.deltaTime, 0f);
                transform.localPosition -= new Vector3(0f, _speed / 2 * Time.deltaTime, 0f);
                wrist.transform.localPosition -= new Vector3(0f, _speed * Time.deltaTime, 0f);
            }
        } // если свайп произведен вверх по экрану, то удлиняем руку
        if(_direct < 0)
        {
            if(transform.localScale.y < 7){
                transform.localScale += new Vector3(0f, _speed * Time.deltaTime, 0f);
                transform.localPosition += new Vector3(0f, _speed / 2 * Time.deltaTime, 0f);
                wrist.transform.localPosition += new Vector3(0f, _speed * Time.deltaTime, 0f);
            }
        }
        
    }

    // Распознавание вних или вверх проводится палец
    private void GetSwapDirection(float zone)
    {
        // если есть нажатия на экране
        if (Input.touchCount > 0)
        {
            // запоминаем начальное нажатие
            Touch touch = Input.GetTouch(0);
            // если статутс нажатия - проведение по экрану
            if(touch.phase == TouchPhase.Moved)
            {
                // то если смещения пальца вышло из мертвой зоны, то определяем
                // вверх или вниз сделан свайп
                if(Mathf.Abs(touch.position.y - _startPos.y) > zone)
                {
                    // так как в unity для Vector2 y идет сверху вниз, то
                    // исходя из этого делаем следующие проверки
                    if(touch.position.y < _startPos.y)
                    {
                        _direct = 1f;
                    }
                    else{
                        _direct = -1f;
                    }
                }
            }
            else{
                // иначе, если палец был убран с экрана или только прикоснулся, то
                // устанавоиваем новую начальную позицию пальца на экране и 
                // обнуляем направление
                _startPos = touch.position;
                _direct = 0f;
            }
        }
    }
}
