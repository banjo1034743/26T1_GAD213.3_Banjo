using System;

/// <summary>
/// A port of the LoDMath static member class written in AS3 under the MIT license agreement.
/// 
/// A collection of math functions that can be very useful for many things.
/// 
/// 
/// As per the license agrrement of the lodGameBox license agreement
/// 
/// Copyright (c) 2009 Dylan Engelman
///
///Permission is hereby granted, free of charge, to any person obtaining a copy
///of this software and associated documentation files (the "Software"), to deal
///in the Software without restriction, including without limitation the rights
///to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
///copies of the Software, and to permit persons to whom the Software is
///
namespace com.spacepuppy.Utils
{
    public static class MathFunctions
    {
        #region Methods

        /// <summary>
        /// FloorTo some place comparative to a 'base', default is 10 for decimal place
        ///
        /// 'place' is represented by the power applied to 'base' to get that place
        /// </summary>
        /// <param name="value"></param>
        /// <param name="place"></param>
        /// <param name="base"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static float FloorTo(float value, int place, uint @base)
        {
            if (place == 0)
            {
                //'if zero no reason going through the math hoops
                return (float)Math.Floor(value);
            }
            else
            {
                float p = (float)Math.Pow(@base, place);
                return (float)Math.Floor(value * p) / p;
            }
        }

        #endregion
    }
}