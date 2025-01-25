using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int lives;
    public float time;
    public float positionX;
    public float positionY;
    public float positionZ;
    public Vector3 GetPosition() { return new Vector3(positionX, positionY, positionZ); }
}