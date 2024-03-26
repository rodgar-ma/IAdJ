using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{ 

    //Tipos de tropa
    public enum TroopClass
    {
        Ligera,
        Pesada,
        Normal
    }

    
    [SerializeField] protected Steering steer;  // Este será el steering final que se aplique al personaje.
    private List<SteeringBehaviour> listSteerings;  // Todos los steering que tiene que calcular el agente.
    public TroopClass troop_type;


    protected  void Awake()
    {
        this.steer = new Steering();

        // Construye una lista con todos las componenen del tipo SteeringBehaviour.
        // La llamaremos listSteerings
        // Puedes usar GetComponents<>()
        listSteerings = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
    }


    // Use this for initialization
    void Start()
    {
        this.Velocity = Vector3.zero;
        setBodiValues();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // En cada frame se actualiza el movimiento
        ApplySteering(Time.deltaTime);

        // En cada frame podría ejecutar otras componentes IA
    }


    private void ApplySteering(float deltaTime)
    {
        Acceleration = Vector3.zero;
        // Actualizar las propiedades para Time.deltaTime según NewtonEuler
        // La actualización de las propiedades se puede hacer en LateUpdate()
        
        // MOVIMIENTO UNIFORME ACELERADO
        // Aceleracion
        Acceleration = this.steer.linear;

        // Si la aceleración es demasiado pequeña paramos al NPC
        
        if(Acceleration.magnitude <= 0.1f)
        {
            Velocity = Vector3.zero;
            Acceleration = Vector3.zero;
        }
        

        // Aceleracion Angular
        AngularAcc = this.steer.angular;
        
        // Si la aceleracion angular es demasiado pequeña paramos de rotar al NPC
        if (AngularAcc <= 0.001f && AngularAcc >= -0.001f)
        {
            Rotation = 0f;
        }
        

        // Velocity
        Velocity = Velocity + Acceleration * deltaTime;   // Steer se interpreta como velocidades.
        // Rotation
        Rotation = Rotation + AngularAcc * deltaTime;  // Aplicamos N-E para a=0
        // Position
        Position = Position + Velocity * deltaTime;
        // Orientation
        Orientation = Orientation + Rotation * deltaTime;

        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, Orientation);

    }



    public virtual void LateUpdate()
    {
        Steering kinematicFinal = new Steering();

        // Reseteamos el steering final.
        this.steer = new Steering();

        // Recorremos cada steering
        foreach (SteeringBehaviour behavior in listSteerings)
        {
            Steering kinematic = behavior.GetSteering(this);
            if (kinematic != null)
            {
                kinematicFinal.angular = kinematicFinal.angular + kinematic.angular;
                kinematicFinal.linear = kinematicFinal.linear + kinematic.linear;
            }
            
        }
        //// La cinemática de este SteeringBehaviour se tiene que combinar
        //// con las cinemáticas de los demás SteeringBehaviour.
        //// Debes usar kinematic con el árbitro desesado para combinar todos
        //// los SteeringBehaviour.
        //// Llamaremos kinematicFinal a la aceleraciones finales de esas combinaciones.

        // A continuación debería entrar a funcionar el actuador para comprobar
        // si la propuesta de movimiento es factible:
        //kinematicFinal = Actuador(kinematicFinal, self)


        // El resultado final se guarda para ser aplicado en el siguiente frame.
        this.steer = kinematicFinal;
    }

    private void setBodiValues()
    {
        switch (troop_type)
        {
            case TroopClass.Ligera:
                MaxAcceleration *= 1.5f;
                break;
            case TroopClass.Pesada:
                MaxAcceleration *= 0.75f;
                break;
        }
    }
}
