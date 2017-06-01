using UnityEngine;

namespace Leap.Unity
{

    /// <summary>
    /// Use this component on a Game Object to allow it to be manipulated by a pinch gesture.  The component
    /// allows rotation, translation, and scale of the object (RTS).
    /// </summary>
    public class LeapRTSCustom : MonoBehaviour
    {

        public enum RotationMethod
        {
            None,
            Single,
            Full
        }

        // _pinchDetectorA et B a assigner au start() car attache a des prefab

        //[SerializeField]
        private PinchDetector _pinchDetectorA = null;

        public PinchDetector PinchDetectorA
        {
            get
            {
                return _pinchDetectorA;
            }
            set
            {
                _pinchDetectorA = value;
            }
        }

        //[SerializeField]
        private PinchDetector _pinchDetectorB = null;

        public PinchDetector PinchDetectorB
        {
            get
            {
                return _pinchDetectorB;
            }
            set
            {
                _pinchDetectorB = value;
            }
        }

        private SpawnManager spawnManager;

        [SerializeField]
        private RotationMethod _oneHandedRotationMethod;

        [SerializeField]
        private RotationMethod _twoHandedRotationMethod;

        [SerializeField]
        private bool _allowScale = true;

        [Header("GUI Options")]
        [SerializeField]
        private KeyCode _toggleGuiState = KeyCode.None;

        //Transform indexL;

        //Transform indexR;

        [SerializeField]
        private bool _showGUI = true;

        private Transform _anchor;

        private bool isCollisioned = false;

        private float _defaultNearClip;

        void Start()
        {
            //      if (_pinchDetectorA == null || _pinchDetectorB == null) {
            //        Debug.LogWarning("Both Pinch Detectors of the LeapRTS component must be assigned. This component has been disabled.");
            //        enabled = false;
            //      }
            //this._pinchDetectorA = GameObject.Find("LMHeadMountedRig/HandModels/CapsuleHand_L/PinchDector_L").GetComponent<PinchDetector>();

            Debug.Log("Spawn Detector");
            //this._pinchDetectorB = GameObject.Find("LMHeadMountedRig/HandModels/CapsuleHand_R/PinchDector_R").GetComponent<PinchDetector>();
            GameObject pinchControl = new GameObject("RTS Anchor");
            _anchor = pinchControl.transform;
            //transform.parent.transform.position = new Vector3(0f, 0f, 0f);
            if(transform.parent != null)
            {
                //indexL = GameObject.Find("LMHeadMountedRig/HandModels/RigidRoundHand_L/index/bone3").GetComponent<Transform>();
                //indexR = GameObject.Find("LMHeadMountedRig/HandModels/RigidRoundHand_R/index/bone3").GetComponent<Transform>();
                transform.parent.localPosition = new Vector3(0f, 0f, 0f);
                //Vector3 averageIndex = new Vector3((this.indexL.transform.position.x + this.indexR.transform.position.x) / 2f,
                //                                        (this.indexL.transform.position.y + this.indexR.transform.position.y) / 2f,
                //                                        (this.indexL.transform.position.z + this.indexR.transform.position.z) / 2f);
                _anchor.transform.localPosition = new Vector3(0f, 0f, 0f);
                transform.localPosition = new Vector3(0f, 0f, 0f);// new Vector3(0f, 0f, 0f);
            }
            _anchor.transform.parent = transform.parent;
            transform.parent = _anchor;
        }

        public void setPinchDetector(PinchDetector left, PinchDetector right)
        {
            _pinchDetectorA = left;
            _pinchDetectorB = right;
        }

        void Update()
        {

            if (spawnManager != null && spawnManager.getSpawningGo() != null)
            {
                return;
            }

            if (Input.GetKeyDown(_toggleGuiState))
            {
                _showGUI = !_showGUI;
            }

            bool didUpdate = false;
            if (_pinchDetectorA != null)
                didUpdate |= _pinchDetectorA.DidChangeFromLastFrame;
            if (_pinchDetectorB != null)
                didUpdate |= _pinchDetectorB.DidChangeFromLastFrame;

            if (didUpdate)
            {
                transform.SetParent(null, true);
            }

            if (_pinchDetectorA != null && _pinchDetectorA.IsActive &&
                _pinchDetectorB != null && _pinchDetectorB.IsActive)
            {
                transformDoubleAnchor();
            }
            else if (_pinchDetectorA != null && _pinchDetectorA.IsActive && this.isCollisioned == true)
            {
                transformSingleAnchor(_pinchDetectorA);
            }
            else if (_pinchDetectorB != null && _pinchDetectorB.IsActive && this.isCollisioned == true)
            {
                transformSingleAnchor(_pinchDetectorB);
            }

            if (didUpdate)
            {
                transform.SetParent(_anchor, true);
            }
        }

        void OnGUI()
        {
            if (_showGUI)
            {
                GUILayout.Label("One Handed Settings");
                doRotationMethodGUI(ref _oneHandedRotationMethod);
                GUILayout.Label("Two Handed Settings");
                doRotationMethodGUI(ref _twoHandedRotationMethod);
                _allowScale = GUILayout.Toggle(_allowScale, "Allow Two Handed Scale");
            }
        }

        public void SetCollision(bool b)
        {
            this.isCollisioned = b;
        }

        public void SetSpawnManager(SpawnManager spManager)
        {
            this.spawnManager = spManager;
        }

        private void doRotationMethodGUI(ref RotationMethod rotationMethod)
        {
            GUILayout.BeginHorizontal();

            GUI.color = rotationMethod == RotationMethod.None ? Color.green : Color.white;
            if (GUILayout.Button("No Rotation"))
            {
                rotationMethod = RotationMethod.None;
            }

            GUI.color = rotationMethod == RotationMethod.Single ? Color.green : Color.white;
            if (GUILayout.Button("Single Axis"))
            {
                rotationMethod = RotationMethod.Single;
            }

            GUI.color = rotationMethod == RotationMethod.Full ? Color.green : Color.white;
            if (GUILayout.Button("Full Rotation"))
            {
                rotationMethod = RotationMethod.Full;
            }

            GUI.color = Color.white;

            GUILayout.EndHorizontal();
        }

        private void transformDoubleAnchor()
        {
            _anchor.position = (_pinchDetectorA.Position + _pinchDetectorB.Position) / 2.0f;

            switch (_twoHandedRotationMethod)
            {
                case RotationMethod.None:
                    break;
                case RotationMethod.Single:
                    Vector3 p = _pinchDetectorA.Position;
                    p.y = _anchor.position.y;
                    _anchor.LookAt(p);
                    break;
                case RotationMethod.Full:
                    Quaternion pp = Quaternion.Lerp(_pinchDetectorA.Rotation, _pinchDetectorB.Rotation, 0.5f);
                    Vector3 u = pp * Vector3.up;
                    _anchor.LookAt(_pinchDetectorA.Position, u);
                    break;
            }

            if (_allowScale)
            {
                _anchor.localScale = Vector3.one * Vector3.Distance(_pinchDetectorA.Position, _pinchDetectorB.Position);
            }
        }

        private void transformSingleAnchor(PinchDetector singlePinch)
        {
            _anchor.position = singlePinch.Position;

            switch (_oneHandedRotationMethod)
            {
                case RotationMethod.None:
                    break;
                case RotationMethod.Single:
                    Vector3 p = singlePinch.Rotation * Vector3.right;
                    p.y = _anchor.position.y;
                    _anchor.LookAt(p);
                    break;
                case RotationMethod.Full:
                    _anchor.rotation = singlePinch.Rotation;
                    break;
            }

            _anchor.localScale = Vector3.one;
        }
    }
}
