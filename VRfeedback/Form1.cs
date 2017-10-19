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
        private Int64[] memoryAddress = new Int64[4];
        private Int64 baseAddress;
        private Char fileDelim = '|';
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
        private Int16[] valueType = new Int16[4];

        // 0 = offset, 1 =  memory address
        private Int32[] offsetMemAdd = new Int32[4];

        // Record the 'when' to send serial output settings per item
        private Int32[] whenVals = new Int32[4];

        // Buffers based on value type
        private Int16[] int16Buff = new Int16[4];
        private Int32[] int32Buff = new Int32[4];
        private Int64[] int64Buff = new Int64[4];
        private double[] doubleBuff = new double[4];
        private Single[] singleBuff = new Single[4];

        // Range holders
        private Int16[,] int16Range = new Int16[4,2];
        private Int32[,] int32Range = new Int32[4,2];
        private Int64[,] int64Range = new Int64[4,2];
        private double[,] doubleRange = new double[4,2];
        private Single[,] singleRange = new Single[4,2];

        // State Holders
        private Int16[,] int16States = new Int16[4,16];
        private Int32[,] int32States = new Int32[4,16];
        private Int64[,] int64States = new Int64[4,16];
        private double[,] doubleStates = new double[4,16];
        private Single[,] singleStates = new Single[4,16];

        // State count holders
        private Int16[] int16StateCount = new Int16[4] { 0, 0, 0, 0};
        private Int16[] int32StateCount = new Int16[4] { 0, 0, 0, 0 };
        private Int16[] int64StateCount = new Int16[4] { 0, 0, 0, 0 };
        private Int16[] doubleStateCount = new Int16[4] { 0, 0, 0, 0 };
        private Int16[] singleStateCount = new Int16[4] { 0, 0, 0, 0 };

        private void resetStateCount()
        {
            for (int i = 0; i < int16StateCount.Count(); i++)
            {
                int16StateCount[i] = 0;
            }

            for (int i = 0; i < int32StateCount.Count(); i++)
            {
                int32StateCount[i] = 0;
            }

            for (int i = 0; i < int64StateCount.Count(); i++)
            {
                int64StateCount[i] = 0;
            }

            for (int i = 0; i < doubleStateCount.Count(); i++)
            {
                doubleStateCount[i] = 0;
            }

            for (int i = 0; i < singleStateCount.Count(); i++)
            {
                singleStateCount[i] = 0;
            }
        }


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

        private void serialSend(Int16 intensityVal, Int16 portVal)
        {
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

                // Only need to do this code once per new detection
                if (processFound == false)
                {
                    processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                    baseAddress = (Int64)process.MainModule.BaseAddress;
                    //baseAddress = (Int64)process.MainModule.EntryPointAddress;

                    lpFound.Text = "Yes";
                }
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
                if(checkActive1.Checked)
                {
                    processValueAction(0);
                }

                if (checkActive2.Checked)
                {
                    processValueAction(1);
                }

                if (checkActive3.Checked)
                {
                    processValueAction(2);
                }

                if (checkActive4.Checked)
                {
                    processValueAction(3);
                }
            }
        }

        private void processValueAction(Int32 IndexVal)
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
            if (valueType[IndexVal] == 0)
            {
                outputValue = readCompareInt16(IndexVal);
            }
            else if (valueType[IndexVal] == 1)
            {
                outputValue = readCompareInt32(IndexVal);
            }
            else if (valueType[IndexVal] == 2)
            {
                outputValue = readCompareInt64(IndexVal);
            }
            else if (valueType[IndexVal] == 3)
            {
                outputValue = readCompareDouble(IndexVal);
            }
            else if (valueType[IndexVal] == 4)
            {
                outputValue = readCompareSingle(IndexVal);
            }

            switch(IndexVal)
            {
                case 0:
                    lcValue1.Text = outputValue[0];
                    break;
                case 1:
                    lcValue2.Text = outputValue[0];
                    break;
                case 2:
                    lcValue3.Text = outputValue[0];
                    break;
                case 3:
                    lcValue4.Text = outputValue[0];
                    break;
            }
            

            // If not empty string, our value has decreased
            if (outputValue[1] != "")
            {

                // Take the value and convert to int16
                Int16 intValChange = 0;
                try
                {
                    intValChange = Math.Abs(Convert.ToInt16(outputValue[1]));
                }
                catch
                {
                    // Don't care, value above is already initialized to 0
                }

                serialSend(intValChange, (Int16)IndexVal);

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

            // Reverse Port Bits
            bool[] TempBool = new bool[4];

            TempBool[0] = tempBoolPort[4];
            TempBool[1] = tempBoolPort[5];
            TempBool[2] = tempBoolPort[6];
            TempBool[3] = tempBoolPort[7];

            tempBoolPort[4] = TempBool[3];
            tempBoolPort[5] = TempBool[2];
            tempBoolPort[6] = TempBool[1];
            tempBoolPort[7] = TempBool[0];

            // Initialize bit array with bool array
            BitArray finalNum = new BitArray(tempBoolPort);
            
            // Convert bool array to byte
            byte finalByte = ConvertToByte(finalNum);
            string listingStr = portVal + ", " + intensityVal + ", " + Convert.ToString(finalByte, 2).PadLeft(8, '0');

            switch (portVal)
            {
                case 0:
                    lLastByte1.Text = listingStr;
                    break;
                case 1:
                    lLastByte2.Text = listingStr;
                    break;
                case 2:
                    lLastByte3.Text = listingStr;
                    break;
                case 3:
                    lLastByte4.Text = listingStr;
                    break;
            }

            return finalByte;
        }

        private Int32 returnWhenVal(Int32 IndexVal)
        {
            Int32 returnVal = 0;

            if (IndexVal == 0)
            {
                returnVal = cbWhen1.SelectedIndex;
            }
            else if (IndexVal == 1)
            {
                returnVal = cbWhen2.SelectedIndex;
            }
            else if (IndexVal == 2)
            {
                returnVal = cbWhen3.SelectedIndex;
            }
            else if (IndexVal == 3)
            {
                returnVal = cbWhen4.SelectedIndex;
            }

            if (returnVal < 0)
            {
                returnVal = 0;
            }

            return returnVal;

        }

        private Int16 returnStateMatch(Int32 IndexVal, Int16 Int16Val, Int32 Int32Val, Int64 Int64Val, double doubleVal, Single singleVal)
        {
            Int16 returnVal = -1;

            if (valueType[IndexVal] == 0)
            {
                Int16 loopStart = 0;
                Int16 loopEnd = int16StateCount[IndexVal];

                while(loopStart < loopEnd)
                {
                    if(int16States[IndexVal, loopStart] == Int16Val)
                    {
                        return loopStart;
                    }

                    loopStart++;
                }
            }
            else if (valueType[IndexVal] == 1)
            {
                Int16 loopStart = 0;
                Int16 loopEnd = int32StateCount[IndexVal];

                while (loopStart < loopEnd)
                {
                    if (int32States[IndexVal, loopStart] == Int32Val)
                    {
                        return loopStart;
                    }

                    loopStart++;
                }
            }
            else if (valueType[IndexVal] == 2)
            {
                Int16 loopStart = 0;
                Int16 loopEnd = int64StateCount[IndexVal];

                while (loopStart < loopEnd)
                {
                    if (int64States[IndexVal, loopStart] == Int64Val)
                    {
                        return loopStart;
                    }

                    loopStart++;
                }
            }
            else if (valueType[IndexVal] == 3)
            {
                Int16 loopStart = 0;
                Int16 loopEnd = doubleStateCount[IndexVal];

                while (loopStart < loopEnd)
                {
                    if (doubleStates[IndexVal, loopStart] == doubleVal)
                    {
                        return loopStart;
                    }

                    loopStart++;
                }
            }
            else if (valueType[IndexVal] == 4)
            {
                Int16 loopStart = 0;
                Int16 loopEnd = singleStateCount[IndexVal];

                while (loopStart < loopEnd)
                {
                    if (singleStates[IndexVal, loopStart] == singleVal)
                    {
                        return loopStart;
                    }

                    loopStart++;
                }
            }


            return returnVal;
        }

        private byte[] readMemoryBytes(Int32 IndexVal, Int32 ByteCount)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[ByteCount];

            Int64 tempAddress = (Int64)memoryAddress[IndexVal];

            if (offsetMemAdd[IndexVal] == 0)
            {
                tempAddress += (Int64)baseAddress;
            }

            if (bitSize == 0)
            {
                ReadProcessMemory(processHandle, (Int32)tempAddress, buffer, buffer.Length, ref bytesRead);
            }
            else
            {
                ReadProcessMemory64(processHandle, tempAddress, buffer, buffer.Length, ref bytesRead);
            }

            return buffer;
        }

        // Read and convert methods
        // They read the raw bytes, convert these to their data type
        // compare if value has decreased, and return string representation

        private string[] readCompareInt16(Int32 IndexVal)
        {
            Int32 whenVal = whenVals[IndexVal];
            string[] returnArray = { "", "" };

            byte[] buffer = readMemoryBytes(IndexVal, 2);

            Int16 tempVal = BitConverter.ToInt16(buffer, 0);

            // Is read value inside range given?
            if(tempVal >= int16Range[IndexVal,0] && tempVal <= int16Range[IndexVal, 1])
            { 
                returnArray[0] = tempVal.ToString();



                if (whenVal == 0)
                {
                    if (tempVal < int16Buff[IndexVal])
                    {
                       Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int16Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                }
                else if (whenVal == 1)
                {
                    if (tempVal > int16Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int16Buff[IndexVal] == 0)
                        {
                            int16Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int16Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 2)
                {
                    if (tempVal < int16Buff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int16Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                    else if (tempVal > int16Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int16Buff[IndexVal] == 0)
                        {
                            int16Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int16Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 3)
                {
                    // Max - Min = max range. EX. 100 - (-100) = 200, 100 - 10 = 70...etc
                    double segmentVal = (int16Range[IndexVal, 1] - int16Range[IndexVal, 0]) / 16.00;
                    Int32 segementedVal = (int)(Math.Round(tempVal / segmentVal));

                    if (segementedVal > 15)
                    {
                        segementedVal = 15;
                    }

                    returnArray[1] = (segementedVal).ToString();
                }
                else
                {
                    Int16 stateMatch = returnStateMatch(IndexVal, tempVal, 0, 0, 0, 0);

                    if(stateMatch != -1)
                    {
                        returnArray[1] = stateMatch.ToString();
                    }
                }

                int16Buff[IndexVal] = tempVal;
            }

            return returnArray;
        }

        private string[] readCompareInt32(Int32 IndexVal)
        {
            Int32 whenVal = whenVals[IndexVal];
            string[] returnArray = { "", "" };

            byte[] buffer = readMemoryBytes(IndexVal, 4);
            Int32 tempVal = BitConverter.ToInt32(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= int32Range[IndexVal,0] && tempVal <= int32Range[IndexVal,1])
            {
                returnArray[0] = tempVal.ToString();

                if (whenVal == 0)
                {
                    if (tempVal < int32Buff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int32Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                }
                else if (whenVal == 1)
                {
                    if (tempVal > int32Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int32Buff[IndexVal] == 0)
                        {
                            int32Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int32Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 2)
                {
                    if (tempVal < int32Buff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int32Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                    else if (tempVal > int32Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int32Buff[IndexVal] == 0)
                        {
                            int32Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int32Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 3)
                {
                    double segmentVal = (int32Range[IndexVal, 1] - int32Range[IndexVal, 0]) / 16.00;
                    Int32 segementedVal = (int)(Math.Round((tempVal - int32Range[IndexVal, 0]) / segmentVal));

                    if (segementedVal > 15)
                    {
                        segementedVal = 15;
                    }

                    returnArray[1] = (segementedVal).ToString();
                }
                else
                {
                    Int16 stateMatch = returnStateMatch(IndexVal, 0, tempVal, 0, 0, 0);

                    if (stateMatch != -1)
                    {
                        returnArray[1] = stateMatch.ToString();
                    }
                }

                int32Buff[IndexVal] = tempVal;
            }

            return returnArray;
        }

        private string[] readCompareInt64(Int32 IndexVal)
        {
            Int32 whenVal = whenVals[IndexVal];
            string[] returnArray = { "", "" };

            byte[] buffer = readMemoryBytes(IndexVal, 8);
            Int64 tempVal = BitConverter.ToInt64(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= int64Range[IndexVal,0] && tempVal <= int64Range[IndexVal,1])
            {
                returnArray[0] = tempVal.ToString();

                if (whenVal == 0)
                {
                    if (tempVal < int64Buff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int64Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                }
                else if (whenVal == 1)
                {
                    if (tempVal > int64Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int64Buff[IndexVal] == 0)
                        {
                            int64Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int64Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 2)
                {
                    if (tempVal < int64Buff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / int64Buff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                    else if (tempVal > int64Buff[IndexVal])
                    {

                        // No divide by zero
                        if (int64Buff[IndexVal] == 0)
                        {
                            int64Buff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / int64Buff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 3)
                {
                    double segmentVal = (int64Range[IndexVal, 1] - int64Range[IndexVal, 0]) / 16.00;
                    Int32 segementedVal = (int)(Math.Round((tempVal - int64Range[IndexVal, 0]) / segmentVal));

                    if (segementedVal > 15)
                    {
                        segementedVal = 15;
                    }

                    returnArray[1] = (segementedVal).ToString();
                }
                else
                {
                    Int16 stateMatch = returnStateMatch(IndexVal, 0, 0, tempVal, 0, 0);

                    if (stateMatch != -1)
                    {
                        returnArray[1] = stateMatch.ToString();
                    }
                }


                int64Buff[IndexVal] = tempVal;
            }

            return returnArray;
        }

        private string[] readCompareDouble(Int32 IndexVal)
        {
            Int32 whenVal = whenVals[IndexVal];
            string[] returnArray = { "", "" };

            byte[] buffer = readMemoryBytes(IndexVal, 8);
            Double tempVal = BitConverter.ToDouble(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= doubleRange[IndexVal,0] && tempVal <= doubleRange[IndexVal,1])
            {
                returnArray[0] = tempVal.ToString();

                if (whenVal == 0)
                {
                    if (tempVal < doubleBuff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / doubleBuff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                }
                else if (whenVal == 1)
                {
                    if (tempVal > doubleBuff[IndexVal])
                    {

                        // No divide by zero
                        if (doubleBuff[IndexVal] == 0)
                        {
                            doubleBuff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / doubleBuff[IndexVal]) *100.0) - 100) / 10).ToString();
                    }
                }
                else if(whenVal == 2)
                {
                    if (tempVal < doubleBuff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / doubleBuff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                    else if (tempVal > doubleBuff[IndexVal])
                    {

                        // No divide by zero
                        if (doubleBuff[IndexVal] == 0)
                        {
                            doubleBuff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / doubleBuff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if (whenVal == 3)
                {
                    double segmentVal = (doubleRange[IndexVal, 1] - doubleRange[IndexVal, 0]) / 16.00;
                    Int32 segementedVal = (int)(Math.Round((tempVal - doubleRange[IndexVal, 0]) / segmentVal));

                    if (segementedVal > 15)
                    {
                        segementedVal = 15;
                    }

                    returnArray[1] = (segementedVal).ToString();
                }
                else
                {
                    Int16 stateMatch = returnStateMatch(IndexVal, 0, 0, 0, tempVal, 0);

                    if (stateMatch != -1)
                    {
                        returnArray[1] = stateMatch.ToString();
                    }
                }

                doubleBuff[IndexVal] = tempVal;
            }


            return returnArray;
        }

        private string[] readCompareSingle(Int32 IndexVal)
        {
            Int32 whenVal = whenVals[IndexVal];
            string[] returnArray = { "", "" };

            byte[] buffer = readMemoryBytes(IndexVal, 4);
            Single tempVal = BitConverter.ToSingle(buffer, 0);

            // Is read value inside range given?
            if (tempVal >= singleRange[IndexVal,0] && tempVal <= singleRange[IndexVal,1])
            {
                returnArray[0] = tempVal.ToString();

                if (whenVal == 0)
                {
                    if (tempVal < singleBuff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / singleBuff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                }
                else if (whenVal == 1)
                {
                    if (tempVal > singleBuff[IndexVal])
                    {

                        // No divide by zero
                        if (singleBuff[IndexVal] == 0)
                        {
                            singleBuff[IndexVal] = 1;
                        }

                        string tempvar = ((int)Math.Floor(((tempVal / singleBuff[IndexVal]) * 100.0) - 100) / 10).ToString();
                        returnArray[1] = tempvar;
                    }
                }
                else if(whenVal == 2)
                {
                    if (tempVal < singleBuff[IndexVal])
                    {
                        Int16 intValDecrease = (Int16)(((int)Math.Floor(((tempVal / singleBuff[IndexVal]) * 100.0) / 10.0)) * 10);

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

                        returnArray[1] = intValDecrease.ToString();
                    }
                    else if (tempVal > singleBuff[IndexVal])
                    {

                        // No divide by zero
                        if (singleBuff[IndexVal] == 0)
                        {
                            singleBuff[IndexVal] = 1;
                        }

                        returnArray[1] = ((int)Math.Floor(((tempVal / singleBuff[IndexVal]) * 100.0) - 100) / 10).ToString();
                    }
                }
                else if (whenVal == 3)
                {
                    double segmentVal = (singleRange[IndexVal, 1] - singleRange[IndexVal, 0]) / 16.00;
                                        Int32 segementedVal = (int)(Math.Round((tempVal - singleRange[IndexVal, 0]) / segmentVal));

                    if (segementedVal > 15)
                    {
                        segementedVal = 15;
                    }

                    returnArray[1] = (segementedVal).ToString();
                }
                else
                {
                    Int16 stateMatch = returnStateMatch(IndexVal, 0, 0, 0, 0, tempVal);

                    if (stateMatch != -1)
                    {
                        returnArray[1] = stateMatch.ToString();
                    }
                }

                singleBuff[IndexVal] = tempVal;
            }

            return returnArray;
        }

        private Int16 setStateArray(Int32 IndexVal)
        {
            Int16 returnVal = 0;
            Int32 typeVal = -1;
            string stateVal = "";

            switch (IndexVal)
            {
                case 0:
                    typeVal = cbType1.SelectedIndex;
                    stateVal = tbStateList1.Text;
                    break;

                case 1:
                    typeVal = cbType2.SelectedIndex;
                    stateVal = tbStateList2.Text;
                    break;

                case 2:
                    typeVal = cbType3.SelectedIndex;
                    stateVal = tbStateList3.Text;
                    break;

                case 3:
                    typeVal = cbType4.SelectedIndex;
                    stateVal = tbStateList4.Text;
                    break;
            }

            // Have state information and a proper type
            if(stateVal.Length > 0 && typeVal != -1)
            {
                returnVal = parseArray(IndexVal, typeVal, stateVal);
            }

            return returnVal;
        }

        private Int16 parseArray(Int32 IndexVal, Int32 typeVal, string stateVals)
        {

            Int16 returnVal = 0;
            Int32 maxStates = 15;
            String[] stateData = stateVals.Split(',');


            if (typeVal == 0)
            {
                Int32 stateLoop = 0;
                foreach (string stateValue in stateData)
                {
                    // Don't parse more states than allowed
                    if(stateLoop > maxStates)
                    {
                        break;
                    }

                    try
                    {
                        int16States[IndexVal, stateLoop] = Convert.ToInt16(stateValue);
                    }
                    catch
                    {
                        returnVal = -1;
                        break;
                    }

                    stateLoop++;
                }

                if(returnVal != -1)
                {
                    int16StateCount[IndexVal] = (Int16)stateLoop;
                }
                

            }
            else if (typeVal == 1)
            {
                Int32 stateLoop = 0;
                foreach (string stateValue in stateData)
                {
                    // Don't parse more states than allowed
                    if (stateLoop > maxStates)
                    {
                        break;
                    }

                    try
                    {
                        int32States[IndexVal, stateLoop] = Convert.ToInt32(stateValue);
                    }
                    catch
                    {
                        returnVal = -1;
                        break;
                    }

                    stateLoop++;
                }

                if (returnVal != -1)
                {
                    int32StateCount[IndexVal] = (Int16)stateLoop;
                }

            }
            else if (typeVal == 2)
            {
                Int32 stateLoop = 0;
                foreach (string stateValue in stateData)
                {
                    // Don't parse more states than allowed
                    if (stateLoop > maxStates)
                    {
                        break;
                    }

                    try
                    {
                        int64States[IndexVal, stateLoop] = Convert.ToInt64(stateValue);
                    }
                    catch
                    {
                        returnVal = -1;
                        break;
                    }

                    stateLoop++;
                }

                if (returnVal != -1)
                {
                    int64StateCount[IndexVal] = (Int16)stateLoop;
                }
            }
            else if (typeVal == 3)
            {
                Int32 stateLoop = 0;
                foreach (string stateValue in stateData)
                {
                    // Don't parse more states than allowed
                    if (stateLoop > maxStates)
                    {
                        break;
                    }

                    try
                    {
                        doubleStates[IndexVal, stateLoop] = Convert.ToDouble(stateValue);
                    }
                    catch
                    {
                        returnVal = -1;
                        break;
                    }

                    stateLoop++;
                }

                if (returnVal != -1)
                {
                    doubleStateCount[IndexVal] = (Int16)stateLoop;
                }
            }
            else if (typeVal == 4)
            {
                Int32 stateLoop = 0;
                foreach (string stateValue in stateData)
                {
                    // Don't parse more states than allowed
                    if (stateLoop > maxStates)
                    {
                        break;
                    }

                    try
                    {
                        singleStates[IndexVal, stateLoop] = Convert.ToSingle(stateValue);
                    }
                    catch
                    {
                        returnVal = -1;
                        break;
                    }

                    stateLoop++;
                }

                if (returnVal != -1)
                {
                    singleStateCount[IndexVal] = (Int16)stateLoop;
                }
            }

            return returnVal;

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

            String tempProcessName = tbProcessName.Text.Replace(".exe", "");

            if (tempProcessName.Length == 0)
            {
                MessageBox.Show("Invalid Process Name");
                return;
            }
            else
            {
                processName = tempProcessName;
            }

            if (comPortsDrop.SelectedIndex == -1)
            {
                MessageBox.Show("COM Port Must Be Selected");
                return;
            }

            // Lazily copy/paste code verification section for each value
            // Later can cleanup code to be more compact.

            bool rangeHasError = false;

            //------------//
            // Section 1 //
            //-----------//
            if (checkActive1.Checked)
            {

                if(cbWhen1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the 'When' for 1");
                    return;
                }

                whenVals[0] = returnWhenVal(0);

                // If when equals 'on state'
                if(whenVals[0] == 4)
                {
                    Int16 stateStatus = setStateArray(0);

                    if(stateStatus != 0)
                    {
                        MessageBox.Show("You have state selected for 1, but the state list is not valid");
                        resetStateCount();
                        return;
                    }
                }

                if (tbMemory1.Text.Length > 0)
                {
                    try
                    {
                        String tempString = tbMemory1.Text.ToUpper();

                        // Just incase it has a hex delim
                        tempString = tempString.Replace("0X", "");
                        memoryAddress[0] = Convert.ToInt64(tempString, 16);
                    }
                    catch
                    {
                        MessageBox.Show("Memory Address Invalid, 1");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Memory Address Invalid, 1");
                    return;
                }

                if (cbType1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select A Data Type, 1");
                    return;
                }

                valueType[0] = (Int16)cbType1.SelectedIndex;

                if (cbOffsetAdd1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select if value is offset or memory address, 1");
                    return;
                }

                offsetMemAdd[0] = cbOffsetAdd1.SelectedIndex;

                if (tbRangeMin1.Text.Length == 0 || tbRangeMax1.Text.Length == 0)
                {
                    MessageBox.Show("Range 1 is Invalid");
                    return;
                }

                switch (cbType1.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            int16Range[0, 0] = Convert.ToInt16(tbRangeMin1.Text);
                            int16Range[0, 1] = Convert.ToInt16(tbRangeMax1.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 1:
                        try
                        {
                            int32Range[0, 0] = Convert.ToInt32(tbRangeMin1.Text);
                            int32Range[0, 1] = Convert.ToInt32(tbRangeMax1.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 2:
                        try
                        {
                            int64Range[0, 0] = Convert.ToInt64(tbRangeMin1.Text);
                            int64Range[0, 1] = Convert.ToInt64(tbRangeMax1.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 3:
                        try
                        {
                            doubleRange[0, 0] = Convert.ToDouble(tbRangeMin1.Text);
                            doubleRange[0, 1] = Convert.ToDouble(tbRangeMax1.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 4:
                        try
                        {
                            singleRange[0, 0] = Convert.ToSingle(tbRangeMin1.Text);
                            singleRange[0, 1] = Convert.ToSingle(tbRangeMax1.Text);
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

                if (rangeHasError == true)
                {
                    MessageBox.Show("Range 1 is Invalid");
                    return;
                }

            }

            //------------//
            // Section 2 //
            //-----------//
            if (checkActive2.Checked)
            {
                if (cbWhen2.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the 'When' for 2");
                    return;
                }

                whenVals[1] = returnWhenVal(1);

                // If when equals 'on state'
                if (whenVals[1] == 4)
                {
                    Int16 stateStatus = setStateArray(1);

                    if (stateStatus != 0)
                    {
                        MessageBox.Show("You have state selected for 2, but the state list is not valid");
                        resetStateCount();
                        return;
                    }
                }

                if (tbMemory2.Text.Length > 0)
                {
                    try
                    {
                        String tempString = tbMemory2.Text.ToUpper();

                        // Just incase it has a hex delim
                        tempString = tempString.Replace("0X", "");
                        memoryAddress[1] = Convert.ToInt64(tempString, 16);
                    }
                    catch
                    {
                        MessageBox.Show("Memory Address Invalid, 2");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Memory Address Invalid, 2");
                    return;
                }

                if (cbType2.SelectedIndex == -1)
                {
                    MessageBox.Show("Select A Data Type, 2");
                    return;
                }

                valueType[1] = (Int16)cbType2.SelectedIndex;

                if (cbOffsetAdd2.SelectedIndex == -1)
                {
                    MessageBox.Show("Select if value is offset or memory address, 2");
                    return;
                }

                offsetMemAdd[1] = cbOffsetAdd2.SelectedIndex;

                if (tbRangeMin2.Text.Length == 0 || tbRangeMax2.Text.Length == 0)
                {
                    MessageBox.Show("Range 2 is Invalid");
                    return;
                }

                rangeHasError = false;
                switch (cbType2.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            int16Range[1, 0] = Convert.ToInt16(tbRangeMin2.Text);
                            int16Range[1, 1] = Convert.ToInt16(tbRangeMax2.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 1:
                        try
                        {
                            int32Range[1, 0] = Convert.ToInt32(tbRangeMin2.Text);
                            int32Range[1, 1] = Convert.ToInt32(tbRangeMax2.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 2:
                        try
                        {
                            int64Range[1, 0] = Convert.ToInt64(tbRangeMin2.Text);
                            int64Range[1, 1] = Convert.ToInt64(tbRangeMax2.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 3:
                        try
                        {
                            doubleRange[1, 0] = Convert.ToDouble(tbRangeMin2.Text);
                            doubleRange[1, 1] = Convert.ToDouble(tbRangeMax2.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 4:
                        try
                        {
                            singleRange[1, 0] = Convert.ToSingle(tbRangeMin2.Text);
                            singleRange[1, 1] = Convert.ToSingle(tbRangeMax2.Text);
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

                if (rangeHasError == true)
                {
                    MessageBox.Show("Range 2 is Invalid");
                    return;
                }

            }

            //------------//
            // Section 3 //
            //-----------//

            if (checkActive3.Checked)
            {
                if (cbWhen3.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the 'When' for 3");
                    return;
                }

                whenVals[2] = returnWhenVal(2);

                // If when equals 'on state'
                if (whenVals[2] == 4)
                {
                    Int16 stateStatus = setStateArray(2);

                    if (stateStatus != 0)
                    {
                        MessageBox.Show("You have state selected for 3, but the state list is not valid");
                        resetStateCount();
                        return;
                    }
                }


                if (tbMemory3.Text.Length > 0)
                {
                    try
                    {
                        String tempString = tbMemory3.Text.ToUpper();

                        // Just incase it has a hex delim
                        tempString = tempString.Replace("0X", "");
                        memoryAddress[2] = Convert.ToInt64(tempString, 16);
                    }
                    catch
                    {
                        MessageBox.Show("Memory Address Invalid, 3");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Memory Address Invalid, 3");
                    return;
                }

                if (cbType3.SelectedIndex == -1)
                {
                    MessageBox.Show("Select A Data Type, 3");
                    return;
                }

                valueType[2] = (Int16)cbType3.SelectedIndex;

                if (cbOffsetAdd3.SelectedIndex == -1)
                {
                    MessageBox.Show("Select if value is offset or memory address, 3");
                    return;
                }

                offsetMemAdd[2] = cbOffsetAdd3.SelectedIndex;

                if (tbRangeMin3.Text.Length == 0 || tbRangeMax3.Text.Length == 0)
                {
                    MessageBox.Show("Range 3 is Invalid");
                    return;
                }

                rangeHasError = false;
                switch (cbType3.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            int16Range[2, 0] = Convert.ToInt16(tbRangeMin3.Text);
                            int16Range[2, 1] = Convert.ToInt16(tbRangeMax3.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 1:
                        try
                        {
                            int32Range[2, 0] = Convert.ToInt32(tbRangeMin3.Text);
                            int32Range[2, 1] = Convert.ToInt32(tbRangeMax3.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 2:
                        try
                        {
                            int64Range[2, 0] = Convert.ToInt64(tbRangeMin3.Text);
                            int64Range[2, 1] = Convert.ToInt64(tbRangeMax3.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 3:
                        try
                        {
                            doubleRange[2, 0] = Convert.ToDouble(tbRangeMin3.Text);
                            doubleRange[2, 1] = Convert.ToDouble(tbRangeMax3.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 4:
                        try
                        {
                            singleRange[2, 0] = Convert.ToSingle(tbRangeMin3.Text);
                            singleRange[2, 1] = Convert.ToSingle(tbRangeMax3.Text);
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

                if (rangeHasError == true)
                {
                    MessageBox.Show("Range 3 is Invalid");
                    return;
                }
            }

            //------------//
            // Section 4 //
            //-----------//

            if (checkActive4.Checked)
            {

                if (cbWhen4.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the 'When' for 4");
                    return;
                }

                whenVals[3] = returnWhenVal(3);

                // If when equals 'on state'
                if (whenVals[3] == 4)
                {
                    Int16 stateStatus = setStateArray(3);

                    if (stateStatus != 0)
                    {
                        MessageBox.Show("You have state selected for 4, but the state list is not valid");
                        resetStateCount();
                        return;
                    }
                }

                if (tbMemory4.Text.Length > 0)
                {
                    try
                    {
                        String tempString = tbMemory4.Text.ToUpper();

                        // Just incase it has a hex delim
                        tempString = tempString.Replace("0X", "");
                        memoryAddress[3] = Convert.ToInt64(tempString, 16);
                    }
                    catch
                    {
                        MessageBox.Show("Memory Address Invalid, 4");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Memory Address Invalid, 4");
                    return;
                }

                if (cbType4.SelectedIndex == -1)
                {
                    MessageBox.Show("Select A Data Type, 4");
                    return;
                }

                valueType[3] = (Int16)cbType4.SelectedIndex;

                if (cbOffsetAdd4.SelectedIndex == -1)
                {
                    MessageBox.Show("Select if value is offset or memory address, 4");
                    return;
                }

                offsetMemAdd[3] = cbOffsetAdd4.SelectedIndex;

                if (tbRangeMin4.Text.Length == 0 || tbRangeMax4.Text.Length == 0)
                {
                    MessageBox.Show("Range 4 is Invalid");
                    return;
                }

                rangeHasError = false;
                switch (cbType4.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            int16Range[3, 0] = Convert.ToInt16(tbRangeMin4.Text);
                            int16Range[3, 1] = Convert.ToInt16(tbRangeMax4.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 1:
                        try
                        {
                            int32Range[3, 0] = Convert.ToInt32(tbRangeMin4.Text);
                            int32Range[3, 1] = Convert.ToInt32(tbRangeMax4.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 2:
                        try
                        {
                            int64Range[3, 0] = Convert.ToInt64(tbRangeMin4.Text);
                            int64Range[3, 1] = Convert.ToInt64(tbRangeMax4.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 3:
                        try
                        {
                            doubleRange[3, 0] = Convert.ToDouble(tbRangeMin4.Text);
                            doubleRange[3, 1] = Convert.ToDouble(tbRangeMax4.Text);
                        }
                        catch
                        {
                            rangeHasError = true;
                        }
                        break;
                    case 4:
                        try
                        {
                            singleRange[3, 0] = Convert.ToSingle(tbRangeMin4.Text);
                            singleRange[3, 1] = Convert.ToSingle(tbRangeMax4.Text);
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

                if (rangeHasError == true)
                {
                    MessageBox.Show("Range 4 is Invalid");
                    return;
                }
            }

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
                Int32 lineNumber = 0;
                foreach (string line in File.ReadLines(loadingDir + Filename))
                {
                    //fileDelim
                    String[] fileData = line.Split(fileDelim);

                    if ((fileData.Length == 7 && lineNumber > 0) || (lineNumber == 0 && fileData.Length == 2))
                    {
                        Properties.Settings.Default.fileToLoad = Filename;
                        Properties.Settings.Default.Save();
                        // File order:
                        // process name, bitsize
                        // memory address/offset, type, range min, range max, offset or address bool, when to send, state string (if used, else an empty string works)

                        if (lineNumber == 0)
                        {

                            tbProcessName.Text = fileData[0].Trim();

                            try
                            {
                                Int32 bitSizeVal = 0;
                                bitSizeVal = Convert.ToInt32(fileData[1]);
                                cbSize.SelectedIndex = bitSizeVal;
                            }
                            catch
                            {
                                cbSize.SelectedIndex = 0;
                            }

                        }
                        else if(lineNumber == 1)
                        {
                            tbMemory1.Text = fileData[0];

                            try
                            {
                                Int32 typeIndex = 0;
                                typeIndex = Convert.ToInt32(fileData[1]);
                                cbType1.SelectedIndex = typeIndex;
                            }
                            catch
                            {
                                cbType1.SelectedIndex = 0;
                            }

                            tbRangeMin1.Text = fileData[2];
                            tbRangeMax1.Text = fileData[3];

                            try
                            {
                                Int32 offsetAddressVal = 0;
                                offsetAddressVal = Convert.ToInt32(fileData[4]);
                                cbOffsetAdd1.SelectedIndex = offsetAddressVal;
                            }
                            catch
                            {
                                cbOffsetAdd1.SelectedIndex = 0;
                            }

                            try
                            {
                                Int32 WhenVal = 0;
                                WhenVal = Convert.ToInt32(fileData[5]);
                                cbWhen1.SelectedIndex = WhenVal;
                            }
                            catch
                            {
                                cbWhen1.SelectedIndex = 0;
                            }

                            tbStateList1.Text = fileData[6];

                            checkActive1.Checked = true;

                        }
                        else if(lineNumber == 2)
                        {
                            tbMemory2.Text = fileData[0];

                            try
                            {
                                Int32 typeIndex = 0;
                                typeIndex = Convert.ToInt32(fileData[1]);
                                cbType2.SelectedIndex = typeIndex;
                            }
                            catch
                            {
                                cbType2.SelectedIndex = 0;
                            }

                            tbRangeMin2.Text = fileData[2];
                            tbRangeMax2.Text = fileData[3];

                            try
                            {
                                Int32 offsetAddressVal = 0;
                                offsetAddressVal = Convert.ToInt32(fileData[4]);
                                cbOffsetAdd2.SelectedIndex = offsetAddressVal;
                            }
                            catch
                            {
                                cbOffsetAdd2.SelectedIndex = 0;
                            }

                            try
                            {
                                Int32 WhenVal = 0;
                                WhenVal = Convert.ToInt32(fileData[5]);
                                cbWhen2.SelectedIndex = WhenVal;
                            }
                            catch
                            {
                                cbWhen1.SelectedIndex = 0;
                            }

                            tbStateList2.Text = fileData[6];

                            checkActive2.Checked = true;
                        }
                        else if(lineNumber == 3)
                        {
                            tbMemory3.Text = fileData[0];

                            try
                            {
                                Int32 typeIndex = 0;
                                typeIndex = Convert.ToInt32(fileData[1]);
                                cbType3.SelectedIndex = typeIndex;
                            }
                            catch
                            {
                                cbType3.SelectedIndex = 0;
                            }

                            tbRangeMin3.Text = fileData[2];
                            tbRangeMax3.Text = fileData[3];

                            try
                            {
                                Int32 offsetAddressVal = 0;
                                offsetAddressVal = Convert.ToInt32(fileData[4]);
                                cbOffsetAdd3.SelectedIndex = offsetAddressVal;
                            }
                            catch
                            {
                                cbOffsetAdd3.SelectedIndex = 0;
                            }

                            try
                            {
                                Int32 WhenVal = 0;
                                WhenVal = Convert.ToInt32(fileData[5]);
                                cbWhen3.SelectedIndex = WhenVal;
                            }
                            catch
                            {
                                cbWhen1.SelectedIndex = 0;
                            }

                            tbStateList3.Text = fileData[6];

                            checkActive3.Checked = true;
                        }
                        else if(lineNumber == 4)
                        {
                            tbMemory4.Text = fileData[0];

                            try
                            {
                                Int32 typeIndex = 0;
                                typeIndex = Convert.ToInt32(fileData[1]);
                                cbType4.SelectedIndex = typeIndex;
                            }
                            catch
                            {
                                cbType4.SelectedIndex = 0;
                            }

                            tbRangeMin4.Text = fileData[2];
                            tbRangeMax4.Text = fileData[3];

                            try
                            {
                                Int32 offsetAddressVal = 0;
                                offsetAddressVal = Convert.ToInt32(fileData[4]);
                                cbOffsetAdd4.SelectedIndex = offsetAddressVal;
                            }
                            catch
                            {
                                cbOffsetAdd4.SelectedIndex = 0;
                            }

                            try
                            {
                                Int32 WhenVal = 0;
                                WhenVal = Convert.ToInt32(fileData[5]);
                                cbWhen4.SelectedIndex = WhenVal;
                            }
                            catch
                            {
                                cbWhen1.SelectedIndex = 0;
                            }

                            tbStateList4.Text = fileData[6];

                            checkActive4.Checked = true;
                        }

                        lFileLoaded.Text = Filename;
                    }
                    else
                    {
                        MessageBox.Show("Invalid File Loaded");
                        lFileLoaded.Text = "--";
                        Properties.Settings.Default.fileToLoad = "";
                        Properties.Settings.Default.Save();
                        break;
                    }

                    lineNumber++;
                }
            }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
