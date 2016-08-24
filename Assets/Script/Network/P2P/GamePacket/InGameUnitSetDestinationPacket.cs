﻿using UnityEngine;
using System.Collections;

public class InGameUnitSetDestinationPacket : IPacket<InGameUnitSetDestinationData>
{
    public class InGameUnitMoveSerializer : Serializer
    {
        public bool Serialize(InGameUnitSetDestinationData data)
        {
            bool ret = true;
            ret &= Serialize(data.identity.unitOwner);
            ret &= Serialize(data.identity.unitId);
            ret &= Serialize(data.destination.x);
            ret &= Serialize(data.destination.y);
            ret &= Serialize(data.destination.z);
            
            return ret;
        }

        public bool Deserialize(ref InGameUnitSetDestinationData element)
        {
            if (GetDataSize() == 0)
            {
                // 데이터가 설정되지 않았다.
                return false;
            }

            bool ret = true;

            ret &= Deserialize(ref element.identity.unitOwner);
            ret &= Deserialize(ref element.identity.unitId);
            ret &= Deserialize(ref element.destination.x);
            ret &= Deserialize(ref element.destination.y);
            ret &= Deserialize(ref element.destination.z);

            return ret;
        }
    }
    InGameUnitSetDestinationData m_data;

    public InGameUnitSetDestinationPacket(InGameUnitSetDestinationData data) // 데이터로 초기화(송신용)
    {
        m_data = data;
    }

    public InGameUnitSetDestinationPacket(byte[] data) // 패킷을 데이터로 변환(수신용)
    {
        InGameUnitMoveSerializer serializer = new InGameUnitMoveSerializer();
        serializer.SetDeserializedData(data);
        m_data = new InGameUnitSetDestinationData();
        serializer.Deserialize(ref m_data);
    }

    public byte[] GetPacketData() // 바이트형 패킷(송신용)
    {
        InGameUnitMoveSerializer serializer = new InGameUnitMoveSerializer();
        serializer.Serialize(m_data);
        return serializer.GetSerializedData();
    }

    public InGameUnitSetDestinationData GetData() // 데이터 얻기(수신용)
    {
        return m_data;
    }

    public int GetPacketId()
    {
        return (int)InGamePacketID.UnitSetDestination;
    }
}