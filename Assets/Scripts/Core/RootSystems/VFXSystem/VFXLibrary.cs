using System.Collections.Generic;using PlasticPipe.Server;
using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.CoreData.Vfx + "Library")]
public class VFXLibrary : ScriptableObject
{
    [SerializeField] private List<VFXData> _vfxDataCollection;
    
    public List<VFXData> VFXDataCollection => _vfxDataCollection;
    
    public void Initialize(List<VFXData> list)
    {
        _vfxDataCollection = list;
    }
}