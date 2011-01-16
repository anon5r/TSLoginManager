using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using TricksterTools.Debug;

namespace TricksterTools.Plugins.ConfigExtra.Structures
{
    namespace Display
    {
        public class Display : IComparable
        {
            /// <summary>
            /// �K�i��
            /// </summary>
            private string GaugeName;

            /// <summary>
            /// ��
            /// </summary>
            private int DisplayWidth;

            /// <summary>
            /// ����
            /// </summary>
            private int DisplayHeight;


            /// <summary>
            /// �K�i��
            /// </summary>
            public string Gauge
            {
                get
                {
                    return this.GaugeName;
                }
            }

            /// <summary>
            /// ��
            /// </summary>
            public int Width
            {
                get
                {
                    return this.DisplayWidth;
                }
            }

            /// <summary>
            /// ����
            /// </summary>
            public int Height
            {
                get
                {
                    return this.DisplayHeight;
                }
            }

            public Display(int width, int height)
            {
                this.DisplayWidth = width;
                this.DisplayHeight = height;
            }

            public Display(int width, int height, string gauge)
            {
                this.DisplayWidth = width;
                this.DisplayHeight = height;
                this.GaugeName = gauge;
            }

            public int CompareTo(object obj)
            {
                int width = ((Display)obj).Width;
                int height = ((Display)obj).Height;

                if (Width < width)
                {
                    return -1;
                }
                else if (Width == width)
                {
                    if (Height < height)
                    {
                        return -1;
                    }
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            /// <summary>
            /// �I�u�W�F�N�g�̓��e�𕶎���ŕԂ��܂��B
            /// </summary>
            /// <returns>&lt;width&gt; x &lt;height&gt;</returns>
            public override string ToString()
            {
                string str = this.DisplayWidth + " x " + this.DisplayHeight;

                //if (this.GaugeName.Length > 0)
                //{
                //  str += " (" + this.GaugeName + ")";
                //}
                return str;
            }
        }

        
        class DisplayCollector
        {
            //private ArrayList displays = new ArrayList();

            private ArrayList currentDisplay = new ArrayList();

            public DisplayCollector()
            {
                /*
                // �f�B�X�v���C�}�b�v
                this.displays.Add(new Display(128, 96, "sQCIF"));
                this.displays.Add(new Display(176, 144, "QCIF"));
                this.displays.Add(new Display(320, 240, "QVGA"));
                this.displays.Add(new Display(400, 240, "WQVGA"));
                this.displays.Add(new Display(352, 288, "CIF"));
                this.displays.Add(new Display(640, 200, "CGA"));
                this.displays.Add(new Display(480, 320, "HVGA"));
                this.displays.Add(new Display(640, 350, "EGA"));
                this.displays.Add(new Display(640, 400, "DCGA"));
                this.displays.Add(new Display(640, 480, "VGA"));
                this.displays.Add(new Display(800, 480, "WVGA"));
                this.displays.Add(new Display(854, 480, "FWVGA"));
                this.displays.Add(new Display(864, 480, "FWVGA+"));
                this.displays.Add(new Display(800, 600, "SVGA"));
                this.displays.Add(new Display(1024, 480, "UWVGA"));
                this.displays.Add(new Display(1024, 576, "WSVGA"));
                this.displays.Add(new Display(1024, 600, "WSVGA"));
                this.displays.Add(new Display(1280, 600, "UWSVGA"));
                this.displays.Add(new Display(1024, 768, "XGA"));
                this.displays.Add(new Display(1280, 768, "WXGA"));
                this.displays.Add(new Display(1152, 864, "XGA+"));
                this.displays.Add(new Display(1280, 800, "WXGA"));
                this.displays.Add(new Display(1366, 768, "FWXGA"));
                this.displays.Add(new Display(1280, 960, "Quad VGA"));
                this.displays.Add(new Display(1440, 900, "WXGA+"));
                this.displays.Add(new Display(1280, 1024, "SXGA"));
                this.displays.Add(new Display(1600, 900, "WXGA++"));
                this.displays.Add(new Display(1400, 1050, "SXGA+"));
                this.displays.Add(new Display(1600, 1024, "WSXGA"));
                this.displays.Add(new Display(1680, 1050, "WSXGA+"));
                this.displays.Add(new Display(1600, 1200, "UXGA"));
                this.displays.Add(new Display(2048, 1080, "2K"));
                this.displays.Add(new Display(1920, 1080, "Full HD"));
                this.displays.Add(new Display(1920, 1200, "WUXGA"));
                this.displays.Add(new Display(2048, 1152, "QWXGA"));
                this.displays.Add(new Display(2048, 1536, "QXGA"));
                this.displays.Add(new Display(2304, 1728, "4M"));
                this.displays.Add(new Display(2560, 1600, "WQXGA"));
                this.displays.Add(new Display(3200, 2400, "QUXGA"));
                this.displays.Add(new Display(3840, 2160, "4x FullHD"));
                this.displays.Add(new Display(4096, 2160, "4K"));
                this.displays.Add(new Display(3840, 2400, "QUXGA Wide"));
                this.displays.Add(new Display(8192, 4320, "8K"));
                */

                // �g�p���Ă�����ɓK�����f�B�X�v���C�}�b�v���擾���܂��B
                Win32API.DISPLAY_DEVICE_MODE deviceMode = new Win32API.DISPLAY_DEVICE_MODE();

                //int mode = Win32API.ENUM_CURRENT_SETTINGS;
                int mode = 0;

                String deviceName = null;
                while (Win32API.EnumDisplaySettings(deviceName, mode, ref deviceMode) != 0)
                {
                    // ���ݗ��p���Ă���s�N�Z�����ibit)�ȊO�̓��X�g�̑ΏۊO�Ƃ���
                    if (deviceMode.dmBitsPerPel != System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel)
                    {
                        mode++;  continue;
                    }

                    int width = deviceMode.dmPelsWidth;
                    int height = deviceMode.dmPelsHeight;
                    int bit = deviceMode.dmBitsPerPel;
                    //int color = 1 <<< deviceMode.dmBitsPerPel;

                    // �Q�[���̉�ʐݒ�Œ�l 800 x 600 �ȉ��͕\�����Ȃ��i�ݒ肵�Ă��Q�[�����N���ł��Ȃ����߁j
                    if (width < 800 || height < 600)
                    {
                        mode++; continue;
                    }

                    if (this.isExistDisplay(width, height)) { mode++; continue; }
                    
                    //SimpleLogger.WriteLine("{0}, {1} ({2}) {3}bit", deviceMode.dmPelsWidth, deviceMode.dmPelsHeight, this.getGauge(deviceMode.dmPelsWidth, deviceMode.dmPelsHeight), deviceMode.dmBitsPerPel);
                    SimpleLogger.WriteLine("{0} x {1} {2}bit", width, height, bit);
                    this.add(deviceMode.dmPelsWidth, deviceMode.dmPelsHeight);
                    mode++;
                }

                this.currentDisplay.Sort();
            }

            public void add(int width, int height)
            {
                /*
                string gauge = this.getGauge(width, height);
                if (gauge == "undefined") return;
                */
                //this.currentDisplay.Add(new Display(width, height, gauge));
                this.currentDisplay.Add(new Display(width, height));
            }

            public ArrayList getMap()
            {
                return this.currentDisplay;
            }

            /// <summary>
            /// �w��̃f�B�X�v���C�T�C�Y�����ɒ�`����Ă�����̂�
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public bool isExistDisplay(int width, int height)
            {
                foreach (Display display in this.currentDisplay)
                {
                    if (display.Width == width && display.Height == height)
                    {
                        return true;
                    }

                }
                return false;
            }

            /*
            /// <summary>
            /// �K�i������f�B�X�v���C�T�C�Y���擾���܂��B
            /// </summary>
            /// <param name="gauge">�K�i��</param>
            /// <returns>Display</returns>
            public Display getDisplay(string gauge)
            {
                foreach (Display display in this.displays)
                {
                    if (display.Gauge == gauge)
                    {
                        //SimpleLogger.WriteLine("Display: " + display.ToString());
                        return display;
                    }

                }
                //SimpleLogger.WriteLine("Display not found.");
                return new Display(0, 0, "undefined");
            }
            
            
            /// <summary>
            /// �f�B�X�v���C�T�C�Y����f�B�X�v���C���擾���܂��B
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public Display getDisplay(int width, int height)
            {
                foreach (Display display in this.displays)
                {
                    if (display.Width == width && display.Height == height)
                    {
                        //SimpleLogger.WriteLine("Display: " + display.ToString());
                        return display;
                    }

                }
                //SimpleLogger.WriteLine("Display gauge not found.");
                return new Display(0, 0, "undefined");
            }
            
            
            /// <summary>
            /// �f�B�X�v���C�T�C�Y����K�i�����擾���܂��B
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public string getGauge(int width, int height)
            {
                foreach (Display display in this.displays)
                {
                    if (display.Width == width && display.Height == height)
                    {
                        //SimpleLogger.WriteLine("Display: " + display.ToString());
                        return display.Gauge;
                    }

                }
                //SimpleLogger.WriteLine("Display gauge not found.");
                return "undefined";
            }
            */
        }
    }

