using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools.Plugins.UIEditor
{

    public class ChatUI
    {
        private int width = 325;
        private int height = 478;

        public ChatUI(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        // 表示枠サイズ
        public int iViewDimX
        {
            get
            {
                return iStatViewDimX + 2;
            }
        }
        public int iViewDimY
        {
            get
            {
                //（固定値/不変）
                return 22;
            }
        }

        // 通常モード時の文字入力枠
        public int iEditBoxDimX
        {
            get
            {
                return iStatViewDimX - 74;
            }
        }
        public int iEditBoxDimY
        {
            get
            {
                //（固定値/不変）
                return 18;
            }
        }
        public int iEditBoxCoordX
        {
            //（固定値/不変/左辺を0としてみた時の横の位置）
            get
            {
                return 58;
            }
        }
        public int iEditBoxCoordY
        {
            //（固定値/不変/底辺を0としてみた時の高さの位置）
            get
            {
                return 3;
            }
        }

        // whisperモード時の文字入力枠
        public int iEditBoxMinDimX
        {
            get
            {
                return iStatViewDimX - iEditBoxMinCoordX - 16;
            }
        }
        public int iEditBoxMinDimY
        {
            //（固定値/不変）
            get
            {
                return 18;
            }
        }
        public int iEditBoxMinCoordX
        {
            get
            {
                //（/左辺を0としてみた時の横の位置/右下の点）
                return ((iStatViewDimX - (iComboBoxDimX + iComboBoxCoordX + 2)) - iStatViewDimX) * -1;
            }
        }
        public int iEditBoxMinCoordY
        {
            get
            {
                //（固定値/不変/底辺を0としてみた時の高さの位置/右下の点）
                return 3;
            }
        }

        // whisperモード時のコンボボックスサイズ
        public int iComboBoxDimX
        {
            get
            {
                return 116;
            }
        }
        public int iComboBoxDimY
        {
            get
            {
                //（固定値/不変）
                return 18;
            }
        }
        public int iComboBoxCoordX
        {
            get
            {
                //（固定値/不変）
                return 58;
            }
        }
        public int iComboBoxCoordY
        {
            get
            {
                //（固定値/不変）
                return 3;
            }
        }


        // エモボタンの位置
        public int iEmoticonBtnCoordX
        {
            get
            {
                //（固定値/不変）
                return 5;
            }
        }
        public int iEmoticonBtnCoordY
        {
            get
            {
                //（固定値/不変）
                return 5;
            }
        }
        // メモボタンの位置
        public int iMemoBtnCoordX
        {
            get
            {
                //（固定値/不変）
                return 22;
            }
        }
        public int iMemoBtnCoordY
        {
            get
            {
                //（固定値/不変）
                return 5;
            }
        }
        // 囁きボタンの位置
        public int iWhisperBtnCoordX
        {
            get
            {
                //（固定値/不変）
                return 40;
            }
        }
        public int iWhisperBtnCoordY
        {
            get
            {
                //（固定値/不変）
                return 5;
            }
        }
        // 閉じるボタンの位置
        public int iCloseBtnCoordX
        {
            get
            {
                return iStatViewDimX - 15;
            }
        }
        public int iCloseBtnCoordY
        {
            get
            {
                //（固定値/不変）
                return 6;
            }
        }

        public int iStatViewDimX
        {
            get
            {
                return this.width;
            }
        }
        public int iStatViewDimY
        {
            get
            {
                //（固定値/不変）
                return 78;
            }
        }

        public int iStatViewMaxDimX
        {
            get
            {
                return iStatViewDimX;
            }
        }
        public int iStatViewMaxDimY
        {
            get
            {
                return this.height;
            }
        }

        public int iModeButtonDimX
        {
            get
            {
                //（固定値/不変）
                return 65;
            }
        }
        public int iModeButtonDimY
        {
            get
            {
                //（固定値/不変）
                return 18;
            }
        }

        public int iDummyModeButtonDimX
        {
            get
            {
                return iStatViewDimX - (iModeButtonDimX * 4);
            }
        }
        public int iDummyModeButtonDimY
        {
            get
            {
                //（固定値/不変）
                return 18;
            }
        }

    }
}
