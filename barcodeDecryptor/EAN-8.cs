//
//  EAN-8.cs
//
//  Author:
//       Francois Bissonnette <fbissonn@gmail.com>
//
//  Copyright (c) 2015 Francois Bissonnette
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;


namespace barcodeDecryptor 
{
  /// <summary>
  /// EAN8
  /// </summary>
     class EAN8 : genericBarcode
  {
        /// <summary>
        /// The check digit multiplier.
        /// </summary>
        private  int[] checkDigitMultiplier = {

            1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3
        };

        /// <summary>
        /// The convert table.
        /// </summary>
        private  String[,] convertTable = {
            { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" },
            { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" },
            { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" }

        };

        /// <summary>
        /// The index of the convert.
        /// </summary>
        private int[,] convertIndex = {

            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 1, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        };

        /// <summary>
        /// The start end symbol.
        /// </summary>
        private const String startEndSymbol = "101";

        /// <summary>
        /// The middle symbol.
        /// </summary>
        private const String middleSymbol = "01010";

        /// <summary>
        /// The encrypted barcode lenght.
        /// </summary>
        private const int encryptedBarcodeLenght = 95;

        /// <summary>
        /// The index of the barcode value.
        /// </summary>
         int[] barcodeValueIndex;

        protected string phrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="barcodeDecryptor.EAN8"/> class.
        /// </summary>
        public EAN8(String phrase)
        {
            barcodeValueIndex = new int[100];
            this.phrase = phrase;

        }
        /// <summary>
        /// Gets the index of the barcode value.
        /// </summary>
        /// <returns>The barcode value index.</returns>
       public override int [] getBarcodeValueIndex()
        {
            return barcodeValueIndex;

        }

        /// <summary>
        /// Encrypt your barcode with the numeral barcode you provide in the parameters.
        /// </summary>
        /// <param name="phrase">Your non encoded barcode ie. 0097363373766</param>
        ///<returns>the encoded barcode (010101) on String format</returns>
         public override String Encrypt()
        {
            String returnValue = phrase;
            int valueToCrypt = -1;        

            if (phrase.Length != 13)
                throw new ApplicationException(string.Format("Invalid barcode lenght of {0} should have a lenght of 13", phrase.Length));

            int valCD = checkDigit();

            if (valCD != Convert.ToInt32(phrase.Substring(phrase.Length - 1, 1)))
                throw new ApplicationException(string.Format("Check digit invalid found {0} in the barcode and {1} after calculation", valCD, Convert.ToInt32(phrase.Substring(phrase.Length - 1, 1))));

            returnValue = startEndSymbol;
            int posPtrPrev;
            int posPtr = 0;
            for(; posPtr < startEndSymbol.Length; posPtr++) barcodeValueIndex[posPtr] = 0;

            valueToCrypt = Convert.ToInt32(phrase.Substring(0, 1));

            for (int pos = 1; pos < phrase.Length; pos++)
            {
                if (pos < 6)
                {
                    returnValue += convertTable[convertIndex[valueToCrypt,pos],Convert.ToInt32(phrase.Substring(pos, 1))];

                    posPtrPrev = posPtr;
                    for(; posPtr < posPtrPrev+convertTable[convertIndex[valueToCrypt,pos],Convert.ToInt32(phrase.Substring(pos, 1))].Length; posPtr++) barcodeValueIndex[posPtr] = 1;

                } 
                else
                {
                    if (pos == 7)
                    {
                        returnValue += middleSymbol; 
                        posPtrPrev = posPtr;
                        for(; posPtr < posPtrPrev+middleSymbol.Length; posPtr++) barcodeValueIndex[posPtr] = 0;
                    }

                    returnValue += convertTable[2,Convert.ToInt32(phrase.Substring(pos, 1))];
                    posPtrPrev = posPtr;
                    for(; posPtr < posPtrPrev+convertTable[2,Convert.ToInt32(phrase.Substring(pos, 1))].Length; posPtr++) barcodeValueIndex[posPtr] = 1;
                }
            }  

            returnValue += startEndSymbol;

            return returnValue;
        }

        /// <summary>
        /// Calculate the checkdigit for the EAN-13 barcode
        /// </summary>
        /// <returns>The checkdigit.</returns>
        /// <param name="phrase">the barcode</param>
        public override int checkDigit()
        {
            int tot = 0, returnValue = -1;

            for (int pos = 0; pos < phrase.Length - 1; pos++)
            {
                tot += Convert.ToInt32(phrase.Substring(pos, 1)) * checkDigitMultiplier[pos];
            }

            returnValue = tot;
            tot = tot % 10;
            returnValue = 10 - tot;

            return returnValue;
        }
  }
}

