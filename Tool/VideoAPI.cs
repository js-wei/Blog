using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public class VideoAPI
    {
        #region 引入资源
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        private static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cpName, byte[] lpszVer, int cpVer);
        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
        #endregion

        #region 消息常量(向窗口发送消息的指令)
        //消息常量 -------------------------------------------- 
        public const int WM_USER = 0x400;
        public const int WM_START = 0x400;    //此并非摄像头消息0x400表示的就是1024 
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int SWP_NOMOVE = 0x2;
        public const int SWP_NOZORDER = 0x4;
        public const int WM_CAP_GET_CAPSTREAMPTR = WM_START + 1;
        public const int WM_CAP_SET_CALLBACK_ERROR = WM_START + 2;//设置收回错误
        public const int WM_CAP_SET_CALLBACK_STATUS = WM_START + 3;//设置收回状态
        public const int WM_CAP_SET_CALLBACK_YIELD = WM_START + 4;//设置收回出产
        public const int WM_CAP_SET_CALLBACK_FRAME = WM_START + 5;//设置收回结构
        public const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_START + 6;//设置收回视频流
        public const int WM_CAP_SET_CALLBACK_WAVESTREAM = WM_START + 7;//设置收回视频波流
        public const int WM_CAP_GET_USER_DATA = WM_START + 8;//获得使用者数据
        public const int WM_CAP_SET_USER_DATA = WM_START + 9;//设置使用者数据
        public const int WM_CAP_DRIVER_CONNECT = WM_START + 10;//驱动程序连接
        public const int WM_CAP_DRIVER_DISCONNECT = WM_START + 11;//断开启动程序连接
        public const int WM_CAP_DRIVER_GET_NAME = WM_START + 12;//获得驱动程序名字
        public const int WM_CAP_DRIVER_GET_VERSION = WM_START + 13;//获得驱动程序版本
        public const int WM_CAP_DRIVER_GET_CAPS = WM_START + 14;//获得驱动程序帽子
        public const int WM_CAP_FILE_SET_CAPTURE_FILE = WM_START + 20;//设置捕获文件
        public const int WM_CAP_FILE_GET_CAPTURE_FILE = WM_START + 21;//获得捕获文件
        public const int WM_CAP_FILE_ALLOCATE = WM_START + 22;//分派文件
        public const int WM_CAP_FILE_SAVEAS = WM_START + 23;//另存文件为
        public const int WM_CAP_FILE_SET_INFOCHUNK = WM_START + 24;//设置开始文件
        public const int WM_CAP_FILE_SAVEDIB = WM_START + 25;//保存文件

        public const int WM_CAP_SAVEDIB = WM_START + 25;//保存文件
        public const int WM_CAP_EDIT_COPY = WM_START + 30;//编辑复制
        public const int WM_CAP_SET_AUDIOFORMAT = WM_START + 35;//设置音频格式
        public const int WM_CAP_GET_AUDIOFORMAT = WM_START + 36;//捕获音频格式
        public const int WM_CAP_DLG_VIDEOFORMAT = WM_START + 41;//1065 打开视频格式设置对话框
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_START + 42;//1066 打开属性设置对话框，设置对比度亮度等
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_START + 43;//1067 打开视频显示
        public const int WM_CAP_GET_VIDEOFORMAT = WM_START + 44;//1068 获得视频格式
        public const int WM_CAP_SET_VIDEOFORMAT = WM_START + 45;//1069 设置视频格式
        public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_START + 46;//1070 打开压缩设置对话框
        public const int WM_CAP_SET_PREVIEW = WM_START + 50;//设置预览
        public const int WM_CAP_SET_OVERLAY = WM_START + 51;//设置覆盖
        public const int WM_CAP_SET_PREVIEWRATE = WM_START + 52;//设置预览比例
        public const int WM_CAP_SET_SCALE = WM_START + 53;//设置刻度
        public const int WM_CAP_GET_STATUS = WM_START + 54;//获得状态
        public const int WM_CAP_SET_SCROLL = WM_START + 55;//设置卷
        public const int WM_CAP_GRAB_FRAME = WM_START + 60;//逮捕结构
        public const int WM_CAP_GRAB_FRAME_NOSTOP = WM_START + 61;//停止逮捕结构
        public const int WM_CAP_SEQUENCE = WM_START + 62;//次序
        public const int WM_CAP_SEQUENCE_NOFILE = WM_START + 63;//使用WM_CAP_SEUENCE_NOFILE消息（capCaptureSequenceNoFile宏），可以不向磁盘文件写入数据。该消息仅在配合回调函数时有用，它允许你的应用程序直接使用音视频数据。
        public const int WM_CAP_SET_SEQUENCE_SETUP = WM_START + 64;//设置安装次序
        public const int WM_CAP_GET_SEQUENCE_SETUP = WM_START + 65;//获得安装次序
        public const int WM_CAP_SET_MCI_DEVICE = WM_START + 66;//设置媒体控制接口
        public const int WM_CAP_GET_MCI_DEVICE = WM_START + 67;//获得媒体控制接口
        public const int WM_CAP_STOP = WM_START + 68;//停止
        public const int WM_CAP_ABORT = WM_START + 69;//异常中断
        public const int WM_CAP_SINGLE_FRAME_OPEN = WM_START + 70;//打开单一的结构
        public const int WM_CAP_SINGLE_FRAME_CLOSE = WM_START + 71;//关闭单一的结构
        public const int WM_CAP_SINGLE_FRAME = WM_START + 72;//单一的结构
        public const int WM_CAP_PAL_OPEN = WM_START + 80;//打开视频
        public const int WM_CAP_PAL_SAVE = WM_START + 81;//保存视频
        public const int WM_CAP_PAL_PASTE = WM_START + 82;//粘贴视频
        public const int WM_CAP_PAL_AUTOCREATE = WM_START + 83; //自动创造
        public const int WM_CAP_PAL_MANUALCREATE = WM_START + 84;//手动创造
        public const int WM_CAP_SET_CALLBACK_CAPCONTROL = WM_START + 85;// 设置收回的错误
        #endregion 消息常量

        #region 操作类
        public class cVideo
        {
            private IntPtr lwndC;			//保存无符号句柄
            private IntPtr mControlPtr;		//保存管理指示器
            private int mWidth;
            private int mHeight;

        
            public cVideo(IntPtr handle, int width, int height)
            {
                mControlPtr = handle;
                mWidth = width;
                mHeight = height;
            }
            /// <summary>
            /// 打开设备
            /// </summary>
            public void StartWebCam()
            {
                byte[] lpszName = new byte[100];
                byte[] lpszVer = new byte[100];
                VideoAPI.capGetDriverDescriptionA(0, lpszName, 100, lpszVer, 100);
                this.lwndC = VideoAPI.capCreateCaptureWindowA(lpszName, VideoAPI.WS_CHILD | VideoAPI.WS_VISIBLE, 0, 0, mWidth, mHeight, mControlPtr, 0);
                if (VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_DRIVER_CONNECT, 0, 0))
                {
                    VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SET_PREVIEWRATE, 100, 0);
                    VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SET_PREVIEW, true, 0);
                }

            }
            /// <summary>
            /// 关闭设备
            /// </summary>
            public void CloseWebCam()
            {
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_DRIVER_DISCONNECT, 0, 0);
            }
			/// <summary>
			/// 拍照
			/// </summary>
			/// <param name="hWndC">句柄</param>
			/// <param name="path">保存bmp文件路径</param>
			public void GrabImage(IntPtr hWndC,string path)
			{
				IntPtr hBmp=Marshal.StringToHGlobalAnsi(path);
                VideoAPI.SendMessage(lwndC,VideoAPI.WM_CAP_SAVEDIB,0,hBmp.ToInt32());
			}
            /// <summary>
            /// 开始录像
            /// </summary>
            /// <param name="path"></param>
            public void StartRecording(string path)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_FILE_SET_CAPTURE_FILE, 0, hBmp.ToInt32());
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SEQUENCE, 0, 0);
            }
            /// <summary>
            /// 停止录像
            /// </summary>
            public void CloseRecording()
            {
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_STOP, 0, 0);
            }

        }
        #endregion

    }


}
