using System;

namespace barcodeDecryptor
{
  /// <summary>
  /// This Class provide you a full access to your EAN-13 decryptions and barcode generation
  /// </summary>
  public class EAN13
  {
    
    private  int[] checkDigitMultiplier = {

      1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3
    };

    private  String[,] convertTable = {
      { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" },
      { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" },
      { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" }
        
    };

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

    private const String startEndSymbol = "101";
    private const String middleSymbol = "01010";
    private const int encryptedBarcodeLenght = 95;
    public int[] barcodeValueIndex;

    public EAN13()
    {
        barcodeValueIndex = new int[100];
        
    }

    /// <summary>
    /// Encrypt your barcode with the numeral barcode you provide in the parameters.
    /// </summary>
    /// <param name="phrase">Your non encoded barcode ie. 0097363373766</param>
    ///<returns>the encoded barcode (010101) on String format</returns>
    public String Encrypt(String phrase)
    {
      String returnValue = phrase;
      int valueToCrypt = -1;        

      if (phrase.Length != 13)
        throw new ApplicationException(string.Format("Invalid barcode lenght of {0} should have a lenght of 13", phrase.Length));

      int valCD = checkDigit(phrase);

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
    public int checkDigit(String phrase)
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

