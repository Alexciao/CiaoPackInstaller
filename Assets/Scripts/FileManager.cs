using UnityEngine;
using System.Collections;
using System;
using SimpleFileBrowser;

public class FileManager : MonoBehaviour
{
	#region Singleton
	public static FileManager Instance;
    
	private void Awake() 
	{
		if (Instance != null && Instance != this) 
		{ 
			Destroy(this); 
		} 
		else 
		{ 
			Instance = this; 
		} 
	}
	#endregion
	
	public string PackDir;

	public void ChoosePack()
	{
		FileBrowser.SetFilters(false, new FileBrowser.Filter("Pack", ".json"));
		FileBrowser.SetDefaultFilter(".json");
		StartCoroutine(ShowLoadDialogCoroutine());
	}

	IEnumerator ShowLoadDialogCoroutine()
	{
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.Files, false, null, null, "Load a pack", "Load" );
		Debug.Log("Success: " + FileBrowser.Success);

		if(FileBrowser.Success)
		{
			PackDir = FileBrowser.Result[0];
			Debug.Log(PackDir);
			GameManager.instance.ContinueInstall();
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
		}
	}

	public string GetMinecraftDirectory()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsServer)
		{
			return "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\.minecraft";
		}
		else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXServer)
		{
			return "/Users/" + Environment.UserName + "/Library/Application Support/minecraft";
		}
		else if (Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxServer)
		{
			return "/home/" + Environment.UserName + "/.minecraft";
		}
		else
		{
			return null;
		}
	}
}