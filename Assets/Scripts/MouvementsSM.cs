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

     private Animator _animator;

     private Vector3 vitesseSurPlane;

     private float _modVitesse;

     //---------------------------------------------------------------------------------------------------------

     void Start()
     {

          _rb = GetComponent<Rigidbody>(); // Donne une valeur à la variable _rb lorsque le script s’exécute
          _animator = GetComponent<Animator>();
          _modVitesse = 1;
          _animator.SetFloat("Sprint", 1);

     }

     void FixedUpdate()
     {

          BougeSM(); //Appel de la méthode pour faire bouger le GameObject
          SMVol();

     }


     //----------------------------------------------------------------------------------------------------------------

     void OnMove(InputValue valeur)
     {   //Méthode qui s’exécute lorsque les touches W, S, A, D, sont enfoncées
         // Paramètre nécessaire pour récupérer les entrées sous la forme d’un Vector2 ((x, y)), lorsqu’une des touches W, S, A, D, est enfoncée
          Vector2 _valeurRecue = valeur.Get<Vector2>();
          _direction = new Vector3(_valeurRecue.x, 0, _valeurRecue.y);  // Convertis les entrées clavier (Vector2d) en vecteur de déplacement.

     }

     void OnMonteDescend(InputValue valeur)
     {

          float _valeurRecue = valeur.Get<float>();
          _directionVerticale = new Vector3(0, _valeurRecue, 0);
          vitesseSurPlane = new Vector3(0, _rb.velocity.y, 0);
          _animator.SetFloat("Verticale", vitesseSurPlane.magnitude);

     }

     void OnShift(InputValue valeur)
     {

          float _valeurRecue = valeur.Get<float>();
          Debug.Log(_valeurRecue);
          if (_valeurRecue > 0)
          {
               _modVitesse = 2;
               _animator.SetFloat("Sprint", 2);
          }
          else
          {
               _modVitesse = 1;
               _animator.SetFloat("Sprint", 1);
          }




     }

     //---------------------------------------------------------------------------------------------------------------------

     void BougeSM()
     { // Méthode servant au déplacement du GameObject

          _rb.AddForce(_direction * Time.deltaTime * _forceVitesse * _modVitesse, ForceMode.VelocityChange); // Instruction qui permet d’appliquer une force douce pour déplacer le GameObject
          vitesseSurPlane = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
          _animator.SetFloat("Horizontale", vitesseSurPlane.magnitude);

     }


     //-----------------------------------------------------------------------------------------------------------------------

     void SMVol()
     {

          _rb.AddForce(_directionVerticale * Time.deltaTime * _forceHaut * _modVitesse, ForceMode.VelocityChange);

     }


}
