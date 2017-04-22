using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public static int HEIGHT = 8;
	public static int WIDTH = 16;

	public Cell[][] cells;

	public void Awake() {
		cells = new Cell[HEIGHT] [WIDTH];
		for (int i = 0; i < HEIGHT; i++) {
			for (int j = 0; j < WIDTH; j++) {
			}
		}
	}
}
