using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : ScriptableObject
{

    /// <summary>
    /// The amount this resource starts off with.
    /// </summary>
    [SerializeField]
    public int startAmount;
	
}

[CreateAssetMenu(fileName = "Wood Resource", menuName = "Resource Types/Wood", order = 10)]
public class WoodResource : Resource
{

}

[CreateAssetMenu(fileName = "Rock Resource", menuName = "Resource Types/Rock", order = 11)]
public class RockResource : Resource
{

}

[CreateAssetMenu(fileName = "Human Resource", menuName = "Resource Types/Human", order = 12)]
public class HumanResource : Resource
{
    public HumanResource()
    {
        startAmount = 1;
    }
}

[CreateAssetMenu(fileName = "Can", menuName = "Resource Types/Can", order = 13)]
public class Can: Resource
{
    public Can()
    {
        startAmount = 1;
    }
}

[CreateAssetMenu(fileName = "Food", menuName = "Resource Types/Food", order = 14)]
public class FoodResource : Resource
{
    
}
