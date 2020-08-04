using System;
using System.Collections.Generic;
using System.Text;

namespace Minos.SocketServer.V1.Common
{
    class Common
    {
        /// <summary>
        /// 문자 정보 Byte 배열에 설정
        /// </summary>
        /// <param name="buffer">저장하기 위한 버퍼</param>
        /// <param name="point_tarket_position">배열 위치 값</param>
        /// <param name="value">문자 정보</param>
        /// <param name="strLength">문자 길이</param>
        public static void SetBufferByString(ref byte[] buffer, ref uint point_tarket_position, string value, int strLength)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = Encoding.GetEncoding("ksc_5601").GetBytes(value);
            int length = bytes.Length;
            Buffer.BlockCopy((Array)bytes, 0, (Array)buffer, (int)point_tarket_position, length);
            point_tarket_position += (uint)length;
            for (int index = length; index < strLength; ++index)
                buffer[(int)point_tarket_position++] = (byte)0;
        }

        /// <summary>
        /// ushort 타입 정수를 버퍼에 저장
        /// </summary>
        /// <param name="buffer">저장하기 위한 버퍼</param>
        /// <param name="point_tarket_position">배열 위치 값</param>
        /// <param name="value">ushort</param>
        public static void SetBufferByUInt16(ref byte[] buffer, ref uint point_tarket_position, ushort value)
        {
            buffer[(int)point_tarket_position++] = (byte)value;
            buffer[(int)point_tarket_position++] = (byte)((uint)value >> 8);
        }

        /// <summary>
        /// uint 타입 정수를 버퍼에 저장
        /// </summary>
        /// <param name="buffer">저장하기 위한 버퍼</param>
        /// <param name="point_tarket_position">배열 위치 값</param>
        /// <param name="value">uint</param>
        public static void SetBufferByUInt32(ref byte[] buffer, ref uint point_tarket_position, uint value)
        {
            SetBufferByUInt16(ref buffer, ref point_tarket_position, (ushort)value);
            SetBufferByUInt16(ref buffer, ref point_tarket_position, (ushort)(value >> 16));
        }
    }
}
