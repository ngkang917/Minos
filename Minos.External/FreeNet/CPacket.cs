using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeNet
{
    /// <summary>
    /// byte[] 버퍼를 참조로 보관하여 pop_xxx 매소드 호출 순서대로 데이터 변환을 수행한다.
    /// </summary>
    public class CPacket
    {
        public CUserToken owner { get; private set; }

        public byte[] Buffer { get; private set; }
        public int Position { get; private set; }
        public int Size { get; private set; }

        // PACKET 규격 20200723
        public string MIS_START_CODE { get; private set; }
        public string MIS_MAC_ADDRESS { get; private set; }
        public ushort MIS_FW_VER { get; private set; }
        public ushort MIS_DB_VER { get; private set; }
        public ushort MIS_CMD { get; private set; }
        public uint MIS_SEND_DATA_SIZE { get; private set; }
        public ushort MIS_DATA_SOCK_NO { get; private set; }
        public ushort MIS_DATA_CNT_N { get; private set; }
        public byte[] MIS_DATA_N { get; private set; }

        public static CPacket Create(ushort mis_cmd)
        {
            CPacket packet = new CPacket();
            //todo:다음 리팩토링 대상은 바로 여기다. CPacketBufferManager!!!
            //CPacket packet = CPacketBufferManager.pop();
            packet.set_protocol(mis_cmd);
            return packet;
        }

        public static CPacket Create(byte[] buffer)
        {
            CPacket packet = new CPacket();
            packet.Buffer = buffer;
            packet.Position = buffer.Length;
            return packet;
        }

        public static void Destroy(CPacket packet)
        {
            //CPacketBufferManager.push(packet);
        }

        public CPacket(ArraySegment<byte> buffer, CUserToken owner)
        {
            // 참조로만 보관하여 작업한다.
            // 복사가 필요하면 별도로 구현해야 한다.
            this.Buffer = buffer.Array;

            // 버퍼 카운트로 사이즈 확인
            this.Size = Buffer.Length;
            this.MIS_START_CODE = Encoding.UTF8.GetString(this.Buffer, 0, 4);
            this.MIS_MAC_ADDRESS = Encoding.UTF8.GetString(this.Buffer, 4, 12);
            this.MIS_FW_VER = BitConverter.ToUInt16(this.Buffer, 16);
            this.MIS_DB_VER = BitConverter.ToUInt16(this.Buffer, 18);
            this.MIS_CMD = BitConverter.ToUInt16(this.Buffer, 20);
            this.MIS_SEND_DATA_SIZE = BitConverter.ToUInt16(this.Buffer, 22);
            // 데이터 전달 시 받을 버퍼 생성
            if (Size > 26)
            {
                this.MIS_DATA_SOCK_NO = BitConverter.ToUInt16(this.Buffer, 26);
                this.MIS_DATA_CNT_N = BitConverter.ToUInt16(this.Buffer, 28);
                this.MIS_DATA_N = new byte[MIS_DATA_CNT_N];
                for (int i = 0; i < MIS_DATA_CNT_N; i++)
                {
                    this.MIS_DATA_N[i] = this.Buffer[30 + i];
                }
            }

            this.owner = owner;
        }

        public CPacket(byte[] buffer, CUserToken owner)
        {
            // 참조로만 보관하여 작업한다.
            // 복사가 필요하면 별도로 구현해야 한다.
            this.Buffer = buffer;

            // 버퍼 카운트로 사이즈 확인
            this.Size = Buffer.Length;
            this.MIS_START_CODE = Encoding.UTF8.GetString(this.Buffer, 0, 4);
            this.MIS_MAC_ADDRESS = Encoding.UTF8.GetString(this.Buffer, 4, 12);
            this.MIS_FW_VER = BitConverter.ToUInt16(this.Buffer, 16);
            this.MIS_DB_VER = BitConverter.ToUInt16(this.Buffer, 18);
            this.MIS_CMD = BitConverter.ToUInt16(this.Buffer, 20);
            this.MIS_SEND_DATA_SIZE = BitConverter.ToUInt16(this.Buffer, 22);
            if (Size > 26)
            {
                this.MIS_DATA_SOCK_NO = BitConverter.ToUInt16(this.Buffer, 26);
                this.MIS_DATA_CNT_N = BitConverter.ToUInt16(this.Buffer, 28);
                this.MIS_DATA_N = new byte[MIS_DATA_CNT_N];
                for (int i = 0; i < MIS_DATA_CNT_N; i++)
                {
                    this.MIS_DATA_N[i] = this.Buffer[30 + i];
                }
            }


            this.owner = owner;
        }


        public CPacket()
        {
            this.Buffer = new byte[1024];
        }

        public Int16 pop_protocol_id()
        {
            return pop_int16();
        }
        public Int16 pop_int16()
        {
            Int16 data = BitConverter.ToInt16(this.Buffer, this.Position);
            this.Position += sizeof(Int16);
            return data;
        }

        public void copy_to(CPacket target)
        {
            target.set_protocol(this.MIS_CMD);
            target.overwrite(this.Buffer, this.Position);
        }

        public void overwrite(byte[] source, int position)
        {
            Array.Copy(source, this.Buffer, source.Length);
            this.Position = position;
        }

        public byte pop_byte()
        {
            byte data = this.Buffer[this.Position];
            this.Position += sizeof(byte);
            return data;
        }

        public ushort pop_ushort()
        {
            ushort data = BitConverter.ToUInt16(this.Buffer, this.Position);
            this.Position += sizeof(ushort);
            return data;
        }

        public uint pop_uint()
        {
            uint data = BitConverter.ToUInt32(this.Buffer, this.Position);
            this.Position += sizeof(uint);
            return data;
        }

        public string pop_start_code()
        {
            // 문자열 길이는 최대 2바이트 까지. 0 ~ 32767
            int len = BitConverter.ToInt32(this.Buffer, this.Position);
            this.Position += sizeof(int);

            // 인코딩은 utf8로 통일한다.
            string data = System.Text.Encoding.UTF8.GetString(this.Buffer, this.Position, len);
            this.Position += len;

            return data;
        }

        public float pop_float()
        {
            float data = BitConverter.ToSingle(this.Buffer, this.Position);
            this.Position += sizeof(float);
            return data;
        }



        public void set_protocol(ushort protocol_id)
        {
            this.MIS_CMD = protocol_id;
            //this.buffer = new byte[1024];

            // 헤더는 나중에 넣을것이므로 데이터 부터 넣을 수 있도록 위치를 점프시켜놓는다.
            this.Position = Defines.HEADERSIZE;

            push_ushort(protocol_id);
        }

        public void record_size()
        {
            // header + body 를 합한 사이즈를 입력한다.
            byte[] header = BitConverter.GetBytes(this.Position);
            header.CopyTo(this.Buffer, 0);
        }

        public void push_ushort(ushort data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += temp_buffer.Length;
        }

        public void push(byte data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += sizeof(byte);
        }

        public void push(Int16 data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += temp_buffer.Length;
        }

        public void push(Int32 data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += temp_buffer.Length;
        }

        public void push(string data)
        {
            byte[] temp_buffer = Encoding.UTF8.GetBytes(data);

            Int16 len = (Int16)temp_buffer.Length;
            byte[] len_buffer = BitConverter.GetBytes(len);
            len_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += sizeof(Int16);

            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += temp_buffer.Length;
        }

        public void push(float data)
        {
            byte[] temp_buffer = BitConverter.GetBytes(data);
            temp_buffer.CopyTo(this.Buffer, this.Position);
            this.Position += temp_buffer.Length;
        }
    }
}
