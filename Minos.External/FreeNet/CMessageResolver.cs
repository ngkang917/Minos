using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeNet
{
    class Defines
    {
        public static readonly int HEADERSIZE = 26;
    }

    public delegate void CompletedMessageCallback(ArraySegment<byte> buffer);

    /// <summary>
    /// [header][body] 구조를 갖는 데이터를 파싱하는 클래스.
    /// - header : 헤더는 26바이트의 길이를 가진다.
    ///   [Start_Code]
    ///   [MAC_Address]
    ///   [FW_Version]
    ///   [DB_Version]
    ///   [CMD]
    ///   [Send_Data_Size]
    /// - body : 메시지 본문으로 데이터를 주고 받는다.
    ///   [DATA_Sock_No]
    ///   [DATA_Cnt_n]
    ///   [Data(n)]
    /// </summary>
    class CMessageResolver
    {
        // 진행중인 버퍼.
        byte[] message_buffer = new byte[1024];
        // 메시지 사이즈.
        int message_size;
        // 현재 버퍼의 포인터 위치, 사용 완료 후 초기화 0
        int current_position;
        // 읽을 위치까지의 거리
        int position_to_read;
        // 전달 된 버퍼 길이
        int transffered_length;

        public CMessageResolver()
        {
            this.message_size = 0;
            this.current_position = 0;
            this.position_to_read = 0;
            this.transffered_length = 0;
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
            /* 1. transffered는 클라이언트에서 보내는 길이
             * 2. Data Length는 중간에 받는 위치 값이 있음
             * 3. 조건
             *    a. 데이터가 정상적으로 잘 넘어 온 경우
             *    b. 데이터가 비상적으로 넘어 온 경우
             *    c. 데이터가 짧게 짤려서 넘어오는 경우
             */   
            this.transffered_length = Convert.ToInt32(transffered);     // 전달 된 길이 저장
            int src_position = offset;                                  // 시작 위치 저장

            // 짤려서 올 경우를 대비해서 반복 처리
            while (this.transffered_length > 0)
            {
                bool completed = false;

                // 1. 현재 위치가 헤더보다 작을 경우
                if (this.current_position < Defines.HEADERSIZE)     
                {
                    this.position_to_read = Defines.HEADERSIZE;    

                    completed = read_until(buffer, ref src_position);
                    if (!completed)
                    {
                        // 아직 다 못읽었으므로 다음 receive를 기다린다.
                        return;
                    }

                    // 못해도 헤더는 다 읽음.
                    this.message_size = get_total_message_size();

                    // 메시지 사이즈가 0이하라면 잘못된 패킷으로 처리한다.
                    // It was wrong message if size less than zero.
                    if (this.message_size <= 0)
                    {
                        clear_buffer();
                        return;
                    }

                    // 다음 목표 지점은 메시지 사이즈 만큼
                    this.position_to_read = this.message_size;

                    // 헤더를 다 읽었는데 더이상 가져올 데이터가 없다면 다음 receive를 기다린다.
                    // (예를들어 데이터가 조각나서 헤더만 오고 메시지는 다음번에 올 경우)
                    if (this.transffered_length <= 0)
                    {
                        return;
                    }
                }

                // 메시지를 읽는다.
                completed = read_until(buffer, ref src_position);

                if (completed)
                {
                    // 패킷 하나를 완성 했다.
                    byte[] clone = new byte[this.position_to_read];
                    Array.Copy(this.message_buffer, clone, this.position_to_read);
                    clear_buffer();
                    callback(new ArraySegment<byte>(clone, 0, this.position_to_read));
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
            // 읽어야 하는 길이 계산(읽을 위치 - 현재 위치)
            int copy_size = this.position_to_read - this.current_position;

            // 전달 받은 데이터가 읽어야 하는 길이보다 작은 경우, 읽어야 하는 길이는 넘어온 길이로 변경
            if (this.transffered_length < copy_size)
            {
                copy_size = this.transffered_length;
            }

            // 위 기준에 따라 라이트 버퍼에 정보를 저장
            Array.Copy(buffer, src_position, this.message_buffer, this.current_position, copy_size);

            // 그리고 현재 위치 정보는 읽어야 하는 길이만큼 읽었으므로 읽은 길이 만큼 이동
            src_position += copy_size;

            // 현재 위치 포지션도 이동.
            this.current_position += copy_size;

            // 남아 있는 길이 정보 변경
            this.transffered_length -= copy_size;

            // 목표지점에 도달 못했으면 false
            if (this.current_position < this.position_to_read)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 헤더+바디 사이즈를 구한다.
        /// 패킷 헤더부분에 이미 전체 메시지 사이즈가 계산되어 있으므로 헤더 크기에 맞게 변환만 시켜주면 된다.
        /// </summary>
        /// <returns></returns>
        int get_total_message_size()
        {
            return BitConverter.ToInt32(this.message_buffer, 22);
        }

        public void clear_buffer()
        {
            Array.Clear(this.message_buffer, 0, this.message_buffer.Length);

            this.current_position = 0;
            this.message_size = 0;

        }
    }
}
