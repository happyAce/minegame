using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventBase : MonoBehaviour
{

	public delegate void handler(object sender,EventArgs e);

	protected Dictionary<string,handler> event_handlers;
	
	protected EventBase()
	{
		event_handlers = new Dictionary<string, handler>(); 
	}
	
	public void postEvent(string name)
	{
		postEvent(name,null,EventArgs.Empty);
	}
	public void postEvent(string name,object sender)
	{
		postEvent(name,sender,EventArgs.Empty);
	}
	public void postEvent(string name,object sender,EventArgs e)
	{
		if(event_handlers[name] != null )
		{
			event_handlers[name](sender,e);
		}
	}
	
	public void addEvent(string name, handler _handler)
	{ 
		event_handlers[name] += _handler;  
	}
	public void removeEvent(string name, handler _handler)
	{
		event_handlers[name] -= _handler;
	}
	
}
