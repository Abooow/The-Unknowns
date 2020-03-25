﻿// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-03-19 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all objects related to the actual game, such as creatures and maptiles.
    /// </summary>
    public class GameObject : SceneObject
    {
        /// <summary>
        /// Initialize a new GameObject within the context of a given SceneLayer.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        public GameObject(ISceneLayer sceneLayer) : base(sceneLayer)
        {
        }

        /// <summary>
        /// Initialize a new GameObject within the context of a given SceneLayer, with a given Transform component.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        /// <param name="transform">A Transform component supplying transformation capabilites to this object.</param>
        public GameObject(ISceneLayer sceneLayer, Transform transform) : base(sceneLayer, transform)
        {
        }

        /// <summary>
        /// Determines what happens when the object is out of scope of the camera.
        /// </summary>
        public virtual void OnOutOfScope()
        {
        }

        /// <summary>
        /// Determines what happens when the object collisions with another GameObject.
        /// </summary>
        /// <param name="other">The GameObject this object collided with.</param>
        public virtual void OnCollision(GameObject other)
        {
        }

        /// <summary>
        /// Determines what happens when this GameObject is triggered another GameObject.
        /// </summary>
        /// <param name="other">The GameObject that triggered this object.</param>
        public virtual void OnTrigger(GameObject other)
        {
        }
    }
}
