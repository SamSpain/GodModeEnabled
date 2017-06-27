using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Building
{
    
    public class ResourceSilo : Building
    {
       


        public Type ResourceType { get; set; }
        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public void Start()
        {
            MaxAmount = 100;
        }
        public void Create(Type resourceType, int MaxAmount)
        {
            ResourceType = resourceType;
            
        }

        /// <summary>
        /// Deposits a resource type into the silo. Discards excess.
        /// </summary>
        /// <param name="incoming">How much agent deposited.</param>
        public void Deposit(int incoming)
        {
            Amount += incoming;
            Amount = Mathf.Clamp(Amount, 0, MaxAmount);
            
        }

        /// <summary>
        /// Withdraws a resource type from the silo.
        /// </summary>
        /// <param name="outgoing">How much agent would need.</param>
        /// <returns>Whether the silo has successfully been able to deduct amount.</returns>
        public bool Withdraw(int outgoing)
        {
            if(outgoing > Amount)
            {
                return false;
            }
            else
            {
                Amount -= outgoing;
                
                return true;
            }
        }

        

        void OnGUI()
        {
            Vector2 worldPoint = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(worldPoint.x - 100, (Screen.height - worldPoint.y) - 50, 200, 100), "Resource Amount: " + Amount);
        }
    } 
}
