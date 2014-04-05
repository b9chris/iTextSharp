using System;
using iTextSharp.text;
/*
 * $Id: GreekAlphabetFactory.cs,v 1.2 2008/05/13 11:25:14 psoares33 Exp $
 * 
 *
 * Copyright 2007 by Bruno Lowagie.
 *
 * The contents of this file are subject to the Mozilla Public License Version 1.1
 * (the "License"); you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the License.
 *
 * The Original Code is 'iText, a free JAVA-PDF library'.
 *
 * The Initial Developer of the Original Code is Bruno Lowagie. Portions created by
 * the Initial Developer are Copyright (C) 1999, 2000, 2001, 2002 by Bruno Lowagie.
 * All Rights Reserved.
 * Co-Developer of the code is Paulo Soares. Portions created by the Co-Developer
 * are Copyright (C) 2000, 2001, 2002 by Paulo Soares. All Rights Reserved.
 *
 * Contributor(s): all the names of the contributors are added in the source code
 * where applicable.
 *
 * Alternatively, the contents of this file may be used under the terms of the
 * LGPL license (the "GNU LIBRARY GENERAL PUBLIC LICENSE"), in which case the
 * provisions of LGPL are applicable instead of those above.  If you wish to
 * allow use of your version of this file only under the terms of the LGPL
 * License and not to allow others to use your version of this file under
 * the MPL, indicate your decision by deleting the provisions above and
 * replace them with the notice and other provisions required by the LGPL.
 * If you do not delete the provisions above, a recipient may use your version
 * of this file under either the MPL or the GNU LIBRARY GENERAL PUBLIC LICENSE.
 *
 * This library is free software; you can redistribute it and/or modify it
 * under the terms of the MPL as stated above or under the terms of the GNU
 * Library General Public License as published by the Free Software Foundation;
 * either version 2 of the License, or any later version.
 *
 * This library is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Library general Public License for more
 * details.
 */
namespace iTextSharp.text.factories {

    /**
    * This class can produce String combinations representing a number built with
    * Greek letters (from alpha to omega, then alpha alpha, alpha beta, alpha gamma).
    * We are aware of the fact that the original Greek numbering is different;
    * See http://www.cogsci.indiana.edu/farg/harry/lan/grknum.htm#ancient
    * but this isn't implemented yet; the main reason being the fact that we
    * need a font that has the obsolete Greek characters qoppa and sampi.
    */
    public class GreekAlphabetFactory {
        /** 
        * Changes an int into a lower case Greek letter combination.
        * @param index the original number
        * @return the letter combination
        */
        public static String GetString(int index) {
            return GetString(index, true);
        }
        
        /** 
        * Changes an int into a lower case Greek letter combination.
        * @param index the original number
        * @return the letter combination
        */
        public static String GetLowerCaseString(int index) {
            return GetString(index);        
        }
        
        /** 
        * Changes an int into a upper case Greek letter combination.
        * @param index the original number
        * @return the letter combination
        */
        public static String GetUpperCaseString(int index) {
            return GetString(index).ToUpper(System.Globalization.CultureInfo.InvariantCulture);
        }

        /** 
        * Changes an int into a Greek letter combination.
        * @param index the original number
        * @return the letter combination
        */
        public static String GetString(int index, bool lowercase) {
            if (index < 1) return "";
            index--;
                
            int bytes = 1;
            int start = 0;
            int symbols = 24;  
            while (index >= symbols + start) {
                bytes++;
                start += symbols;
                symbols *= 24;
            }
                  
            int c = index - start;
            char[] value = new char[bytes];
            while (bytes > 0) {
                bytes--;
                value[bytes] = (char)(c % 24);
                if (value[bytes] > 16) value[bytes]++;
                value[bytes] += (char)(lowercase ? 945 : 913);
                value[bytes] = SpecialSymbol.GetCorrespondingSymbol(value[bytes]);
                c /= 24;
            }
            
            return new String(value);
        }
    }
}