using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : System.ICloneable {
	public int y_corner_min, y_corner_max, x_corner_min, x_corner_max;
	public int size_x, size_y;
	public int entry_x, entry_y;
	public int exit_x, exit_y;

	public int x, y;

	public List<string> types;

	public double difficulty;

	public Block(int xmin, int xmax, int ymin, int ymax, int sizex, int sizey, int entx, int enty, int exx, int exy, double diff, List<string> typ){
		y_corner_min = ymin;
		y_corner_max = ymax;
		x_corner_min = xmin;
		x_corner_max = xmax;

		size_x = sizex;
		size_y = sizey;

		entry_x = entx;
		entry_y = enty;

		exit_x = exx;
		exit_y = exy;

		types = typ;

		difficulty = diff;
	}

	public object Clone()
    {
        return this.MemberwiseClone();
    }
}

public class Chunk : MonoBehaviour {

	public static int HEIGHT = 6;
	public static int WIDTH = 32;
	public static List<Block> blocktypes = (new List<Block>() 
				//new Block(min)
				{ new Block(0, WIDTH-3, 0, HEIGHT-1, 3, 1, 0, 0, 2, 0, 0.1, new List<string>() {"platform", "empty", "platform"}),
				  new Block(0, WIDTH-4, 0, HEIGHT-1, 4, 1, 0, 0, 3, 0, 0.15, new List<string>() {"platform", "empty", "empty", "platform"}),
				  new Block(0, WIDTH-5, 0, HEIGHT-1, 5, 1, 0, 0, 4, 0, 0.20, new List<string>() {"platform", "empty", "empty", "empty", "platform"}),
				  new Block(0, WIDTH-2, 0, HEIGHT-1, 2, 1, 0, 0, 1, 0, 0, new List<string>() {"platform", "platform"}),
				  new Block(0, WIDTH-2, 0, HEIGHT-2, 2, 2, 0, 0, 1, 1, 0.02, new List<string>() {"platform", "empty", "empty", "platform"}),
				  new Block(0, WIDTH-3, 0, HEIGHT-2, 3, 2, 0, 1, 2, 0, 0.12, new List<string>() {"empty", "empty", "platform", "platform", "empty", "empty"}),
				  new Block(0, WIDTH-7, 0, HEIGHT-2, 7, 2, 0, 1, 6, 0, 0.32, new List<string>() {"empty", "empty", "platform", "empty", "empty", "platform", "platform", 
				  																				"platform", "spikes", "empty", "spikes", "spikes", "spikes", "empty"}),
				  new Block(0, WIDTH-3, 0, HEIGHT-4, 3, 4, 0, 0, 3, 3, 0.22, new List<string>() {"platform", "empty", "empty",
				  																				"empty", "platform", "empty", 
				  																				"platform","empty", "empty",
				  																				"empty", "platform", "platform" }),
				  new Block(0, WIDTH-4, 0, HEIGHT-1, 4, 1, 0, 0, 4, 0, 0.15, new List<string>() {"platform","fallPlatform", "fallPlatform", "fallPlatform"}),
				  new Block(0, WIDTH-7, 0, HEIGHT-2, 7, 2, 0, 1, 6, 0, 0.30, new List<string>() {"empty", "empty", "platform", "empty", "empty", "fallPlatform", "platform", 
																								"platform", "spikes", "fallPlatform", "spikes", "spikes", "spikes", "empty"}),
				});

	public Cell cellPrefab;

	public List<List<Cell>> cells;

	public void Awake() {
		cells = new List<List<Cell>>();
	}

	// Generates chunk with starts
	public List<int> generate(List<int> starts) {
		double diflvl = 0.3;
		int nrexits=0;

		List<List<bool>> cellset = new List<List<bool>>();

		for (int i = 0; i < HEIGHT; i++) {
			cells.Add(new List<Cell>());
			cellset.Add(new List<bool>());
			for (int j = 0; j < WIDTH; j++) {
				cells [i].Add(Instantiate (cellPrefab));
				cells [i] [j].transform.parent = gameObject.transform;
				cells [i] [j].transform.localPosition = new Vector3 (2 * j, 4 * i, 0);

				cellset [i].Add(false);
			}
		}


		List<int> ends = new List<int>();
		
		List<int> startsx = new List<int>();
		for (int i=0; i<starts.Count; i++){
			startsx.Add(0);
		}

		List<Block> blocks = new List<Block>();
		for (int i=0; i<starts.Count; i++){

			int bindex;

			do{
				bindex = Random.Range(0, blocktypes.Count);
			}while (!(starts[i] - blocktypes[bindex].entry_y >= blocktypes[bindex].y_corner_min &&
					starts[i] - blocktypes[bindex].entry_y <= blocktypes[bindex].y_corner_max &&
				   startsx[i] - blocktypes[bindex].entry_x >= blocktypes[bindex].x_corner_min &&
				   startsx[i] - blocktypes[bindex].entry_x <= blocktypes[bindex].x_corner_max &&
				   startsx[i] - blocktypes[bindex].entry_x + blocktypes[bindex].exit_x <= WIDTH-1));

			double diffgood = System.Math.Abs(blocktypes[bindex].difficulty - diflvl);
			diffgood *= 3;
			//if (diffgood > 1)
			//	diffgood = 1;
			diffgood *= 100;
			if (Random.Range (0, 101) <= diffgood) {
				i--;
				continue;
			}

			Block b = (Block)blocktypes[bindex].Clone();

			b.y = starts[i] - b.entry_y;
			b.x = startsx[i] - b.entry_x;

			int lindex = 0;
			for (int k=b.y; k<b.y+b.size_y; k++)
				for (int j=b.x; j<b.x+b.size_x; j++){
					//Debug.Log(b.x);
					cells[k][j].setType(b.types[lindex++]);
				}

			if (b.x+b.exit_x < WIDTH-1){
				starts.Add(b.y+b.exit_y);
				startsx.Add(b.x+b.exit_x);
			}
			else{
				ends.Add(b.y);
				continue;
			}
		}

		/*
		for (int i = 0; i < HEIGHT; i++) {
			for (int j = 0; j < WIDTH; j++) {
				if (Random.Range (0, 100) < 30) {
					//cells [i] [j].setType ("platform");

				}
			}
		}
		*/

		return ends;
	}
}
