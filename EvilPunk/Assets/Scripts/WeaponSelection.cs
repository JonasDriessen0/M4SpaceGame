using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public List<GameObject> weapons; // List of weapon prefabs
    public Transform weaponSpawnPoint; // The spawn point for the selected weapon

    private int currentWeaponIndex; // Index of the currently selected weapon
    private GameObject currentWeaponInstance; // Instance of the currently selected weapon

    private void Start()
    {
        currentWeaponIndex = 0;
        CreateWeaponInstance();
    }

    private void Update()
    {
        // Scroll through weapons using mouse scroll wheel or keyboard keys
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f)
        {
            SelectNextWeapon();
        }
        else if (scrollInput < 0f)
        {
            SelectPreviousWeapon();
        }

        // Disable/Enable weapon based on key press (for testing purposes)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleWeapon(2);
        }
        // Add more key codes as needed for additional weapons
    }

    private void SelectNextWeapon()
    {
        // Destroy the current weapon instance
        Destroy(currentWeaponInstance);

        // Increment the weapon index and wrap around to the start if needed
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Create a new instance of the selected weapon
        CreateWeaponInstance();
    }

    private void SelectPreviousWeapon()
    {
        // Destroy the current weapon instance
        Destroy(currentWeaponInstance);

        // Decrement the weapon index and wrap around to the end if needed
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Count) % weapons.Count;

        // Create a new instance of the selected weapon
        CreateWeaponInstance();
    }

    private void CreateWeaponInstance()
    {
        // Instantiate the selected weapon prefab at the spawn point
        currentWeaponInstance = Instantiate(weapons[currentWeaponIndex], weaponSpawnPoint.position, weaponSpawnPoint.rotation);
        currentWeaponInstance.transform.parent = weaponSpawnPoint;
        currentWeaponInstance.transform.localPosition = Vector3.zero;
        currentWeaponInstance.transform.localRotation = Quaternion.identity;
    }

    private void ToggleWeapon(int index)
    {
        // Check if the provided index is within the valid range
        if (index >= 0 && index < weapons.Count)
        {
            // Toggle the active state of the weapon prefab
            weapons[index].SetActive(!weapons[index].activeSelf);

            // If the current weapon is the one being toggled, destroy the instance
            if (index == currentWeaponIndex)
            {
                Destroy(currentWeaponInstance);

                // Find the next enabled weapon index and update the current weapon
                FindNextEnabledWeapon();

                // Create a new instance of the selected weapon
                CreateWeaponInstance();
            }
        }
    }

    private void FindNextEnabledWeapon()
    {
        int startIndex = currentWeaponIndex;
        do
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        }
        while (!weapons[currentWeaponIndex].activeSelf && currentWeaponIndex != startIndex);
    }
}
