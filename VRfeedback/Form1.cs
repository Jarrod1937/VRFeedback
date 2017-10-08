using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace VRfeedback
{
    public partial class Form1 : Form
    {
        private string processName = "";
        private bool processFound = false;
        private IntPtr processHandle = IntPtr.Zero;
        private Int64 memoryAddress = 0;
        private Char fileDelim = '|';
        private uint decreaseIncrement = 0;
        private bool serialStarted = false;

        // 0 = 32, 1 = 64
        private Int16 bitSize = 0;

        /*
         0 = Int16
         1 = Int32
         2 = Int64
         3 = double float
         4 = single float
         */ 
        Int16 valueType = 0;

        // Buffers based on value type
        private Int16 int16Buff = 0;
        private Int32 int32Buff = 0;
        private Int64 int64Buff = 0;
        private double doubleBuff = 0.00;
        private Single singleBuff = 0.00F;

        // Range holders
        private Int16[] int16Range = new Int16[2];
        private Int32[] int32Range = new Int32[2];
        private Int64[] int64Range = new Int64[2];
        private double[] doubleRange = new double[2];
        private Single[] singleRange = new Single[2];



        // Read permission constant
        private const int PROCESS_WM_READ = 0x0010;

        // import for looking for other application processes.
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, Int32 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool ReadProcessMemory64(IntPtr hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private Process process = null;

        private SerialPort _serialPort = new SerialPort();

        // Constructor and load functions
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populatePorts();

            if (Properties.Settings.Default.fileToLoad.Length > 0)
            {
                loadProcessFile(Properties.Settings.Default.fileToLoad);
            }

            if (Properties.Settings.Default.portCount >= 0)
            {
                Int32 portIndex = Properties.Settings.Default.portCount;
                cbPorts.SelectedIndex = Properties.Settings.Default.portCount;
            }

            if (Properties.Settings.Default.ComPort.Length > 0)
            {
                Int32 comCheck = comPortsDrop.Items.IndexOf(Properties.Settings.Default.ComPort);
                if (comCheck != -1)
                {
                    comPortsDrop.Text = Properties.Settings.Default.ComPort;
                }
                
            }
        }

        // Helper functions

        public static void Exit()
        {
            Properties.Settings.Default.Save();
        }

        private void serialSetup()
        {
            _serialPort.PortName = comPortsDrop.Text;
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;

            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
        }

        private void serialOpen()
        {
            try
            {
                _serialPort.Open();
            }
            catch
            {
                MessageBox.Show("Serial failure, check that the COM ports exist");
            }
           
        }

        private void serialOutput(Byte outputByte)
        {
            Byte[] tempByteArray = new Byte[] { outputByte };
            _serialPort.Write(tempByteArray, 0, 1);
        }

        private void serialClose()
        {
            _serialPort.Close();
        }

        private void serialSend(Int16 intensityVal)
        {
            // Randomly bounce between available ports... If 1, default to 1
            // The port numbers are referenced by 0 based index
            Int16 portVal = 0;

            if (cbPorts.SelectedIndex > 0)
            {
                Random rand = new Random();
                portVal = (Int16)rand.Next(0, cbPorts.SelectedIndex);
            }

            byte serialByte = serialByteCalc(portVal, intensityVal);
            serialOutput(serialByte);
        }

        private void populatePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comPortsDrop.Items.Add(port);
            }
        }

        byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }


        // Timer for finding process and attaching to it.
        // Timer is used so the program can be setup and then the game can be launched.
        // The program will then automatically search for and attach once found.
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                process = Process.GetProcessesByName(processName)[0];
                processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                lpFound.Text = "Yes";
                processFound = true;

            }
            catch
            {
                lpFound.Text = "No";
                processFound = false;
            }
            
        }

        // Timer for parsing memory once process is attached
        private void timer2_Tick(object sender, EventArgs e)
        {
            // Process bool is true and our handle is not a zero pointer (null)
            if(processFound == true && processHandle != IntPtr.Zero)
            {
                string[] outputValue = { "", "" };

                /*
                 0 = Int16
                 1 = Int32
                 2 = Int64
                 3 = double float
                 4 = single float
                 */

                // Parse memory region based on selected data type
                if (valueType == 0)
                {
                    outputValue = readCompareInt16();
                }
                else if(valueType == 1)
                {
                    outputValue = readCompareInt32();
                }
                else if(valueType == 2)
                {
                    outputValue = readCompareInt64();
                }
                else if(valueType == 3)
                {
                    outputValue = readCompareDouble();
                }
                else if (valueType == 4)
                {
                    outputValue = readCompareSingle();
                }

                lcValue.Text = outputValue[0];

                // If not empty string, our value has decreased
                if(outputValue[1] != "")
                {
                    decreaseIncrement++;

                    // Incrementing rolls over value automatically.
                    // Do it here just in case... 
                    // Though, highly unlikely someone's
                    // session would exceed 4,294,967,294.
                    if (decreaseIncrement >= 4294967294)
                    {
                        decreaseIncrement = 0;
                    }

                    lValueDecrease.Text = decreaseIncrement + " times";

                    // Take the value and convert to int16
                    Int16 intValDecrease = 0;
                    try
                    {
                        intValDecrease = Convert.ToInt16(outputValue[1]);
                    }
                    catch
                    {
                        // Don't care, value above is already initialized to 0
                    }

                    // Take the inverse of the decrease.
                    // E.G. The value decrease to 90%, we want to say the value decrease by 10%
                    intValDecrease = (Int16)(100 - intValDecrease);

                    // Next we simply want to scale the 10's range to 4 bit range (0-15)
                    intValDecrease = (Int16)(intValDecrease / 10);

                    if (intValDecrease < 0)
                    {
                        // We know some decrease occured, so if the value is < 0, set to 1
                        intValDecrease = 1;
                    }

                    serialSend(intValDecrease);

                }
            }
        }

        // Unless I'm missing something I couldn't find a more efficient method for this.
        // Takes the port to be used and intensity (decrease amount) as two 4 bit values
        // then merges them into a single byte. This makes the serial communication more
        // compact and easier to parse on a uController (do basic bit shifting on received byte).
        private Byte serialByteCalc(Int16 portVal, Int16 intensityVal)
        {
            // Get bytes of passed int16 values (2 bytes each)
            byte[] intBytesPort = BitConverter.GetBytes(portVal);
            byte[] intBytesIntensity = BitConverter.GetBytes(intensityVal);

            // Working under the assumption the value passed will never exceed a single byte range,
            // grab first byte from byte array.
            byte portNumByte = intBytesPort[0];
            byte intensityByte = intBytesIntensity[0];

            // Using the decrease percentage, encode port and decrease into single byte
            // first 4 bits from the left are port, the other 4 are the decrease (1-15)
            string portNumBinStr = Convert.ToString(portNumByte, 2);
            int[] portNumBits = portNumBinStr.PadLeft(8, '0').Select(c => int.Parse(c.ToString())).ToArray();

            // Initialize a bool array of all false.
            bool[] tempBoolPort = new bool[8];

            // Loop through and convert binary 'int' values to bool.
            Int32 loopc = 0;
            foreach (int i in portNumBits)
            {
                tempBoolPort[loopc] = Convert.ToBoolean(i);
                loopc++;
            }

            // Same procedure for intensity
            string intensityBinStr = Convert.ToString(intensityByte, 2);
            int[] intensityBits = intensityBinStr.PadLeft(8, '0').Select(c => int.Parse(c.ToString())).ToArray();

            bool[] tempBoolintensity = new bool[8];

            loopc = 0;
            foreach (int i in intensityBits)
            {
                tempBoolintensity[loopc] = Convert.ToBoolean(i);
                loopc++;
            }

            // Carry intensity values to port bool array.
            tempBoolPort[0] = tempBoolintensity[7];
            tempBoolPort[1] = tempBoolintensity[6];
            tempBoolPort[2] = tempBoolintensity[5];
            tempBoolPort[3] = tempBoolintensity[4];

            // Initialize bit array with bool array
            BitArray finalNum = new BitArray(tempBoolPort);
            
            // Convert bool array to byte
            byte finalByte = ConvertToByte(finalNum);

            // Display byte as binary string, good for debugging
            label3.Text = Convert.ToString(finalByte, 2).PadLeft(8, '0');

            return finalByte;
        }

        // Read and convert methods
        // They read the raw bytes, convert these to their data type
        // compare if value has decreased, and return string representation

        private string[] readCompareInt16()
        {
            string[] returnArray = { "", "" };
            int bytesRead = 0;
            byte[] buffer = new byte[2];
            if (bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            Int16 tempVal = BitConverter.ToInt16(buffer, 0);

            // Is read value inside range given?
            if(tempVal >= int16Range[0] && tempVal <= int16Range[1])
            { 
                returnArray[0] = tempVal.ToString();

                if (tempVal < int16Buff)
                {
                    returnArray[1] = (((int)Math.Floor(((tempVal / int16Buff) * 100.0) / 10.0)) * 10).ToString();
                }

                int16Buff = tempVal;
                loutOfRange.Text = "--";
            }
            else
            {
                loutOfRange.Text = "Yes";
            }

            return returnArray;
        }

        private string[] readCompareInt32()
        {
            string[] returnArray = { "", "" };
            int bytesRead = 0;
            byte[] buffer = new byte[4];
            if (bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            Int32 tempVal = BitConverter.ToInt32(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= int32Range[0] && tempVal <= int32Range[1])
            {
                returnArray[0] = tempVal.ToString();

                if (tempVal < int32Buff)
                {
                    double percentVal = (((double)tempVal / (double)int32Buff) * 100.0);
                    Int32 floorVal = (int)Math.Floor(percentVal / 10.0);
                    returnArray[1] = (floorVal * 10).ToString();
                }

                int32Buff = tempVal;
                loutOfRange.Text = "--";
            }
            else
            {
                loutOfRange.Text = "Yes";
            }

            return returnArray;
        }

        private string[] readCompareInt64()
        {
            string[] returnArray = { "", "" };
            int bytesRead = 0;
            byte[] buffer = new byte[8];
            if (bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            Int64 tempVal = BitConverter.ToInt64(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= int64Range[0] && tempVal <= int64Range[1])
            {
                returnArray[0] = tempVal.ToString();

                if (tempVal < int64Buff)
                {
                    returnArray[1] = (((int)Math.Floor(((tempVal / int64Buff) * 100.0) / 10.0)) * 10).ToString();
                }

                int64Buff = tempVal;
                loutOfRange.Text = "--";
            }
            else
            {
                loutOfRange.Text = "Yes";
            }

            return returnArray;
        }

        private string[] readCompareDouble()
        {
            string[] returnArray = { "", "" };
            int bytesRead = 0;
            byte[] buffer = new byte[8];
            if (bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            Double tempVal = BitConverter.ToDouble(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= doubleRange[0] && tempVal <= doubleRange[1])
            {
                returnArray[0] = tempVal.ToString();

                if (tempVal < doubleBuff)
                {
                    returnArray[1] = (((int)Math.Floor(((tempVal / doubleBuff) * 100.0) / 10.0)) * 10).ToString();
                }

                doubleBuff = tempVal;
                loutOfRange.Text = "--";
            }
            else
            {
                loutOfRange.Text = "Yes";
            }

            return returnArray;
        }

        private string[] readCompareSingle()
        {
            string[] returnArray = { "", "" };
            int bytesRead = 0;
            byte[] buffer = new byte[4];

            if(bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, memoryAddress, buffer, buffer.Length, ref bytesRead);
            }
            
            Single tempVal = BitConverter.ToSingle(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= singleRange[0] && tempVal <= singleRange[1])
            {
                returnArray[0] = tempVal.ToString();

                if (tempVal < singleBuff)
                {
                    returnArray[1] = (((int)Math.Floor(((tempVal / singleBuff) * 100.0) / 10.0)) * 10).ToString();
                }

                singleBuff = tempVal;
                loutOfRange.Text = "--";
            }
            else
            {
                loutOfRange.Text = "Yes";
            }

            return returnArray;
        }

        // Button actions

        // This is to start the serial communication and value initialization.
        // Most of the checking is very basic. Could easily do a regex for the string values.
        // This would make it more resilient.
        private void button1_Click(object sender, EventArgs e)
        {
            if(serialStarted == true)
            {
                serialClose();
            }

            if(cbSize.SelectedIndex == -1)
            {
                MessageBox.Show("Set the process bit size");
                return;
            }

            if(tbMemory.Text.Length > 0)
            {
                try
                {
                    String tempString = tbMemory.Text.ToUpper();

                    // Just incase it has a hex delim
                    tempString = tempString.Replace("0X", "");
                    memoryAddress = Convert.ToInt64(tempString, 16);
                }
                catch
                {
                    MessageBox.Show("Memory Address Invalid");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Memory Address Invalid");
                return;
            }

            String tempProcessName = tbProcessName.Text.Replace(".exe", "");

            if(tempProcessName.Length == 0)
            {
                MessageBox.Show("Invalid Process Name");
                return;
            }
            else
            {
                processName = tempProcessName;
            }

            if(cbType.SelectedIndex == -1)
            {
                MessageBox.Show("Select A Data Type");
                return;
            }

            valueType = (Int16)cbType.SelectedIndex;


            if (tbRangeMin.Text.Length == 0 || tbRangeMax.Text.Length == 0)
            {
                MessageBox.Show("Range is Invalid");
                return;
            }

            bool rangeHasError = false;
            switch(cbType.SelectedIndex)
            {
                case 0:
                    try
                    {
                        int16Range[0] = Convert.ToInt16(tbRangeMin.Text);
                        int16Range[1] = Convert.ToInt16(tbRangeMax.Text);
                    }
                    catch
                    {
                        rangeHasError = true;
                    }
                    break;
                case 1:
                    try
                    {
                        int32Range[0] = Convert.ToInt32(tbRangeMin.Text);
                        int32Range[1] = Convert.ToInt32(tbRangeMax.Text);
                    }
                    catch
                    {
                        rangeHasError = true;
                    }
                    break;
                case 2:
                    try
                    {
                        int64Range[0] = Convert.ToInt64(tbRangeMin.Text);
                        int64Range[1] = Convert.ToInt64(tbRangeMax.Text);
                    }
                    catch
                    {
                        rangeHasError = true;
                    }
                    break;
                case 3:
                    try
                    {
                        doubleRange[0] = Convert.ToDouble(tbRangeMin.Text);
                        doubleRange[1] = Convert.ToDouble(tbRangeMax.Text);
                    }
                    catch
                    {
                        rangeHasError = true;
                    }
                    break;
                case 4:
                    try
                    {
                        singleRange[0] = Convert.ToSingle(tbRangeMin.Text);
                        singleRange[1] = Convert.ToSingle(tbRangeMax.Text);
                    }
                    catch
                    {
                        rangeHasError = true;
                    }
                    break;
                default:
                    rangeHasError = true;
                    break;
            }

            if(rangeHasError == true)
            {
                MessageBox.Show("Range is Invalid");
                return;
            }

            if(comPortsDrop.SelectedIndex == -1)
            {
                MessageBox.Show("COM Port Must Be Selected");
                return;
            }

            if (cbPorts.SelectedIndex == -1)
            {
                MessageBox.Show("Number of Output Ports Must Be Selected");
                return;
            }

            Properties.Settings.Default.portCount = cbPorts.SelectedIndex;
            Properties.Settings.Default.Save();

            // If we made it this far, we're all good to start.
            // Just because the values are good doesn't mean they're correct though

            // Start serial
            serialSetup();
            serialOpen();

            // Start timer1 to look for and attach to process
            timer1.Enabled = true;

            // Start timer2 to look at process memory region for our data
            timer2.Enabled = true;

            button1.Text = "Restart Serial";
            serialStarted = true;
        }

        // Open dialog for loading a preset file.
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "/applications";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadProcessFile(openFileDialog1.FileName);
            }
        }

        // Load, parse, and initialize values from passed preset filename.
        private void loadProcessFile(String Filename)
        {
            Filename = System.IO.Path.GetFileName(Filename);
            String loadingDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\applications\\";
            if(File.Exists(loadingDir + Filename))
            {
                
                foreach (string line in File.ReadLines(loadingDir + Filename))
                {
                    //fileDelim
                    String[] fileData = line.Split(fileDelim);

                    if(fileData.Length == 6)
                    {
                        Properties.Settings.Default.fileToLoad = Filename;
                        Properties.Settings.Default.Save();
                        // File order:
                        // memory address, type, range min, range max, process name, bitsize
                        tbMemory.Text = fileData[0];

                        Int32 typeIndex = 0;

                        try
                        {
                            typeIndex = Convert.ToInt32(fileData[1]);
                        }
                        catch
                        {
                            // Don't care
                        }

                        cbType.SelectedIndex = typeIndex;
                        tbRangeMin.Text = fileData[2];
                        tbRangeMax.Text = fileData[3];

                        tbProcessName.Text = fileData[4].Trim();

                        Int32 bitSizeVal = 0;
                        try
                        {
                            bitSizeVal = Convert.ToInt32(fileData[5]);
                        }
                        catch
                        {
                            // Don't care
                        }

                        cbSize.SelectedIndex = bitSizeVal;

                        lFileLoaded.Text = Filename;
                    }
                    else
                    {
                        MessageBox.Show("Invalid File Loaded");
                        lFileLoaded.Text = "--";
                        Properties.Settings.Default.fileToLoad = "";
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        // Did our COM ports or port count change? Save value to preferences.
        private void cbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 portCount = cbPorts.SelectedIndex;
            Properties.Settings.Default.portCount = portCount;
            Properties.Settings.Default.Save();
        }

        private void comPortsDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ComPort = comPortsDrop.Text;
            Properties.Settings.Default.Save();
        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bitSize = (Int16)cbSize.SelectedIndex;
        }
    }
}
