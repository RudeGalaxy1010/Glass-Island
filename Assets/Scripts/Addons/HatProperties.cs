using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Hat Properties", menuName = "Custom/HatProperties", order = 0)]
public class HatProperties : ScriptableObject
{
    private const string SaveKey = "Hats";
    private const string CurrentHatIdSaveKey = "CurrentHat";

    public Hat[] Hats;
    public int CurrentHatId = 0;

    public Hat CurrentHat => Hats.First(h => h.Id == CurrentHatId);

    public Hat LastUnlockedHat => Hats.FirstOrDefault(h => h.IsUnlocked == false);
    public bool HasLockedHats => Hats.FirstOrDefault(h => h.IsUnlocked == false) != null;

    public void SelectHat(int id)
    {
        CurrentHatId = id;
        Save();
    }

    public void UnlockHat(int id)
    {
        Hat hat = Hats.First(h => h.Id == id);
        hat.IsUnlocked = true;
        Save();
    }

    public void Save()
    {
        if (Hats == null || Hats.Length == 0)
        {
            return;
        }

        for (int i = 1; i < Hats.Length; i++)
        {
            Hat hat = Hats.First(h => h.Id == i);
            PlayerPrefs.SetInt(SaveKey + i, Convert.ToInt32(hat.IsUnlocked));
        }

        PlayerPrefs.SetInt(CurrentHatIdSaveKey, CurrentHatId);
    }

    public void Load() 
    { 
        if (PlayerPrefs.HasKey(SaveKey + 0) == false)
        {
            Debug.LogWarning("Can't load hats!");
            return;
        }

        for (int i = 1; i < Hats.Length; i++)
        {
            Hat hat = Hats.First(h => h.Id == i);
            hat.IsUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt(SaveKey + i));
        }

        CurrentHatId = PlayerPrefs.GetInt(CurrentHatIdSaveKey);
    }

    public void ResetHats()
    {
        PlayerPrefs.DeleteKey(CurrentHatIdSaveKey);

        for (int i = 1; i < Hats.Length; i++)
        {
            PlayerPrefs.DeleteKey(SaveKey + i);
        }
    }
}
