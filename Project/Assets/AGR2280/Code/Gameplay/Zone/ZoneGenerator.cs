using UnityEngine;
using System.Collections;

public class ZoneGenerator : MonoBehaviour {


	int ZonePieceCount;
	public int NumberOfPieces;
	public GameObject ZonePiece_Straight;
	public GameObject ZonePiece_TurnLeft;
    public GameObject ZonePiece_TurnRight;
    GameObject thisPiece;

	public enum TrackPiece
	{
		Piece_Straight = 0,
		Piece_Turn = 1
	};

    int turnBasedPieceSide;

	void Start () 
	{
		ZonePieceCount = 1;
	}

	public void GenerateNewPiece()
	{
		int randomNum = Random.Range(0, NumberOfPieces + 1);
		switch (randomNum)
        {
            case 1:
                thisPiece = ZonePiece_Straight;
                break;
            case 2:
                thisPiece = ZonePiece_TurnLeft;
                break;
            case 3:
                thisPiece = ZonePiece_TurnRight;
                break;
        }

        Transform connector = GameObject.Find("Piece_" + (ZonePieceCount - 1)).transform.Find("Connector");
        Object placePiece = Instantiate(thisPiece, connector.transform.position, connector.transform.rotation);
        placePiece.name = "Piece_" + ZonePieceCount;
        Destroy(GameObject.Find("Piece_" + (ZonePieceCount - 3)));
        ZonePieceCount++;

	}
}
