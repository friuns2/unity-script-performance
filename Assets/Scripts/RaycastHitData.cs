using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GP.Utils
{
    /// <summary>
    /// Data class for the non-alloc Physics helper functions in RaycastHelper
    /// </summary>
    [System.Serializable]
    public class RaycastHitData
    {
        [Header("SETUP")]
        /// <summary>
        /// A Layer mask that is used to selectively ignore colliders when casting.
        /// </summary>
        [SerializeField]
        public LayerMask layerMask = Physics.DefaultRaycastLayers;

        /// <summary>
        /// Specifies whether this query should hit Triggers.
        /// </summary>
        [SerializeField]
        public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;

        /// <summary>
        /// Will resize the hitResults array
        /// if the results array is smaller (or equal to)
        /// the number of results from Physics. You
        /// will always get ALL the hits back, not just
        /// the amount you allocated for.
        /// </summary>
        [SerializeField]
        public bool autoResize = true;

        /// <summary>
        /// The casting functions will allocate the hitResults
        /// array for you if this is greater than zero.
        /// This variable makes it easier to setup in the 
        /// editor.
        /// </summary>
        [SerializeField]
        public int sizeAtInit = 0;

        [Space(10)]
        [Header("RUNTIME RESULTS")]
        /// <summary>
        /// The amount of hits stored into the results buffer.
        /// </summary>
        [SerializeField]
        public int numHits = 0;

        /// <summary>
        /// Results returned from Physics calls
        /// </summary>
        [SerializeField]
        public RaycastHit[] hitResults;

        public const int DEFAULT_SIZE = 16;

        public RaycastHitData()
        {
            if (sizeAtInit > 0)
                Init(sizeAtInit);
        }

        /// <summary>
        /// Initialize the collResults array
        /// </summary>
        /// <param name="size">Array size</param>
        public RaycastHitData(int size)
        {
            if (size > 0)
                Init(size);
        }

        /// <summary>
        /// Initialize the hitResults array
        /// </summary>
        /// <param name="hitCapacity">Set to 0 for default size</param>
        public void Init(int hitCapacity = 0)
        {
            if ( hitCapacity == 0 )
                hitResults = new RaycastHit[DEFAULT_SIZE];
            else
                hitResults = new RaycastHit[hitCapacity];
            numHits = 0;
        }
    }
}
