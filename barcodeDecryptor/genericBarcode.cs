//
//  genericBarcode.cs
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
  /// Generic barcode.
  /// </summary>
  abstract class genericBarcode
  {

   
    /// <summary>
    /// Initializes a new instance of the <see cref="barcodeDecryptor.genericBarcode"/> class.
    /// </summary>
    public genericBarcode()
    {
        
    }

    /// <summary>
    /// Encrypt the specified phrase.
    /// </summary>
    /// <param name="phrase">Phrase.</param>
    public String Encrypt(String phrase)
    {
      string returnValue = "";

      switch (phrase.Length)
        {

                case 13:
                    return (new EAN13());
                    break;
                case 8:
                    return (new EAN8());
                    break;
          
          returnValue = cEAN13.Encrypt(phrase);
          break;
          
        }
      
      return returnValue;
    }

    /// <summary>
    /// Gets the index of the barcode value.
    /// </summary>
    /// <returns>The barcode value index.</returns>
    public int [] getBarcodeValueIndex()
    {
      return cEAN13.getBarcodeValueIndex();
    }
    


  }
}

