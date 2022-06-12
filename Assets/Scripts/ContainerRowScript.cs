using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerRowScript : MonoBehaviour
{
    private List<GameObject> containerSpots = new List<GameObject>();
    private GameObject containerPrefab;

    private int maxHeight;
    private int containerHeight;
    private AnimationCurve animEasing;
    private float animTime;

    //Setters
    public void SetContainerPrefab(GameObject prefab) { containerPrefab = prefab; }
    public void SetMaxHeight(int height) { maxHeight = height; }
    public void SetContainerHeight(int height) { containerHeight = height; }
    public void SetAnimEasing(AnimationCurve easing) { animEasing = easing; }
    public void SetAnimTime(float time) { animTime = time; }
    public void AddContainerSpot(GameObject containerSpot) { containerSpots.Add(containerSpot); }

    /// <summary>
    /// Проверяет находится ли ряд контейнеров на земле
    /// </summary>
    /// <returns>Возвращает true, если ряд контйенеров находится на земле</returns>
    public bool ContainersOnLand() 
    {
        for (int i = 0; i < containerSpots.Count; i++)
        {
            List<GameObject> activeContainers = containerSpots[i].GetComponent<ContainerSpotScript>().GetContainers();
            for (int j = 0; j < activeContainers.Count; j++)
                if(!activeContainers[j].GetComponent<ContainerScript>().OnPlace())
                    return false;
        }
        return true;
    }


    /// <summary>
    /// Создание нового контйенера в точке вызова
    /// </summary>
    /// <param name="css">Точка, в которой необходимо создать контейнер</param>
    public void CreateContainer(ContainerSpotScript css)
    {
        if (css.GetContainers().Count >= maxHeight) return;

        if (!ContainersOnLand()) DropContainersRow();

        GameObject newContainer = Instantiate(containerPrefab, css.transform.position, Quaternion.identity, css.transform);
        newContainer.transform.localScale = Vector3.one;

        newContainer.AddComponent<ContainerScript>();
        newContainer.GetComponent<ContainerScript>().SetSpotScript(css);
        newContainer.GetComponent<ContainerScript>().SetRowScript(this);
        newContainer.transform.position += Vector3.up * css.GetContainers().Count * containerHeight;
        newContainer.GetComponent<ContainerScript>().SetStartPosition();

        css.GetContainers().Add(newContainer);
    }

    /// <summary>
    /// Удаление контейнера из определенной точки
    /// </summary>
    /// <param name="css">Точка, в которой необходимо удалить контйенер</param>
    public void RemoveContainer(ContainerSpotScript css)
    {
        if (css.GetContainers().Count <= 0) return;

        GameObject container = css.GetContainers()[css.GetContainers().Count - 1];

        css.GetContainers().Remove(container);
        Destroy(container);
    }

    /// <summary>
    /// Функция поднятия ряда контейнеров
    /// </summary>
    /// <param name="containersRows">Список других рядов на сцене</param>
    public void RaiseContainersRow(List<GameObject> containersRows)
    {
        //Если контейнеры подняты их не нужно поднимать
        if (!ContainersOnLand()) return;

        //Проверка подняты ли контейнеры в других рядах
        //И если подняты то опустить их
        for (int i = 0; i < containersRows.Count; i++) 
        {
            if (containersRows[i] == gameObject) continue;
            if (!containersRows[i].GetComponent<ContainerRowScript>().ContainersOnLand()) 
                containersRows[i].GetComponent<ContainerRowScript>().DropContainersRow();
        }

        //Прерывание всех анимаций
        StopAllCoroutines();

        //Поднятие контейнеров
        for (int i = 0; i < containerSpots.Count; i++) 
        {
            List<GameObject> activeContainers = containerSpots[i].GetComponent<ContainerSpotScript>().GetContainers();
            for (int j = 0; j < activeContainers.Count; j++) 
            {
                Vector3 targetPos = activeContainers[j].transform.position;
                targetPos.y = containerHeight * (maxHeight + 1) + containerHeight * 2 * j;

                StartCoroutine(activeContainers[j].GetComponent<ContainerScript>().Raise(animEasing, animTime, targetPos));
            }
        }
    }

    /// <summary>
    /// Опускание ряда контейнеров
    /// </summary>
    public void DropContainersRow() 
    {
        //Прерывание всех анимаций
        StopAllCoroutines();

        for (int i = 0; i < containerSpots.Count; i++)
        {
            List<GameObject> activeContainers = containerSpots[i].GetComponent<ContainerSpotScript>().GetContainers();
            for (int j = 0; j < activeContainers.Count; j++)
                StartCoroutine(activeContainers[j].GetComponent<ContainerScript>().Drop(animEasing, animTime, activeContainers[j].transform.position));
        }
    }
}
