using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootholdSystem : SystemBase, IOnUpdate
{
    public override void SetUp()
    {
        
    }

    public void OnUpdate()
    {
        if (gameStat.isFootHoldInputUp)
        {
            Debug.Log("A");
            if (!isPlayerStandOnFoothold(gameStat.footholdObject, gameStat.playerLayer))
            {
                Vector3 footholdPos = FootholdPos(
                    gameStat.player.transform.position,
                    gameStat.playerMouseVector,
                    gameStat.footholdSetDistance,
                    gameStat.playerMoveRayHitLayer
                    );

                if ((footholdPos - gameStat.player.transform.position).magnitude < 2) return;

                FootholdSet(gameStat.footholdObject, footholdPos);
                Debug.Log("Foothold");
            }
        }
    }

    private Vector3 FootholdPos(Vector3 _originPos, Vector3 _directionVec, float _maxDistance, int _rayHitMask)
    {
        //当たらない
        if (!Physics.Raycast(_originPos, _directionVec, out RaycastHit hitInfo, _maxDistance, _rayHitMask))
        {
            //マウスの先
            //Debug.Log(mouseVec);
            return _originPos + new Vector3(_directionVec.x * _maxDistance, _directionVec.y, _directionVec.z * _maxDistance);
        }
        else
        {
            //できる
            return hitInfo.point;
        }
    }

    private void FootholdSet(GameObject _foothold, Vector3 _pos)
    {
        _foothold.transform.position = _pos;
    }

    //特定のオブジェクトと接しているか
    private bool isPlayerStandOnFoothold(GameObject _foothold, LayerMask _playerMask)
    {
        Collider[] cols = Physics.OverlapBox(
            _foothold.transform.position,
            _foothold.transform.localScale / 2,
            Quaternion.identity,
            _playerMask
            );

        bool isStand = false;

        if (cols.Length > 0)
        {
            isStand = true;
        }
        else
        {
            isStand = false;
        }

        return isStand;
    }
}