    namespace Sound
    {
        public class Bits
        {
            /// <summary>
            /// �Đ��r�b�g
            /// </summary>
            public int Bit;


            public override string ToString()
            {
                return String.Format("{0:D}", this.Bit) + " Bit";
            }
        }




        public class Channel
        {
            /// <summary>
            /// �`�����l��
            /// </summary>
            public int ch;


            public override string ToString()
            {
                return this.ch + " Channel";
            }
        }




        public class SampleRate
        {
            /// <summary>
            /// ���g��
            /// </summary>
            public int Rate;


            public override string ToString()
            {
                return this.Rate.ToString();
            }
        }




        class BitCollector
        {
            private ArrayList bits = new ArrayList();

            public BitCollector()
            {
                this.add(8);
                this.add(16);
                this.add(24);
                this.add(32);
                this.add(48);
            }

            public void add(int bit)
            {
                Bits bits = new Bits();
                bits.Bit = bit;
                this.bits.Add(bits);
            }


            public ArrayList getMap()
            {
                return this.bits;
            }

        }



        class ChannelCollector
        {
            private ArrayList channels = new ArrayList();

            public ChannelCollector()
            {
                this.add(8);
                this.add(16);
                //this.add(24);
                this.add(32);
                this.add(48);
            }

            public void add(int channel)
            {
                Channel channels = new Channel();
                channels.ch = channel;
                this.channels.Add(channels);
            }


