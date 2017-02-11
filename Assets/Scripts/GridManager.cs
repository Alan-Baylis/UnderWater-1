using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType {Sine};
public class GridManager : MonoBehaviour {

	List<GameObject> cubePool; // For object pooling down the line
	public TextAsset levelMap;
	GameObject[,] grid;

	public delegate void CubeTransform();
	CubeTransform cubeTransformationFunction;
	public bool infinteScrolling; // Not actually handled yet
	// Use this for initialization
	void Start () {
		CreateGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateGrid() {
		if(levelMap != null) {
			string temp = levelMap.text;
			string[] lines = temp.Split('\n');

			GameObject tempBlock = Resources.Load<GameObject>("GroundHiddenWave");

			SetGridDimensions(lines);
			for(int i = 0; i < lines.Length; i++) {
				char[] blocks = lines[i].ToCharArray();
				for(int k = 0; k < blocks.Length; k++) {
					int xPos = Mathf.RoundToInt(k / 2);
					k++;
					tempBlock = ParseBlockType(blocks[k]);
					if(tempBlock != null) {
						grid[xPos, i] = (GameObject)Instantiate(tempBlock, new Vector3(xPos, float.Parse(blocks[k].ToString()) * 0.7f, i), Quaternion.identity);
					}
				}
			}
		}

		for(int i = 0; i < 100; i++) {
			
		}
	}

	void SetGridDimensions(string[] lines) {
		int height = lines.Length;
		int width = 0;
		foreach(string line in lines) {
			if(Mathf.RoundToInt(line.Length / 2) > width) {
				width = Mathf.RoundToInt(line.Length / 2);
			}
		}

		grid = new GameObject[width,height];
	}

	void ExpandGrid() {
		// Used for inifinite mode
	}

	GameObject[] GetOrthogonalNeighbors(GameObject cubeToCheck) {
		GameObject[] returnVal;
		return new GameObject[] {};
	}

	GameObject[] GetDiagonalNeighbors(Transform cube) {
		return new GameObject[] {};
	}

	GameObject[] GetAllNeighbors(Transform cube) {
		return new GameObject[] {};
	}

	void StartWaterWave(int height) {
		
	}

	void StartSeismicWave(int x, int y) {
		
	}

	GameObject ParseBlockType(char type) {
		switch(type) {
			case 'x':
			return Resources.Load<GameObject>("GroundHiddenWave");
		default:
			return Resources.Load<GameObject>("GroundHiddenWave");
		} ;
	}
}
