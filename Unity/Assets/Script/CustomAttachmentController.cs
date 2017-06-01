using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Attachments
{

    public class CustomAttachmentController : AttachmentController
    {
        [SerializeField]
        HandAttachments handAttachment;
        
        protected virtual void ChangeChildState()
        {
            if(handAttachment!=null)
            {
                activate(handAttachment.Palm, this.IsActive);
                activate(handAttachment.Arm, this.IsActive);
                activate(handAttachment.Thumb, this.IsActive);
                activate(handAttachment.PinchPoint, this.IsActive);
                activate(handAttachment.Index, this.IsActive);
                activate(handAttachment.Middle, this.IsActive);
                activate(handAttachment.Ring, this.IsActive);
                activate(handAttachment.Pinky, this.IsActive);
                activate(handAttachment.GrabPoint, this.IsActive);
            }
        }

        private void activate(Transform tr, bool b)
        {
            GameObject go = tr.gameObject;
            if(go!=null)
            {
                go.SetActive(b);
            }
        }

        private void OnDisable()
        {
            if (DeactivateOnDisable)
                Deactivate(false);
        }

    }
}