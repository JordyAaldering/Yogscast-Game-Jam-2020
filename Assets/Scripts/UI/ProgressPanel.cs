using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ProgressPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText;

	private Dictionary<string, Tuple<int, int>> progress;

	private void Start()
	{
		PopulateProgress();
		UpdateText();
	}

	private void PopulateProgress()
	{
		progress = new Dictionary<string, Tuple<int, int>>();
		foreach (Table t in FindObjectsOfType<Table>()) {
			if (progress.TryGetValue(t.tableName, out var val)) {
				progress[t.tableName] = new Tuple<int, int>(
					val.Item1 + (t.IsBought ? 1 : 0), val.Item2 + 1);
			} else {
				progress.Add(t.tableName, new Tuple<int, int>(t.IsBought ? 1 : 0, 1));
			}
		}
	}

	public void IncreaseCounter(string key)
	{
		progress[key] = new Tuple<int, int>(
			progress[key].Item1 + 1, progress[key].Item2);
		UpdateText();
	}

	private void UpdateText()
	{
		progressText.text = "";
		foreach (var pair in progress) {
			progressText.text += $"{pair.Key}: {pair.Value.Item1}/{pair.Value.Item2}\n";
		}
	}
}
