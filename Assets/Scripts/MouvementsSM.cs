using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouvementsSM : MonoBehaviour
{
   private Rigidbody _rb; //Variable pouvant contenir une valeur de type Rigidbody

   private Vector3 _direction; // Variable globale privée, pouvant contenir une valeur de type Vector3
                               //Variable qui contient les valeurs permettant de diriger le Gameobject.
 
    private Vector3 _directionVerticale;
 
   [SerializeField] private float _forceVitesse; // Variable pouvant contenir une valeur de type float
                                                 // Variable pour contrôler la vitesse et, ou, la force de déplacement
                                                 // Variable accessible dans l'inspecteur, sous forme de champ de texte

    [SerializeField] private float _forceHaut;

   //---------------------------------------------------------------------------------------------------------

   void Start(){

    _rb = GetComponent<Rigidbody>(); // Donne une valeur à la variable _rb lorsque le script s’exécute

   }
   
   void FixedUpdate(){

        BougeSM(); //Appel de la méthode pour faire bouger le GameObject
        SMVol();

   }
   
   
   //----------------------------------------------------------------------------------------------------------------
   
    void OnMove(InputValue valeur){   //Méthode qui s’exécute lorsque les touches W, S, A, D, sont enfoncées
                                      // Paramètre nécessaire pour récupérer les entrées sous la forme d’un Vector2 ((x, y)), lorsqu’une des touches W, S, A, D, est enfoncée
       Vector2 _valeurRecue = valeur.Get<Vector2>();
        _direction = new Vector3(_valeurRecue.x,0,_valeurRecue.y);  // Convertis les entrées clavier (Vector2d) en vecteur de déplacement.

    }

    void OnMonteDescend(InputValue valeur){

        float _valeurRecue = valeur.Get<float>();
        _directionVerticale = new Vector3(0,_valeurRecue,0);

    }
   
   //---------------------------------------------------------------------------------------------------------------------
   
   void BougeSM(){ // Méthode servant au déplacement du GameObject

        _rb.AddForce(_direction * Time.deltaTime * _forceVitesse,ForceMode.VelocityChange); // Instruction qui permet d’appliquer une force douce pour déplacer le GameObject

   }
   
   //-----------------------------------------------------------------------------------------------------------------------
   
   void SMVol(){

        _rb.AddForce(_directionVerticale * Time.deltaTime * _forceHaut,ForceMode.VelocityChange);

   }
   
   
}
