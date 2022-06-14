using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Размеры контейнера
    private const int containerLength = 2;
    private const int containerWidth = 1;
    private const int containerHeight = 1;

    //Точка создания уровня
    [SerializeField] private GameObject spawnPoint;

    //Префабы объектов
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject containerSpot;
    [SerializeField] private GameObject ground;

    //Кривая анимации
    [SerializeField] private AnimationCurve animEasing;

    //Время анимации
    [SerializeField] private float animTime;

    //Список рядов контейнеров
    private List<GameObject> containersRows = new List<GameObject>();

    //Размеры поля
    private int length;
    private int width;
    private int height;

    //Зазоры поля
    private float lengthGap;
    private float widthGap;

    private void Update()
    {
        //Находится ли пользователь на сцена
        if (!GetComponent<HubManager>().GetOnScene()) return;

        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        //Создание контейнера
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "container":
                    hit.transform.gameObject.GetComponent<ContainerScript>().CreateContainer();
                    break;
                case "spot":
                    hit.transform.gameObject.GetComponent<ContainerSpotScript>().CreateContainer();
                    break;
            }
        }

        //Удаление контейнера
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "container":
                    hit.transform.gameObject.GetComponent<ContainerScript>().RemoveContainer();
                    break;
                case "spot":
                    hit.transform.gameObject.GetComponent<ContainerSpotScript>().RemoveContainer();
                    break;
            }

        }

        //Поднятие ряда контейнеров
        if (Input.GetKeyDown(KeyCode.W) && Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "container":
                    hit.transform.gameObject.GetComponent<ContainerScript>().RaiseContainers(containersRows);
                    break;
                case "spot":
                    hit.transform.gameObject.GetComponent<ContainerSpotScript>().RaiseContainers(containersRows);
                    break;
            }
        }

        //Опускание ряда контейнеров
        if (Input.GetKeyDown(KeyCode.S) && Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "container":
                    hit.transform.gameObject.GetComponent<ContainerScript>().DropContainers();
                    break;
                case "spot":
                    hit.transform.gameObject.GetComponent<ContainerSpotScript>().DropContainers();
                    break;
            }
        }

        //Панелька
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "container") 
        {
            ContainerScript cs = hit.transform.gameObject.GetComponent<ContainerScript>();

            string newText = "L: " + cs.GetPositionInStack().x + '\n';
            newText += "W: " + cs.GetPositionInStack().z + '\n';
            newText += "H: " + cs.GetPositionInStack().y;
            GetComponent<HubManager>().GetInfoPanel().SetInfo("Контейнер", newText);
            GetComponent<HubManager>().GetInfoPanel().Enable(true, Input.mousePosition);
        }
        else
            GetComponent<HubManager>().GetInfoPanel().Enable(false, Vector3.zero);
    }

    /// <summary>
    /// Функция создания уровня
    /// </summary>
    public void CreateLevel()
    {
        for (int i = 0; i < length; i++)
        {
            //Создание пустышки в которой будет храниться ряд контейнеров
            GameObject emptyGameObject = new GameObject();
            Vector3 emptyGameObjectPos = new Vector3((lengthGap + containerLength) * i, 0, 0);
            GameObject newRow = Instantiate(emptyGameObject, emptyGameObjectPos, Quaternion.identity, spawnPoint.transform);
            Destroy(emptyGameObject);

            //Добавление всех необходимых компонентов на пустышку
            newRow.AddComponent<ContainerRowScript>();
            newRow.GetComponent<ContainerRowScript>().SetContainerPrefab(container);
            newRow.GetComponent<ContainerRowScript>().SetMaxHeight(height);
            newRow.GetComponent<ContainerRowScript>().SetContainerHeight(containerHeight);
            newRow.GetComponent<ContainerRowScript>().SetAnimEasing(animEasing);
            newRow.GetComponent<ContainerRowScript>().SetAnimTime(animTime);

            for (int j = 0; j < width; j++)
            {
                //Создание точки появления контейнера
                Vector3 containerSpotPos = new Vector3((lengthGap + containerLength) * i, 0.2f, (widthGap + containerWidth) * j);
                GameObject newContainerSpot = Instantiate(containerSpot, containerSpotPos, Quaternion.identity, newRow.transform);

                //Добавление всех необходимых компонентов на точку
                newContainerSpot.AddComponent<ContainerSpotScript>();
                newContainerSpot.GetComponent<ContainerSpotScript>().SetRowScript(newRow.GetComponent<ContainerRowScript>());
                newContainerSpot.GetComponent<ContainerSpotScript>().SetPositionInStack(new Vector3(i, 0, j));

                newRow.GetComponent<ContainerRowScript>().AddContainerSpot(newContainerSpot);
            }

            containersRows.Add(newRow);
        }

        //Настройка центра вращения у камеры
        float sceneCenterX = ((float)(length * (containerLength + lengthGap) - lengthGap) - containerLength) / 2;
        float sceneCenterY = 1f;
        float sceneCenterZ = ((float)(width * (containerWidth + widthGap) - widthGap) - containerWidth) / 2;

        Vector3 newCenter = new Vector3(sceneCenterX, sceneCenterY, sceneCenterZ);
        GetComponent<CameraController>().SetSceneCenter(newCenter);

        //Создание земли под точками появления контейнеров
        newCenter.y = 0.1f;
        GameObject newGround = Instantiate(ground, newCenter, Quaternion.identity, spawnPoint.transform);
        newGround.transform.localScale = new Vector3(newCenter.x * 2 + containerLength, newCenter.y, newCenter.z * 2 + containerWidth) * 100f;
    }

    /// <summary>
    /// Передача параметров о поле из HudManager в SceneManager
    /// </summary>
    /// <param name="length">Длина поля</param>
    /// <param name="width">Ширина поля</param>
    /// <param name="height">Высота поля</param>
    /// <param name="lengthGap">Зазор по длине</param>
    /// <param name="widthGap">Зазор по ширине</param>
    public void SetData(int length, int width, int height, float lengthGap, float widthGap)
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.lengthGap = lengthGap;
        this.widthGap = widthGap;
    }
}
