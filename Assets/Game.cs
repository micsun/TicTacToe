using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour
{
	const int GRID_MAX = 3;

	public GameObject[,] tiles = new GameObject[3,3];
	public GameObject statusText;

	private int _moves = 0;
	private bool _gameEnd = false;

	void Start()
	{
		tiles[0, 0] = GameObject.Find("Tile0");
		tiles[0, 1] = GameObject.Find("Tile1");
		tiles[0, 2] = GameObject.Find("Tile2");
		tiles[1, 0] = GameObject.Find("Tile3");
		tiles[1, 1] = GameObject.Find("Tile4");
		tiles[1, 2] = GameObject.Find("Tile5");
		tiles[2, 0] = GameObject.Find("Tile6");
		tiles[2, 1] = GameObject.Find("Tile7");
		tiles[2, 2] = GameObject.Find("Tile8");

		statusText.guiText.text = "";
	}

	void Update()
	{
		
	}

	public bool GameEnded()
	{
		return _gameEnd;
	}

	public void Step(GameTile.TileType type)
	{
		_moves++;

		if (checkWin(GameTile.TileType.X))
		{
			statusText.guiText.text = "You win!";
			_gameEnd = true;
		}
		else if (checkWin(GameTile.TileType.O))
		{
			statusText.guiText.text = "You lose!";
			_gameEnd = true;
		}
		else if (checkTie())
		{
			statusText.guiText.text = "Tie!";
			_gameEnd = true;
		}
		else if (type != GameTile.TileType.O)
		{
			computerMove();
		}
	}

	private void computerMove()
	{
		// No AI, just fill in the blanks...
		var blanks = new List<GameTile>();

		for (var i = 0; i < GRID_MAX; i++)
		{
			for (var j = 0; j < GRID_MAX; j++)
			{
				if (tiles[i, j].GetComponent<GameTile>().GetTileType() == GameTile.TileType.Blank)
				{
					blanks.Add(tiles[i, j].GetComponent<GameTile>());
				}
			}
		}

		if (blanks.Count > 0)
		{
			var randomBlank = blanks.ElementAtOrDefault(new System.Random().Next() % blanks.Count);
			randomBlank.SetTileType(GameTile.TileType.O);
			Step(GameTile.TileType.O);
		}
	}

	private bool checkWin(GameTile.TileType tileType)
	{
		// Check for horizontal win
		for (var i = 0; i < GRID_MAX; i++)
		{
			for (var j = 0; j < GRID_MAX; j++)
			{
				if (tiles[i, j].GetComponent<GameTile>().GetTileType() != tileType)
				{
					break;
				}

				if (j == GRID_MAX - 1)
				{
					return true;
				}
			}
		}

		// Check for vertical win
		for (var i = 0; i < GRID_MAX; i++)
		{
			for (var j = 0; j < GRID_MAX; j++)
			{
				if (tiles[i, j].GetComponent<GameTile>().GetTileType() != tileType)
				{
					break;
				}

				if (j == GRID_MAX - 1)
				{
					return true;
				}
			}
		}

		// Check for diagnol win
		for (var i = 0; i < GRID_MAX; i++)
		{
			if (tiles[i, i].GetComponent<GameTile>().GetTileType() != tileType)
			{
				break;
			}

			if (i == GRID_MAX - 1)
			{
				return true;
			}
		}

		for (var i = 0; i < GRID_MAX; i++)
		{
			if (tiles[GRID_MAX - 1 - i, i].GetComponent<GameTile>().GetTileType() != tileType)
			{
				break;
			}

			if (i == GRID_MAX - 1)
			{
				return true;
			}
		}

		return false;
	}

	private bool checkTie()
	{
		return _moves >= GRID_MAX * GRID_MAX;
	}
}
