using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LayerController : MonoBehaviour
{
    [SerializeField] private GameObject layerObject;
    private Queue<GameObject> layersStack = new Queue<GameObject>();
    private void Start()
    {
        Init();
        //DeactivateAllLayers();
        ActivateTopLayer();
        
    }
    private void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            var item = Instantiate(layerObject);
            item.name = $" Layer {i}";
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y + (40 * i), 0);
            item.transform.localScale = Vector3.one;
            item.transform.SetAsFirstSibling();
            layersStack.Enqueue(item);
        }
    }
    private void DeactivateAllLayers()
    {
        foreach (var layer in layersStack)
        {
            layer.SetActive(false);
        }
    }
    private void ActivateTopLayer()
    {
        if (layersStack.Count > 0)
        {
            GameObject topLayer = layersStack.Peek();
            topLayer.SetActive(true);
        }
        else
        {
            Debug.Log("All layers completed!");
        }
    }
}
