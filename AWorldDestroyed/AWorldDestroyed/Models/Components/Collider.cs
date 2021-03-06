﻿// =============================================
//         Editor:     Philip  Abrahamsson
//         Last edit:  2020-03-24
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using System.Collections.Generic;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;

namespace AWorldDestroyed.Models.Components
{
    public delegate void ColliderEvent(Collider collider, GameObject other, Side side);
    
    /// <summary>
    /// Defines bounds for collision detection.
    /// </summary>
    public class Collider : Component, Utility.IUpdateable
    {
        public bool IsTrigger { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Offset { get; set; }
        public float Friction { get; set; }

        public event ColliderEvent OnTrigger;
        public event ColliderEvent OnTriggerEnter;
        public event ColliderEvent OnTriggerExit;
        public event ColliderEvent OnCollision;

        private List<GameObject> triggeredGameObj;
        private List<GameObject> triggeredThisFrame;

        /// <summary>
        /// Creates a new instance of the Collider class with the given size.
        /// </summary>
        /// <param name="size">The height and width of the Collider.</param>
        public Collider(Vector2 size) :base()
        {
            Size = size;
            Offset = Vector2.Zero;
            Friction = 0.9f;

            triggeredGameObj = new List<GameObject>();
            triggeredThisFrame = new List<GameObject>();
        }

        /// <summary>
        /// Updates the lists that handle OnEnter and OnExit detection.
        /// Fires OnExit event if an object has left the area.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            for (int i = triggeredGameObj.Count - 1; i >= 0; i--)
            {
                if (!triggeredThisFrame.Contains(triggeredGameObj[i]))
                {
                    OnTriggerExit?.Invoke(this, triggeredGameObj[i], Side.Unknown);
                    triggeredGameObj.Remove(triggeredGameObj[i]);
                }
            }
            triggeredThisFrame.Clear();
        }

        /// <summary>
        /// Get the bounds rectangle of this collider.
        /// </summary>
        /// <returns>Floating point 2D-rectangle.</returns>
        public RectangleF GetRectangle()
        {
            Vector2 position = AttachedTo.Transform.WorldPosition + Offset;
            return new RectangleF(position, Size * AttachedTo.Transform.Scale);
        }

        /// <summary>
        /// Method to fire trigger events based on certain conditions.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="side"></param>
        public void Trigger(GameObject other, Side side)
        {
            if (!triggeredGameObj.Contains(other))
            {
                triggeredGameObj.Add(other);
                OnTriggerEnter?.Invoke(this, other, side);
            }

            if (!triggeredThisFrame.Contains(other)) triggeredThisFrame.Add(other);

            OnTrigger?.Invoke(this, other, side);
        }

        /// <summary>
        /// Method to fire collision events.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="side"></param>
        public void Collide(GameObject other, Side side)
        {
            OnCollision?.Invoke(this, other, side);
        }

        /// <summary>
        /// Creates a copy of the Collider instance with the same attribute values as this instance.
        /// </summary>
        /// <returns>A copy of this Collider instance.</returns>
        public override Component Copy()
        {
            return new Collider(this.Size)
            {
                Offset = this.Offset
            };
        }
    }
}