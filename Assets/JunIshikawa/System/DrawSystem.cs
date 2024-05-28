using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : SystemBase, IOnUpdate
{
    public void OnUpdate()
    {
        LineDraw(
            gameStat.player.transform.position,
            gameStat.playerMouseVector,
            gameStat.playerMoveMaxDistance,
            gameStat.playerMoveRayHitLayer,
            gameStat.playerLineRenderer
        );
    }

    //Lineの終点
    private Vector3 EndPos(Vector3 _originPos, Vector3 _directionVec, float _maxDistance, int _layerMask)
    {
        Vector3 localVec = _originPos;

        if(Physics.Raycast(_originPos , _directionVec , out RaycastHit _hitInfo , _maxDistance , _layerMask)) 
        {
            localVec = _hitInfo.point;
        }
        
        return localVec;
    }
    
    //Lineの描画
    private void LineDraw(Vector3 _originPos, Vector3 _directionVec, float _maxDistance, int _layerMask , LineRenderer _lineRenderer)
    {
        Vector3[] positions = new Vector3[] {_originPos , EndPos(_originPos , _directionVec , _maxDistance , _layerMask)};

        _lineRenderer.SetPositions(positions);
    }
}
