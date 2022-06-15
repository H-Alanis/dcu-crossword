using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GMScript : MonoBehaviour
{

	public string[] words = Array.Empty<string>();
	public KeyCode AcceptKey = KeyCode.Return;

	public static string currentWord = string.Empty;
	public Transform preview;
	public List<Transform> TargetLetters = new();

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(AcceptKey))
		{
			var match = words.Where(w => w.Equals(currentWord, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
			if (match != default)
			{
				// Word matched a desired word.
				foreach (var (letter, target) in match.Zip(TargetLetters, (letter, target) => (letter.ToString(), target)))
				{
					// Render the word in the target possion
					target.GetComponent<TextMesh>().text = letter;
				}
			}
			
			// If it doesn't match a word, reset
			// If word matched, also reset
			currentWord = string.Empty;
		}
		else if (Input.anyKeyDown)
		{
			foreach (var pressed in Input.inputString)
			{
				currentWord = pressed switch
				{
					'\b' when (currentWord.Length > 0) => currentWord.Remove(currentWord.Length -1),
					'\n' => currentWord,
					'\r' => currentWord,
					_ => currentWord += pressed
				};
			}
		}

		// Show the current spelled word
		preview.GetComponent<TextMesh>().text = currentWord;
	}
}
