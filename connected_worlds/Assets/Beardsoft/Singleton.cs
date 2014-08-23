using UnityEngine;
using System.Collections;

namespace Beardsoft.Singleton{
	
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		#region Properties and Variables

		private static bool _applicationIsQuitting;
		protected static bool applicationIsQuitting { get { return _applicationIsQuitting; } }

		private static object _lock = new object();

		protected static T _instance;
		public static T instance{

			get{
				if(applicationIsQuitting){
					Debug.LogWarning("Tried to create instance of " + typeof(T) + " during application quit.");
					return null;
				}

				lock(_lock){
					if(_instance == null){

						_instance = (T) FindObjectOfType(typeof(T));
					
						//Check to see if we've created multiple singletons
						if (FindObjectsOfType(typeof(T)).Length > 1){}

						//If we haven't created any singletons yet let's make one :D
						if(_instance == null){
							string singletonName = "Singleton(" + typeof(T).Name + ")";
							_instance = new GameObject(singletonName).AddComponent<T>();
						}
					}

					return _instance;
				}
			}

		}

		#endregion

		#region MonoBehavior Implementation

		protected virtual void Awake(){
			if(_instance == null){
				_instance = this as T;
			}
			else{
				Debug.LogWarning("Duplicate " + this.name + ". Destroying one");
				Destroy(gameObject);
			}
		}

		protected virtual void OnApplicationQuit(){
			_applicationIsQuitting = true;
		}

		#endregion
	}
}
