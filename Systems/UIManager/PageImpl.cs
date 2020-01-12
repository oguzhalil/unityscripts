using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityScripts
{
    public abstract class PageImpl : MonoBehaviour
    {
        private Page pageRef;
        public PageTypes pageType;
        public Page page
        {
            get
            {
                if ( pageRef == null )
                {
                    pageRef = UIManager.Instance.GetPage( pageType );
                }

                return pageRef;
            }
        }

        public abstract void VOnRestore (); // Restore page to default values
    }
}
