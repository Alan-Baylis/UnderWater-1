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
		foreach(GameObject cube in GetDiagonalNeighbors(grid[2,1])) {
			cube.transform.position += new Vector3(0, 10, 0);
		}
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
		List<GameObject> returnVal = new List<GameObject>();
		Vector2 gridPos = FindCubePosition(cubeToCheck);
		int x = Mathf.RoundToInt(gridPos.x);
		int y = Mathf.RoundToInt(gridPos.y);
		if(x > 0 && CubeExistsAt(x - 1, y)) {
			returnVal.Add(grid[x - 1, y]);
		}
		if(x < grid.GetLength(0) - 2 && CubeExistsAt(x + 1, y)) {
			returnVal.Add(grid[x + 1, y] );
		}
		if(y > 0 && CubeExistsAt(x, y -1)) {
			returnVal.Add(grid[x, y - 1]);
		}
		if(y < grid.GetLength(1) - 2 && CubeExistsAt(x, y + 1)) {
			returnVal.Add(grid[x, y + 1]);
		}
		return returnVal.ToArray();
	}

	GameObject[] GetDiagonalNeighbors(GameObject cubeToCheck) {
		List<GameObject> returnVal = new List<GameObject>();
		Vector2 gridPos = FindCubePosition(cubeToCheck);
		int x = Mathf.RoundToInt(gridPos.x);
		int y = Mathf.RoundToInt(gridPos.y);
		if(x > 0) {
			if(y > 0 && CubeExistsAt(x - 1, y - 1)){ 
				returnVal.Add(grid[x - 1, y - 1]);
				}
			if(y < grid.GetLength(1) - 2 && CubeExistsAt(x - 1, y + 1)) {
				returnVal.Add(grid[x - 1, y + 1]);
			}
		}
		if(x < grid.GetLength(0) - 2) {
			if(y > 0 && CubeExistsAt(x + 1, y - 1)){ 
				returnVal.Add(grid[x + 1, y - 1]);
				}
			if(y < grid.GetLength(1) - 2 && CubeExistsAt(x + 1, y + 1)) {
				returnVal.Add(grid[x + 1, y + 1]);
			}
		}
		return returnVal.ToArray();
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

	Vector2 FindCubePosition(GameObject cube) {
		for(int i = 0; i < grid.GetLength(0); i++) {
			for(int k = 0; k < grid.GetLength(1); k++) {
				if(grid[i, k].Equals(cube)) {
					return new Vector2(i, k);
				}
			}
		}

		return new Vector2(-1, -1);
	}

	bool CubeExistsAt(int x, int y) {
		return grid[x, y] != null;
	}

	bool CubeExistsAt(Vector2 pos) {
		return grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] != null;
	}
}
