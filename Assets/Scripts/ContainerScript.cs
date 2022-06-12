using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    private ContainerRowScript rowScript;
    private ContainerSpotScript spotScript;

    private Vector3 startPosition;

    //Setters
    public void SetSpotScript(ContainerSpotScript css) { spotScript = css; }
    public void SetRowScript(ContainerRowScript crs) { rowScript = crs; }
    public void SetStartPosition() { startPosition = transform.position; }

    /// <summary>
    /// Вызов метода создания нового контейнера
    /// </summary>
    public void CreateContainer() { rowScript.CreateContainer(spotScript); }

    /// <summary>
    /// Вызов метода удаления контейнера
    /// </summary>
    public void RemoveContainer() { rowScript.RemoveContainer(spotScript); }

    /// <summary>
    /// Вызов метода поднятия ряда контйенеров
    /// </summary>
    public void RaiseContainers(List<GameObject> containersRows) { rowScript.RaiseContainersRow(containersRows); }

    /// <summary>
    /// Вызов метода опускания ряда контйенеров
    /// </summary>
    public void DropContainers() { rowScript.DropContainersRow(); }


    /// <summary>
    /// Корутина поднятия контейнера
    /// </summary>
    /// <param name="easing">Кривая анимации</param>
    /// <param name="targetPosition">Точка, на которую необходимо поднять контейнер</param>
    /// <returns></returns>
    public IEnumerator Raise(AnimationCurve easing, float animationTime, Vector3 targetPosition) 
    {
        for (float i = 0; i < 1; i += Time.deltaTime / animationTime) 
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = targetPosition; 
    }

    /// <summary>
    /// Корутина опускания контейнера
    /// </summary>
    /// <param name="easing">Кривая анимации</param>
    /// <param name="currentPos">Точка, в которой находился контейнер в момент вызова корутины</param>
    /// <returns></returns>
    public IEnumerator Drop(AnimationCurve easing, float animationTime, Vector3 currentPos)
    {
        for (float i = 0; i < 1; i += Time.deltaTime / animationTime)
        {
            transform.position = Vector3.Lerp(currentPos, startPosition, easing.Evaluate(i));
            yield return null;
        }
        transform.position = startPosition;
    }

    /// <summary>
    /// Проверяет находится ли контейнер на начальной позиции
    /// </summary>
    /// <returns>Возвражает true когда контейнер находится в начальной позиции</returns>
    public bool OnPlace() { return transform.position == startPosition; }

}
