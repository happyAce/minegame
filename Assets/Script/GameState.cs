using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameState : EventBase 
{
	private static EventBase singleleton;
 	private event GameState.handler ScoreAdd;
	private event GameState.handler GameOver;
 	
	private GameState():base()
 	{
 		event_handlers["scoreadd"] = ScoreAdd;
 		event_handlers["gameover"] = GameOver;
 	}
	public static EventBase GetInstance()
	{
		if(singleleton == null)
			singleleton = new GameState();
		return singleleton; 
	}  	
}
