using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Encoding
{
    public class IndexableEncoder
    {

        private static CodingCase[] CODING_CASES = new[]
        {
            // CodingCase(int initialShift, int finalShift)
            new CodingCase(7, 1),
            // CodingCase(int initialShift, int middleShift, int finalShift)
            new CodingCase(14, 6, 2),
            new CodingCase(13, 5, 3),
            new CodingCase(12, 4, 4),
            new CodingCase(11, 3, 5),
            new CodingCase(10, 2, 6),
            new CodingCase(9, 1, 7),
            new CodingCase(8, 0)
        };

        public static int GetEncodedLength(byte[] inputArray)
        {
            // Use long for intermediaries to protect against overflow
            return (int)((8L * inputArray.Length + 14L) / 15L) + 1;
        }

        static int GetDecodedLength(char[] encoded)
        {
            int numChars = encoded.Length - 1;
            if (numChars <= 0)
            {
                return 0;
            }
            else
            {
                // Use long for intermediaries to protect against overflow
                long numFullBytesInFinalChar = encoded[encoded.Length - 1];
                long numEncodedChars = numChars - 1;
                return (int)((numEncodedChars * 15L + 7L) / 8L + numFullBytesInFinalChar);
            }
        }

        public static string Encode(byte[] input)
        {
            if (input.Length > 0)
            {
                char[] outputArray = new char[GetEncodedLength(input)];
                int inputByteNum = 0;
                int caseNum = 0;
                int outputCharNum = 0;
                CodingCase codingCase;
                for (; inputByteNum + CODING_CASES[caseNum].numBytes <= input.Length; ++outputCharNum)
                {
                    codingCase = CODING_CASES[caseNum];
                    if (2 == codingCase.numBytes)
                    {
                        outputArray[outputCharNum] = (char)(((input[inputByteNum] & 0xFF) << codingCase.initialShift)
                            + (((uint)(input[inputByteNum + 1] & 0xFF) >> codingCase.finalShift) & codingCase.finalMask) & (short)0x7FFF);
                    }
                    else
                    { // numBytes is 3
                        outputArray[outputCharNum] = (char)(((input[inputByteNum] & 0xFF) << codingCase.initialShift)
                            + ((input[inputByteNum + 1] & 0xFF) << codingCase.middleShift)
                            + (((uint)(input[inputByteNum + 2] & 0xFF) >> codingCase.finalShift) & codingCase.finalMask) & (short)0x7FFF);
                    }
                    inputByteNum += codingCase.advanceBytes;
                    if (++caseNum == CODING_CASES.Length)
                    {
                        caseNum = 0;
                    }
                }
                // Produce final char (if any) and trailing count chars.
                codingCase = CODING_CASES[caseNum];

                if (inputByteNum + 1 < input.Length)
                { // codingCase.numBytes must be 3
                    outputArray[outputCharNum++] = (char)((((input[inputByteNum] & 0xFF) << codingCase.initialShift) + ((input[inputByteNum + 1] & 0xFF) << codingCase.middleShift)) & (short)0x7FFF);
                    // Add trailing char containing the number of full bytes in final char
                    outputArray[outputCharNum++] = (char)1;
                }
                else if (inputByteNum < input.Length)
                {
                    outputArray[outputCharNum++] = (char)(((input[inputByteNum] & 0xFF) << codingCase.initialShift) & (short)0x7FFF);
                    // Add trailing char containing the number of full bytes in final char
                    outputArray[outputCharNum++] = caseNum == 0 ? (char)1 : (char)0;
                }
                else
                { // No left over bits - last char is completely filled.
                  // Add trailing char containing the number of full bytes in final char
                    outputArray[outputCharNum++] = (char)1;
                }
                return new string(outputArray);
            }
            return string.Empty;
        }

        public static byte[] Decode(string input)
        {
            var inputArray = input.ToCharArray();
            var outputLength = GetDecodedLength(inputArray);
            var outputArray = new byte[outputLength];
            int numInputChars = inputArray.Length - 1;
            int numOutputBytes = outputLength;

            if (numOutputBytes > 0)
            {
                int caseNum = 0;
                int outputByteNum = 0;
                int inputCharNum = 0;
                short inputChar;
                CodingCase codingCase;
                for (; inputCharNum < numInputChars - 1; ++inputCharNum)
                {
                    codingCase = CODING_CASES[caseNum];
                    inputChar = (short)inputArray[inputCharNum];
                    if (2 == codingCase.numBytes)
                    {
                        if (0 == caseNum)
                        {
                            outputArray[outputByteNum] = (byte)((uint)inputChar >> codingCase.initialShift);
                        }
                        else
                        {
                            outputArray[outputByteNum] += (byte)((uint)inputChar >> codingCase.initialShift);
                        }
                        outputArray[outputByteNum + 1] = (byte)((inputChar & codingCase.finalMask) << codingCase.finalShift);
                    }
                    else
                    { // numBytes is 3
                        outputArray[outputByteNum] += (byte)((uint)inputChar >> codingCase.initialShift);
                        outputArray[outputByteNum + 1] = (byte)((uint)(inputChar & codingCase.middleMask) >> codingCase.middleShift);
                        outputArray[outputByteNum + 2] = (byte)((inputChar & codingCase.finalMask) << codingCase.finalShift);
                    }
                    outputByteNum += codingCase.advanceBytes;
                    if (++caseNum == CODING_CASES.Length)
                    {
                        caseNum = 0;
                    }
                }
                // Handle final char
                inputChar = (short)inputArray[inputCharNum];
                codingCase = CODING_CASES[caseNum];
                if (0 == caseNum)
                {
                    outputArray[outputByteNum] = 0;
                }
                outputArray[outputByteNum] += (byte)((uint)inputChar >> codingCase.initialShift);
                int bytesLeft = numOutputBytes - outputByteNum;
                if (bytesLeft > 1)
                {
                    if (2 == codingCase.numBytes)
                    {
                        outputArray[outputByteNum + 1] = (byte)((uint)(inputChar & codingCase.finalMask) >> codingCase.finalShift);
                    }
                    else
                    { // numBytes is 3
                        outputArray[outputByteNum + 1] = (byte)((uint)(inputChar & codingCase.middleMask) >> codingCase.middleShift);
                        if (bytesLeft > 2)
                        {
                            outputArray[outputByteNum + 2] = (byte)((inputChar & codingCase.finalMask) << codingCase.finalShift);
                        }
                    }
                }
            }
            return outputArray;
        }

        class CodingCase
        {
            public int numBytes, initialShift, middleShift, finalShift, advanceBytes = 2;
            public short middleMask, finalMask;

            public CodingCase(int initialShift, int middleShift, int finalShift)
            {
                this.numBytes = 3;
                this.initialShift = initialShift;
                this.middleShift = middleShift;
                this.finalShift = finalShift;
                this.finalMask = (short)((uint)0xFF >> finalShift);
                this.middleMask = (short)((short)0xFF << middleShift);
            }

            public CodingCase(int initialShift, int finalShift)
            {
                this.numBytes = 2;
                this.initialShift = initialShift;
                this.finalShift = finalShift;
                this.finalMask = (short)((uint)0xFF >> finalShift);
                if (finalShift != 0)
                {
                    advanceBytes = 1;
                }
            }
        }
    }
}
