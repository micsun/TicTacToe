using UnityEngine;
using System.Collections;

public class GameTile : MonoBehaviour
{
	public enum TileType { Blank, X, O };

	public Sprite blankTexture;
	public Sprite textureO;
	public Sprite textureX;
	public Sprite transparentX;

	private TileType _tileType;
	private Game _game;

	void Start()
	{
		SetTileType(TileType.Blank);
		_game = GameObject.Find("Game").GetComponent<Game>();
	}

	void OnMouseOver()
	{
		if (_tileType == TileType.Blank && !_game.GameEnded())
		{
			GetComponent<SpriteRenderer>().sprite = transparentX;
		}
	}

	void OnMouseExit()
	{
		if (_tileType == TileType.Blank)
		{
			GetComponent<SpriteRenderer>().sprite = blankTexture;
		}
	}

	void OnMouseUpAsButton()
	{
		if (_game.GameEnded())
		{
			Application.LoadLevel("MainMenu");
		}
		if (_tileType == TileType.Blank)
		{
			SetTileType(TileType.X);
			GameObject.FindWithTag("Game").SendMessage("Step", TileType.X);
		}
	}

	public void SetTileType(GameTile.TileType type)
	{
		_tileType = type;
		
		switch (type)
		{
			case TileType.Blank:
				GetComponent<SpriteRenderer>().sprite = blankTexture;
				break;
			case TileType.O:
				GetComponent<SpriteRenderer>().sprite = textureO;
				break;
			case TileType.X:
				GetComponent<SpriteRenderer>().sprite = textureX;
				break;
		}
	}

	public TileType GetTileType()
	{
		return _tileType;
	}
}
