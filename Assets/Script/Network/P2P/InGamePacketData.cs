﻿using UnityEngine;
using System.Collections;

public enum InGamePacketID
{
    CreateUnit = 20,            // 유닛 생성
    UnitSetDestination,         // 유닛 목적지 설정
    UnitImmediatelyMove,        // 유닛 즉시 이동
    UnitSetTarget,              // 유닛 타겟 설정
    UnitAttack,                 // 유닛 공격
        
    UnitAddSKill,               // 스킬 추가
    UnitCastSkill,              // 스킬 시전
    UnitStop,                   // 정지
    UnitLevelUp,                // 레벨업
    UnitDamaged,                // 피해입음

    UnitDeath,                   // 죽음
    UnitInterpolation           // 유닛 보간 패킷 ( 위치, 회전값 )
}

public struct UnitIdentity // 유닛 정체성
{
    public byte unitOwner;  // 유닛 주인
    public byte unitId;      // 인스턴스 유닛의 ID
}

public class InGameCreateUnitData // 유닛생성 데이터
{
    public UnitIdentity identity; // 유닛 소유, ID
    public byte unitType;         // 유닛 종류
    public Vector3 position;      // 생성위치
    public byte level;            // 유닛 레벨
}

public class InGameUnitSetDestinationData // 유닛 목적지 설정
{
    public UnitIdentity identity;    
    public Vector3 destination;// 목적지
    public Vector3 currentPosition; // 현재 위치
    public Vector3 forward;         // 바라보고 있는 방향
}

public class InGameUnitSetTargetData // 목표 유닛 설정
{
    public UnitIdentity identitySource; // 현재 유닛
    public UnitIdentity identityTarget; // 목표 유닛
    public Vector3 currentPosition;     // 현재위치
    public Vector3 forward;             // 바라보는 방향
}

public class InGameUnitImmediatelyMoveData // 유닛 즉시 이동
{
    public UnitIdentity identity;       
    public Vector3 destination;         // 목적지
    public Vector3 forward;             // 바라보는 방향
}

public class InGameUnitAttackData // 목표 공격
{
    public UnitIdentity identitySource;         // 명령을 받느는 유닛
    public UnitIdentity identityTarget;         // 타겟 유닛
    public Vector3 currentPosition;             // 현재 위치
    public Vector3 forward;                     // 방향
}
public class InGameUnitAddSkillData
{
    public UnitIdentity identity;
    public byte skillid;    
}
public class InGameUnitCastSkillData // 스킬 시전
{
    public UnitIdentity identity;
    public Vector3 currentPosition;         // 현재 위치
    public Vector3 forward;                 // 바라보는 방향
    public byte skillIndex;                 // 배운 스킬의 인덱스
    public Skill.Type type;                 // 타입 - 즉시발동, 타겟위치, 타겟 유닛...
    public Vector3 destination;             // 타격 위치
    public UnitIdentity identityTarget;     // 타격 유닛

}

public class InGameUnitStopData     // 유닛 스탑
{
    public UnitIdentity identity;
    public Vector3 currentPosition;         // 현재 위치
    public Vector3 forward;                 // 바라보는 방향
}

public class InGameUnitLevelUpData  // 레벨업
{
    public UnitIdentity identity;    
    public byte level;
}

public class InGameUnitDamagedData  // 유닛 피해 입음
{
    public UnitIdentity identity;    
    public float damage;
}
public class InGameUnitDeathData    // 유닛 사망
{
    public UnitIdentity identity;      
}

public class InGameUnitInterpolationData // 유닛 보간용도 패킷
{
    public UnitIdentity identity;   // 유닛 id
    public Vector3 position;        // 유닛 위치
    public Vector3 forward;         // 유닛이 바라보는 방향
}