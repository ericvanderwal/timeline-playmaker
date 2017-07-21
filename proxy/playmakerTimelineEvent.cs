using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using HutongGames.PlayMaker;

public class playmakerTimelineEvent : BasicPlayableBehaviour
{
	
	public ExposedReference<PlayMakerFSM> playmakerObject;
	public string eventName;
	public bool broadcastAll;
	public bool enableDebug;
	
	private PlayMakerFSM fsm;

	public override void OnGraphStart(Playable playable) 
	{
		
		fsm = playmakerObject.Resolve(playable.GetGraph().GetResolver());
		
	}
	
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		
		if (playable.GetTime() <= 0)
			return;
		
		if(eventName == "")
		{
			if (enableDebug)
			{
				Debug.Log("You are missing an event name.");
			}
			return;
		}
	}
	
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
				
		if(!broadcastAll)
		{
			if(fsm == null)
			{
				if (enableDebug)
				{
					Debug.Log("You are missing a FSM name.");
				}
				return;
			}
			
			else
			{
				fsm.SendEvent(eventName);
				if(enableDebug)
				{
					Debug.Log("The event: " + eventName + " has been sent to FSM: " + fsm.FsmName);
				}
			}
		}
		
		else
		{
			PlayMakerFSM.BroadcastEvent(eventName);
			if(enableDebug)
			{
				Debug.Log("The event: " + eventName + " has been sent broadcast to all");
			}
		}
		
		
	}

}
