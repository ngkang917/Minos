﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeNet
{
    class Defines
    {
        public static readonly int MESSAGERSIZE = 0;
    }

    public delegate void CompletedMessageCallback(ArraySegment<byte> buffer);

    /// <summary>
    /// [header][body] 구조를 갖는 데이터를 파싱하는 클래스.
    /// - header : 데이터 사이즈. Defines.HEADERSIZE에 정의된 타입만큼의 크기를 갖는다.
    ///				2바이트일 경우 Int16, 4바이트는 Int32로 처리하면 된다.
    ///				본문의 크기가 Int16.Max값을 넘지 않는다면 2바이트로 처리하는것이 좋을것 같다.
    /// - body : 메시지 본문.
    /// </summary>
    class CMessageResolver
    {
        // Receive 된 메시지 저장 용 버퍼
        byte[] full_message_buffer;

        // 메시지 전체 사이즈
        int message_size;
        // 현재 포인터 위치 용, 사용 후 0으로 변경
        int current_position;
        // 읽어와야 할 위치 
        int position_to_read_position;
        // 남은 사이즈
        int remain_size;

        public CMessageResolver()
        {
            this.message_size = 0;
            this.current_position = 0;
            this.position_to_read_position = 0;
            this.remain_size = 0;
        }

        /// <summary>
        /// 소켓 버퍼로부터 데이터를 수신할 때 마다 호출된다.
        /// 데이터가 남아 있을 때 까지 계속 패킷을 만들어 callback을 호출 해 준다.
        /// 하나의 패킷을 완성하지 못했다면 버퍼에 보관해 놓은 뒤 다음 수신을 기다린다.
        /// </summary>
        /// <param name="buffer">받은 버퍼 데이터</param>
        /// <param name="offset">시작 위치</param>
        /// <param name="transffered">데이터 길이</param>
        public void on_receive(byte[] buffer, int offset, int transffered, CompletedMessageCallback callback)
        {
            byte[] lite_message_buffer = new byte[transffered];
            // 데이터 시작 위치
            int src_position = offset;
            // 읽을 데이터의 길이 
            this.remain_size = transffered;
            Array.Copy(buffer, offset, lite_message_buffer, 0, transffered);
            
            byte[] t = BitConverter.GetBytes(30U);


            // 남은 데이터가 있다면 계속 반복한다.
            while (this.remain_size > 0)
            {
                bool completed = false;

                //헤더만큼 못읽은 경우 헤더를 먼저 읽는다.
                if (this.current_position < Defines.MESSAGERSIZE)
                {

                    // 목표 지점 설정(헤더 위치까지 도달하도록 설정).
                    this.position_to_read_position = Defines.MESSAGERSIZE;

                    completed = read_until(buffer, ref src_position);

                    if (!completed)
                    {
                        return;
                    }

                    // 헤더 하나를 온전히 읽어왔으므로 메시지 사이즈를 구한다.
                    this.message_size = get_total_message_size();

                    // 메시지 사이즈가 0이하라면 잘못된 패킷으로 처리한다.
                    if (this.message_size <= 0)
                    {
                        clear_buffer();
                        return;
                    }

                    // 다음 목표 지점.
                    this.position_to_read_position = this.message_size;

                    // 헤더를 다 읽었는데 더이상 가져올 데이터가 없다면 다음 receive를 기다린다.
                    // (예를들어 데이터가 조각나서 헤더만 오고 메시지는 다음번에 올 경우)
                    if (this.remain_size <= 0)
                    {
                        return;
                    }
                }

                // 메시지를 읽는다.
                completed = read_until(buffer, ref src_position);

                if (completed)
                {
                    // 패킷 하나를 완성 했다.
                    byte[] clone = new byte[this.position_to_read_position];
                    Array.Copy(this.full_message_buffer, clone, this.position_to_read_position);
                    clear_buffer();
                    callback(new ArraySegment<byte>(clone, 0, this.position_to_read_position));
                }
            }
        }

        /// <summary>
        /// 목표지점으로 설정된 위치까지의 바이트를 원본 버퍼로부터 복사한다.
        /// 데이터가 모자랄 경우 현재 남은 바이트 까지만 복사한다.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size_to_read"></param>
        /// <returns>다 읽었으면 true, 데이터가 모자라서 못 읽었으면 false를 리턴한다.</returns>
        bool read_until(byte[] buffer, ref int src_position)
        {
            // 읽어와야 할 바이트.
            // 데이터가 분리되어 올 경우 이전에 읽어놓은 값을 빼줘서 부족한 만큼 읽어올 수 있도록 계산해 준다.
            int copy_size = this.position_to_read_position - this.current_position;

            // 앗! 남은 데이터가 더 적다면 가능한 만큼만 복사한다.
            if (this.remain_size < copy_size)
            {
                copy_size = this.remain_size;
            }

            // 버퍼에 복사.
            Array.Copy(buffer, src_position, this.full_message_buffer, this.current_position, copy_size);

            // 원본 버퍼 포지션 이동.
            src_position += copy_size;

            // 타겟 버퍼 포지션도 이동.
            this.current_position += copy_size;

            // 남은 바이트 수.
            this.remain_size -= copy_size;

            // 목표지점에 도달 못했으면 false
            if (this.current_position < this.position_to_read_position)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 헤더 + 바디 사이즈를 구한다.
        /// 패킷 헤더부분에 이미 전체 메시지 사이즈가 계산되어 있으므로 헤더 크기에 맞게 변환만 시켜주면 된다.
        /// </summary>
        /// <returns></returns>
        int get_total_message_size()
        {
            // 512 byte를 넘기지 않을 예정
            return BitConverter.ToInt32(this.full_message_buffer, 0);
        }

        public void clear_buffer()
        {
            Array.Clear(this.full_message_buffer, 0, this.full_message_buffer.Length);

            this.current_position = 0;
            this.message_size = 0;

        }
    }
}
