using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : SystemBase, IOnUpdate
{
    public void OnUpdate()
    {
        if(gameStat.isMoveInput)
        {
            LineDraw(
                gameStat.player.transform.position + gameStat.playerMouseVector * gameStat.lineStartDistance,
                EndPos(
                    gameStat.player.transform.position,
                    gameStat.playerMouseVector,
                    gameStat.lineStartDistance,
                    gameStat.playerMoveMaxDistance,
                    gameStat.playerMoveRayHitLayer
                    ),
                gameStat.playerLineRenderer
            );
        }
        else if (gameStat.isFootHoldInput)
        {
            LineDraw(
                gameStat.player.transform.position + gameStat.playerMouseVector * gameStat.lineStartDistance,
                EndPos(
                    gameStat.player.transform.position,
                    gameStat.playerMouseVector,
                    gameStat.footholdSetDistance,
                    gameStat.footholdSetDistance,
                    gameStat.playerMoveRayHitLayer
                    ),
                gameStat.playerLineRenderer
                );
        }
        else
        {
            LineDraw(
                gameStat.player.transform.position + gameStat.playerMouseVector * gameStat.lineStartDistance,
                gameStat.player.transform.position + gameStat.playerMouseVector * gameStat.lineMaxDistance,
                gameStat.playerLineRenderer
            );
        }
    }

    //Lineの終点
    private Vector3 EndPos(Vector3 _originPos, Vector3 _directionVec, float _baseDistance, float _maxDistance, int _layerMask)
    {
        Vector3 localVec = _originPos + _directionVec * _baseDistance;

        if(Physics.Raycast(_originPos , _directionVec , out RaycastHit _hitInfo , _maxDistance , _layerMask)) 
        {
            localVec = _hitInfo.point;
        }
        
        return localVec;
    }
    
    //Lineの描画
    private void LineDraw(Vector3 _originPos, Vector3 _endPos, LineRenderer _lineRenderer)
    {
        Vector3[] positions = new Vector3[] {_originPos , _endPos};

        _lineRenderer.SetPositions(positions);
    }
}
