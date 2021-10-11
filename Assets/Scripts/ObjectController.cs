using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private XRRayInteractor right, left;
    [SerializeField] private LayerMask huge, tiny, normal;
    [SerializeField] private TextMeshProUGUI text;

    private LayerMask _currentMask;
    private LayerMask _tinyMode, _normalMode, _hugeMode;
    private List<LayerMask> layerList = new List<LayerMask>();
    private List<LayerMask> modeList = new List<LayerMask>();

    private void Start()
    {
        _tinyMode = tiny;
        _normalMode = tiny + normal;
        _hugeMode = normal + huge;

        FillListOfModes();
        FillListOfLayerMasks();

        ApplyLayerMask(_normalMode);
    }

    private void FillListOfLayerMasks()
    {
        modeList.Add(_tinyMode);
        modeList.Add(_normalMode);
        modeList.Add(_hugeMode);
    }

    private void FillListOfModes()
    {
        layerList.Add(tiny);
        layerList.Add(normal);
        layerList.Add(huge);
    }

    public void ChangeMode()
    {
        var next = FindNextModeIndex();

        ApplyLayerMask(modeList[next]);
        ApplyText(next);
    }

    private int FindNextModeIndex()
    {
        var next = modeList.IndexOf(_currentMask) + 1;
        next = next >= modeList.Count ? 0 : next;

        return next;
    }

    private void ApplyLayerMask(LayerMask curMask)
    {
        _currentMask = curMask;
        right.interactionLayerMask = _currentMask;
        left.interactionLayerMask = _currentMask;
    }

    private void ApplyText(int index)
    {
        text.text = $"Mode:\n{LayerMask.LayerToName((int) Mathf.Log(layerList[index], 2))}";
    }
}