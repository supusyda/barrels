using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    // Abstract method to get a product instance.
    private readonly Dictionary<KeyCode, ISelectableType> _typeMappings;

    // public BoxFactory(BoxType2SO normalBox, BoxType2SO specialBox, BoxType2SO wallBox, BoxType2SO noneBox, BoxType2SO barrelBox,
    //                   NodeType yesNode, NodeType noNode, NodeType cube)
    // {
    //     _typeMappings = new Dictionary<KeyCode, ISelectableType>
    //     {
    //         // { KeyCode.A, normalBox },
    //         // { KeyCode.S, specialBox },
    //         // { KeyCode.D, wallBox },
    //         // { KeyCode.F, noneBox },
    //         // { KeyCode.G, barrelBox },
    //         // { KeyCode.W, yesNode },
    //         // { KeyCode.E, noNode },
    //         // { KeyCode.R, cube }
    //     };
    // }

    public ISelectableType HandleInput()
    {
        foreach (var entry in _typeMappings)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                return entry.Value;
            }
        }

        return null; // No input detected
    }
}

public interface ISelectableType
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    public void Initialize();
}