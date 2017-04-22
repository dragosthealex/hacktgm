﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public static int HEIGHT = 6;
	public static int WIDTH = 32;

	public Cell cellPrefab;

	public List<List<Cell>> cells;


	public void Awake() {
		cells = new List<List<Cell>>();
	}

	// Generates chunk with starts
	public void generate(List<int> starts) {
		for (int i = 0; i < HEIGHT; i++) {
			cells.Add(new List<Cell>());
			for (int j = 0; j < WIDTH; j++) {
				cells [i].Add(Instantiate (cellPrefab));
				cells [i] [j].transform.parent = gameObject.transform;
				cells [i] [j].transform.localPosition = new Vector3 (2 * j, 4 * i, 0);
				if (Random.Range (0, 100) > 20) {
					cells [i] [j].setType ("platform");
				}
			}
		}
	}
}