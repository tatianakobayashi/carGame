using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]

public class LimitedCam : CinemachineExtension
{
    public float m_XPosition = 10;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            // Segue target apenas no eixo Y
            pos.x = m_XPosition;
            state.RawPosition = pos;
        }
    }
}