            public ArrayList getMap()
            {
                return this.channels;
            }

        }





        class SampleRateCollector
        {
            private ArrayList rates = new ArrayList();

            public SampleRateCollector()
            {
                this.add(22050);
                this.add(32000);
                this.add(44100);
                this.add(48000);
                this.add(96000);
            }

            public void add(int rate)
            {
                SampleRate samplerate = new SampleRate();
                samplerate.Rate = rate;
                this.rates.Add(samplerate);
            }

            public ArrayList getMap()
            {
                return this.rates;
            }
        }
    }






    namespace Images
    {

        public enum ImageFormat
        {
            JPG,
            BMP
        }


        public class Formats
        {
            /// <summary>
            /// �摜�t�H�[�}�b�g
            /// </summary>
            public ImageFormat Format;


            public override string ToString()
            {
                return Format.ToString();
            }
        }


        class FormatsCollector
        {
            private ArrayList formats = new ArrayList();

            public FormatsCollector()
            {
                this.add(ImageFormat.BMP);
                this.add(ImageFormat.JPG);
            }

            public void add(ImageFormat imgformat)
            {
                Formats format = new Formats();
                format.Format = imgformat;
                this.formats.Add(format);
            }

            public ArrayList getMap()
            {
                return this.formats;
            }

        }
    }
}
