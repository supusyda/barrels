using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType2
{
    None,
    Wall,
    Normal,
    Special,

    barrel,
    Special2,
    Special3,
    Special4,


}
[CreateAssetMenu(fileName = "BoxType2", menuName = "BoxType2/Box", order = 0)]

public class BoxType2SO : ScriptableObject
{
    // Start is called before the first frame update
    public BoxType2 boxType;
    public Sprite tileSprite;
    protected Transform transform;
    public virtual void Update()
    {

    }
    public virtual void Init(Transform transform)
    {
        this.transform = transform;
    }
    public virtual void OnDisable()
    {

    }
}
