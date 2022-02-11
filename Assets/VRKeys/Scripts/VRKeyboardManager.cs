using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using VRKeys;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

public class VRKeyboardManager : MonoBehaviour
{
	/// <summary>
	/// Reference to the VRKeys keyboard.
	/// </summary>
	[SerializeField]
	private Keyboard keyboard;

	[SerializeField]
	private GameObject localVRPlayerCamera;

	[SerializeField]
	private Vector3 relativePosition = new Vector3(0,1,2);
	
	[SerializeField]
	private TMP_InputField playerNameInputField;

	[SerializeField]
	private GameObject leftBaseController;
	[SerializeField]
	private GameObject leftMarret;

	[SerializeField]
	private GameObject rightMarret;
	[SerializeField]
	private GameObject rightBaseController;

	[SerializeField]
	private bool _displayVRKeyboard;


    private void Start()
    {
		if (_displayVRKeyboard)
		{
			EnableVRKeyboard();
		}
		else
		{
			DisableVRKeyboard();
		}
	}

    /// <summary>
    /// Show the keyboard with a custom input message. Triggered through insepctor Unity event on LoginUI Prefab
    /// </summary>
    public void EnableVRKeyboard()
	{		
		keyboard.Enable();
		keyboard.SetPlaceholderMessage("What should we call you?");

		keyboard.OnUpdate.AddListener(HandleUpdate);
		keyboard.OnSubmit.AddListener(HandleSubmit);
		keyboard.OnCancel.AddListener(HandleCancel);

		keyboard.gameObject.transform.position = localVRPlayerCamera.transform.position + relativePosition;
		AttachMarrets();
		leftBaseController.GetComponent<XRRayInteractor>().enabled = false;
		rightBaseController.GetComponent<XRRayInteractor>().enabled = false;
	}

	private void AttachMarrets()
	{
		leftMarret.transform.SetParent(leftBaseController.transform);
		leftMarret.transform.localPosition = Vector3.zero;
		leftMarret.transform.localRotation = Quaternion.Euler(new Vector3(90f,0f,0f));
		leftMarret.SetActive(true);

		rightMarret.transform.SetParent(rightBaseController.transform);
		rightMarret.transform.localPosition = Vector3.zero;
		rightMarret.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		rightMarret.SetActive(true);
	}

	void DetachMarrets()
	{
		leftMarret.transform.SetParent(null);
		leftMarret.SetActive(false);

		rightMarret.transform.SetParent(null);
		rightMarret.SetActive(false);
	}

	public void DisableVRKeyboard() 
	{
		keyboard.OnUpdate.RemoveListener(HandleUpdate);
		keyboard.OnSubmit.RemoveListener(HandleSubmit);
		keyboard.OnCancel.RemoveListener(HandleCancel);

		keyboard.Disable();

		DetachMarrets();

		leftBaseController.GetComponent<XRRayInteractor>().enabled = true;
		rightBaseController.GetComponent<XRRayInteractor>().enabled = true;
	}

	public void HandleUpdate(string text)
	{
		keyboard.HideValidationMessage();
		playerNameInputField.text = text;
		playerNameInputField.caretPosition = playerNameInputField.text.Length;
	}

	/// <summary>
	/// Connect this to OnSubmit.
	/// </summary>
	public void HandleSubmit(string text)
	{
		DisableVRKeyboard();

		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}

	public void HandleCancel()
	{
		Debug.Log("Cancelled keyboard input!");
		DisableVRKeyboard();

		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}
}
