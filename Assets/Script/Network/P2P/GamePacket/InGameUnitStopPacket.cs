﻿using UnityEngine;
using System.Collections;

public class InGameUnitStopPacket : IPacket<InGameUnitStopData>
{
    public class InGameUnitStopSerializer : Serializer
    {
        public bool Serialize(InGameUnitStopData data)
        {
            bool ret = true;

            ret &= Serialize(data.identity.unitOwner);
            ret &= Serialize(data.identity.unitId);

            ret &= Serialize(data.currentPosition.x);
            ret &= Serialize(data.currentPosition.y);
            ret &= Serialize(data.currentPosition.z);

            ret &= Serialize(data.forward.x);
            ret &= Serialize(data.forward.y);
            ret &= Serialize(data.forward.z);



            return ret;
        }

        public bool Deserialize(ref InGameUnitStopData element)
        {
            if (GetDataSize() == 0)
            {
                // 데이터가 설정되지 않았다.
                return false;
            }

            bool ret = true;

            ret &= Deserialize(ref element.identity.unitOwner);
            ret &= Deserialize(ref element.identity.unitId);

            ret &= Deserialize(ref element.currentPosition.x);
            ret &= Deserialize(ref element.currentPosition.y);
            ret &= Deserialize(ref element.currentPosition.z);

            ret &= Deserialize(ref element.forward.x);
            ret &= Deserialize(ref element.forward.y);
            ret &= Deserialize(ref element.forward.z);
            return ret;
        }
    }
    InGameUnitStopData m_data;

    public InGameUnitStopPacket(InGameUnitStopData data)
    {
        m_data = data;
    }

    public InGameUnitStopPacket(byte[] data) // 패킷을 데이터로 변환(수신용)
    {
        InGameUnitStopSerializer serializer = new InGameUnitStopSerializer();
        serializer.SetDeserializedData(data);
        m_data = new InGameUnitStopData();
        serializer.Deserialize(ref m_data);
    }

    public byte[] GetPacketData() // 바이트형 패킷(송신용)
    {
        InGameUnitStopSerializer serializer = new InGameUnitStopSerializer();
        serializer.Serialize(m_data);
        return serializer.GetSerializedData();
    }

    public InGameUnitStopData GetData()
    {
        return m_data;
    }

    public int GetPacketId()
    {
        return (int)InGamePacketID.UnitStop;
    }
}
