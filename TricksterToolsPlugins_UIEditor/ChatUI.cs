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

        // �\���g�T�C�Y
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
                //�i�Œ�l/�s�ρj
                return 22;
            }
        }

        // �ʏ탂�[�h���̕������͘g
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
                //�i�Œ�l/�s�ρj
                return 18;
            }
        }
        public int iEditBoxCoordX
        {
            //�i�Œ�l/�s��/���ӂ�0�Ƃ��Ă݂����̉��̈ʒu�j
            get
            {
                return 58;
            }
        }
        public int iEditBoxCoordY
        {
            //�i�Œ�l/�s��/��ӂ�0�Ƃ��Ă݂����̍����̈ʒu�j
            get
            {
                return 3;
            }
        }

        // whisper���[�h���̕������͘g
        public int iEditBoxMinDimX
        {
            get
            {
                return iStatViewDimX - iEditBoxMinCoordX - 16;
            }
        }
        public int iEditBoxMinDimY
        {
            //�i�Œ�l/�s�ρj
            get
            {
                return 18;
            }
        }
        public int iEditBoxMinCoordX
        {
            get
            {
                //�i/���ӂ�0�Ƃ��Ă݂����̉��̈ʒu/�E���̓_�j
                return ((iStatViewDimX - (iComboBoxDimX + iComboBoxCoordX + 2)) - iStatViewDimX) * -1;
            }
        }
        public int iEditBoxMinCoordY
        {
            get
            {
                //�i�Œ�l/�s��/��ӂ�0�Ƃ��Ă݂����̍����̈ʒu/�E���̓_�j
                return 3;
            }
        }

        // whisper���[�h���̃R���{�{�b�N�X�T�C�Y
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
                //�i�Œ�l/�s�ρj
                return 18;
            }
        }
        public int iComboBoxCoordX
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 58;
            }
        }
        public int iComboBoxCoordY
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 3;
            }
        }


        // �G���{�^���̈ʒu
        public int iEmoticonBtnCoordX
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 5;
            }
        }
        public int iEmoticonBtnCoordY
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 5;
            }
        }
        // �����{�^���̈ʒu
        public int iMemoBtnCoordX
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 22;
            }
        }
        public int iMemoBtnCoordY
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 5;
            }
        }
        // �����{�^���̈ʒu
        public int iWhisperBtnCoordX
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 40;
            }
        }
        public int iWhisperBtnCoordY
        {
            get
            {
                //�i�Œ�l/�s�ρj
                return 5;
            }
        }
        // ����{�^���̈ʒu
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
                //�i�Œ�l/�s�ρj
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
                //�i�Œ�l/�s�ρj
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
                //�i�Œ�l/�s�ρj
                return 65;
            }
        }
        public int iModeButtonDimY
        {
            get
            {
                //�i�Œ�l/�s�ρj
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
                //�i�Œ�l/�s�ρj
                return 18;
            }
        }

    }
}
