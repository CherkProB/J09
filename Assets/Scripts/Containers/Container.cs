using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    //Точка, в которой находится контейнер, когда он на земле
    private Vector3 startPosition;
    //Позиция контйенера в ряду, секции и в высоту
    private Vector3Int position;
    //Информация о анимации
    private AnimationCurve easing;
    private float animationTime;
    //
    private Outline outline;

    //Свойства
    public Vector3 StartPosition { get { return startPosition; } set { startPosition = value; } }
    public Vector3Int Position { get { return position; } set { position = value; } }
    public AnimationCurve Easing { set { easing = value; } }
    public float AnimationTime { set { animationTime = value; } }
    public Outline Outline { get { return outline; } set { outline = value; } }

    /// <summary>
    /// Корутина поднимания контйенера в воздух
    /// </summary>
    /// <param name="containerSize">Размеры контейнеров, для определения точки, на которую необходимо поднять</param>
    /// <param name="maxHeight">Максимальное количество контейнеров в высоту, для того чтобы поднять над всем контейнерами</param>
    /// <returns></returns>
    public IEnumerator Raise(Vector3Int containerSize, int maxHeight) 
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y =  containerSize.y * (maxHeight + 1) + containerSize.y * 2 * position.y;

        Vector3 activePosition = transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / animationTime) 
        {
            transform.position = Vector3.Lerp(activePosition, targetPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = targetPosition;
    }

    /// <summary>
    /// Опускание контйенера, с точки в которой он сейчас находится
    /// В начальную точку
    /// </summary>
    /// <returns></returns>
    public IEnumerator Drop() 
    {
        if (!this) Debug.Log("sss");

        Vector3 activePosition = transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / animationTime) 
        {
            transform.position = Vector3.Lerp(activePosition, startPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = startPosition;
    }

    private void OnMouseEnter() => outline.enabled = true;
    private void OnMouseExit() => outline.enabled = false;
}
