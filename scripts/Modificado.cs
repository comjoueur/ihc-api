/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;
using UnityEngine.Video;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class Modificado : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;



    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();

            if (mTrackableBehaviour.TrackableName == "elephant")
            {
                if (ConnectWS.opcion_mia_de_mi !=6)
                {
                    ConnectWS.opcion_mia_de_mi = 6;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.elefanteseleccionado = true;
                Debug.Log("elephant");
            }
            if (mTrackableBehaviour.TrackableName == "Jobcard")//11
            {
                if (ConnectWS.opcion_mia_de_mi != 11)
                {
                    ConnectWS.opcion_mia_de_mi = 11 ;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.humanseleccionado = true;
                Debug.Log("humano");
            }
            if (mTrackableBehaviour.TrackableName == "rhino")//7
            {

                if (ConnectWS.opcion_mia_de_mi != 5)
                {
                    ConnectWS.opcion_mia_de_mi = 5;
                    ConnectWS.cambio = true;
                }
                Debug.Log("rhino");
            }
            if (mTrackableBehaviour.TrackableName == "whale")
            {
                if (ConnectWS.opcion_mia_de_mi != 5)
                {
                    ConnectWS.opcion_mia_de_mi = 5;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.whaleseleccionado = true;
                Debug.Log("whale");
            }
            if (mTrackableBehaviour.TrackableName == "tiger")
            {
                if (ConnectWS.opcion_mia_de_mi != 1)
                {
                    ConnectWS.opcion_mia_de_mi = 1;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.tigreseleccionado = true;
                Debug.Log("tigre");
            }
            if (mTrackableBehaviour.TrackableName == "gorilla")//8
            {
                if (ConnectWS.opcion_mia_de_mi != 8)
                {
                    ConnectWS.opcion_mia_de_mi = 8;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.gorilaseleccionado = true;
                Debug.Log("gorila");
            }
            if (mTrackableBehaviour.TrackableName == "deer")
            {
                if (ConnectWS.opcion_mia_de_mi != 3)
                {
                    ConnectWS.opcion_mia_de_mi = 3;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.deerseleccionado = true;
                Debug.Log("deer");
            }
            if (mTrackableBehaviour.TrackableName == "bear")
            {
                if (ConnectWS.opcion_mia_de_mi != 2)
                {
                    ConnectWS.opcion_mia_de_mi = 2;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.ososeleccionado = true;
                Debug.Log("oso");
            }
            if (mTrackableBehaviour.TrackableName == "shark")//9
            {
                if (ConnectWS.opcion_mia_de_mi != 9)
                {
                    ConnectWS.opcion_mia_de_mi = 9;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.sharkseleccionado = true;
                Debug.Log("shark");
            }
            if (mTrackableBehaviour.TrackableName == "octopus")
            {
                if (ConnectWS.opcion_mia_de_mi != 4)
                {
                    ConnectWS.opcion_mia_de_mi = 4;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.pulposeleccionado = true;
                Debug.Log("pulpo");
            }
            if (mTrackableBehaviour.TrackableName == "owl")//10
            {
                if (ConnectWS.opcion_mia_de_mi != 10)
                {
                    ConnectWS.opcion_mia_de_mi = 10;
                    ConnectWS.cambio = true;
                }
                Animalseleccionado.owlseleccionado = true;
                Debug.Log("owl");
            }
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
            if (mTrackableBehaviour.TrackableName == "elephant")
            {
                Animalseleccionado.elefanteseleccionado = false;
                Debug.Log("elephant");
            }
            if (mTrackableBehaviour.TrackableName == "Jobcard")
            {
                Animalseleccionado.humanseleccionado = false;
                Debug.Log("humano");
            }
            if (mTrackableBehaviour.TrackableName == "rhino")
            {
                Animalseleccionado.rhinoseleccionado = false;
                Debug.Log("rhino");
            }
            if (mTrackableBehaviour.TrackableName == "whale")
            {
                Animalseleccionado.whaleseleccionado = false;
                Debug.Log("whale");
            }
            if (mTrackableBehaviour.TrackableName == "tiger")
            {
                Animalseleccionado.tigreseleccionado = false;
                Debug.Log("tigre");
            }
            if (mTrackableBehaviour.TrackableName == "gorilla")
            {
                Animalseleccionado.gorilaseleccionado = false;
                Debug.Log("gorila");
            }
            if (mTrackableBehaviour.TrackableName == "deer")
            {
                Animalseleccionado.deerseleccionado = false;
                Debug.Log("deer");
            }
            if (mTrackableBehaviour.TrackableName == "bear")
            {
                Animalseleccionado.ososeleccionado = false;
                Debug.Log("oso");
            }
            if (mTrackableBehaviour.TrackableName == "shark")
            {
                Animalseleccionado.sharkseleccionado = false;
                Debug.Log("shark");
            }
            if (mTrackableBehaviour.TrackableName == "octopus")
            {
                Animalseleccionado.pulposeleccionado = false;
                Debug.Log("pulpo");
            }
            if (mTrackableBehaviour.TrackableName == "owl")
            {
                Animalseleccionado.owlseleccionado = false;
                Debug.Log("owl");
            }
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
