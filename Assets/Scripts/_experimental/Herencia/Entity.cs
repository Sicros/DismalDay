using UnityEngine;


// con "abstract" se define que esta clase solo se puede heredar, más no asignar a un objeto.
// Ayuda en caso de que no queremos que nuestra clase padre defina a un objeto, solo que los hijos
// cumplan este rol, pero tengan acceso a la información de la clase.
public abstract class Entity : MonoBehaviour
{
    // protected es similar a private, pero permite a las clases hijos acceder a él.
    // [SerializeField] protected string entityName;
    // [SerializeField] protected string entityID;
    protected string entityName;
    protected string entityID;

    // Cuando un método es marcado como virtual, se crea virtualmente un tabla
    // que ordena el método de acuerdo a la clase que lo llama.
    public virtual string GetName()
    {
        return entityName;
    }

    public virtual void Init()
    {

    }
}
