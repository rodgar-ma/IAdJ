﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AgentNPC))]
public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] protected Agent target;
    protected bool useDefaultTarget = true;
    protected string nameSteering = "no steering";

    public Agent Target
    {
        set { target = value; }
        get { return target; }
    }

    public string NameSteering
    {
        set { nameSteering = value; }
        get { return nameSteering; }
    }


    /// <summary>
    /// Cada SteerinBehaviour retornará un Steering=(vector, escalar)
    /// acorde a su propósito: llegar, huir, pasear, ...
    /// Sobreescribie siempre este método en todas las clases hijas.
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    public virtual Steering GetSteering(Agent agent)
    {
        return null;
    }


    protected virtual void OnGUI()
    {
        // Para la depuración te puede interesar que se muestre el nombre
        // del steeringbehaviour sobre el personaje.
        // Te puede ser util Rect() y GUI.TextField()
        // https://docs.unity3d.com/ScriptReference/GUI.TextField.html
    }
}
