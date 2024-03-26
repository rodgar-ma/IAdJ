using UnityEngine;

public class Bodi : MonoBehaviour
{

    [SerializeField] protected float _mass = 1;
    [SerializeField] protected float _maxSpeed = 1;
    [SerializeField] protected float _maxRotation = 1;
    [SerializeField] protected float _maxAcceleration = 1;
    [SerializeField] protected float _maxAngularAcc = 1;
    [SerializeField] protected float _maxForce = 1;

    protected Vector3 _acceleration; // aceleración lineal
    protected float _angularAcc;  // aceleración angular
    protected Vector3 _velocity; // velocidad lineal
    protected float _rotation;  // velocidad angular
    protected float _speed;  // velocidad escalar
    protected float _orientation;  // 'posición' angular
    // Se usará transform.position como 'posición' lineal

    /// Un ejemplo de cómo construir una propiedad en C#
    /// <summary>
    /// Mass for the NPC
    /// </summary>
    public float Mass
    {
        get { return _mass; }
        set { _mass = Mathf.Max(0, value); }
    }

    // CONSTRUYE LAS PROPIEDADES SIGUENTES. PUEDES CAMBIAR LOS NOMBRE A TU GUSTO
    // Lo importante es controlar el set

    // Aceleración máxima
    public float MaxForce
    {
        get { return _maxForce; }
        set { _maxForce = Mathf.Max(0, value); } // Fuerza maxima NO Negativa
    }
    // public float MaxSpeed
    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = Mathf.Max(0, value); } // Velocidad máxima NO Negativa
    }
    // public Vector3 Velocity
    public Vector3 Velocity
    {
        get { return new Vector3(_velocity.x, _velocity.y, _velocity.z); }
        set { _velocity = checkValue(value, MaxSpeed); }
    }
    // public float MaxRotation
    public float MaxRotation
    {
        get { return _maxRotation; }
        set { _maxRotation = Mathf.Max(0, value); } // Rotacion máxima NO Negativa
    }
    // public float Rotation. 
    public float Rotation
    {
        get { return _rotation; }
        set { _rotation = checkValue(value, MaxRotation); }
    }

    // public float MaxAcceleration
    public float MaxAcceleration
    {
        get { return _maxAcceleration; }
        set { _maxAcceleration = Mathf.Max(0, value); } // Aceleración maxima NO Negativa
    }
    // public float MaxAngularAcc
    public float MaxAngularAcc
    {
        get { return _maxAngularAcc; }
        set { _maxAngularAcc = Mathf.Max(0, value); } // Aceleración maxima NO Negativa
    }

    // public Vector3 Acceleration
    public Vector3 Acceleration
    {
        get { return new Vector3(_acceleration.x, _acceleration.y, _acceleration.z); }
        set { _acceleration = checkValue(value, MaxAcceleration); }
    }
    // public float AngularAcc
    public float AngularAcc
    {
        get { return _angularAcc; }
        set { _angularAcc = checkValue(value, MaxAngularAcc); }
    }
    // public Vector3 Position. Recuerda. Esta es la única propiedad que trabaja sobre transform.
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    // public float Orientation
    public float Orientation
    {
        get { return _orientation; }
        set { _orientation = value; }
    }
    // public float Speed  
    public float Speed
    {
        get { return _speed; }
        set { _speed = Mathf.Max(0, value); }
    }

    // TE PUEDEN INTERESAR LOS SIGUIENTES MÉTODOS.
    // Añade todos los que sean referentes a la parte física.

    public float DegreesToRadians(float grados)
    {
        return (grados * 2 * Mathf.PI / 360);
    }

    public float RadiansToDegrees(float radians)
    {
        return (radians * 360 / (2 * Mathf.PI));
    }

    // Devuelve el ángulo de un vector en radianes
    public float VectorToAngle(Vector3 vec)
    {
        // Obtenemos el angulo en radianes
        float rAngle = Mathf.Atan2(vec.x, vec.z);
        // Lo devolvemos en grados
        return RadiansToDegrees(rAngle);
    }

    public Vector3 AngleToVector(float grados)
    {
        float x, z;
        float rAngle = DegreesToRadians(grados);
        x = Mathf.Sin(rAngle);
        z = Mathf.Cos(rAngle);

        return new Vector3(x, 0, z);
    }

    //      Retorna el ángulo heading en (-180, 180) en grado o radianes. Lo que consideres
    public float Heading()
    {
        float heading = Orientation % 360;
        if (heading <= 180)
        {
            return heading;
        }
        else return heading - 360;
        // Se devuelve en grados
    }

    public Vector3 checkValue(Vector3 value, float maxValue)
    {
        if(value.magnitude > maxValue)
        {
            value.Normalize();
            value = value * maxValue;
        }
        return value;
    }

    public float checkValue(float value, float maxValue)
    {
        if (Mathf.Abs(value) > maxValue)
        {
            if(value < 0)
            {
                value = maxValue * (-1);
            }
            else
            {
                value = maxValue;
            }
        }
        return value;
    }

    // public static float MapToRange(float rotation, Range r)
    //      Retorna un ángulo de (-180, 180) a (0, 360) expresado en grado or radianes
    // public float MapToRange(Range r)
    //      Retorna la orientación de este bodi, un ángulo de (-180, 180), a (0, 360) expresado en grado or radianes

    //      Retorna el ángulo de una posición usando el eje Z como el primer eje
    // public float PositionToAngle()

    // public Vector3 OrientationToVector()
    //      Retorna un vector a partir de una orientación usando Z como primer eje
    // public Vector3 VectorHeading()  // Nombre alternativo
    //      Retorna un vector a partir de una orientación usando Z como primer eje
    public float GetMiniminAngleTo(float directionAngle)
    {
        // Obtenemos angulo orientacion en grados
        float orientationAngle = Heading();

        // Obtenemos la diferencia minima
        float diffAngle = directionAngle - orientationAngle;

        if(diffAngle > 180)
        {
            diffAngle -= 360;
        }
        if(diffAngle < -180)
        {
            diffAngle += 360;
        }
        return diffAngle;
    }
    //      Determina el menor ángulo en 2.5D para que desde la orientación actual mire en la dirección del vector dado como parámetro
    // public void ResetOrientation()
    //      Resetea la orientación del bodi
    // public float PredictNearestApproachTime(Bodi other, float timeInit, float timeEnd)
    //      Predice el tiempo hasta el acercamiento más cercano entre este y otro vehículo entre B y T (p.e. [0, Mathf.Infinity])
    // public float PredictNearestApproachDistance3(Bodi other, float timeInit, float timeEnd)

}
