
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JSONManager : MonoBehaviour
{
    #region Singleton
    public static JSONManager instance;
    
    private void Awake() 
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    #endregion
    
    public string packContent;
    public Pack pack = new Pack();
    
    public void LoadPack()
    {
        packContent = File.ReadAllText(FileManager.Instance.PackDir);
        pack = JsonConvert.DeserializeObject<Pack>(packContent);
    }
}
