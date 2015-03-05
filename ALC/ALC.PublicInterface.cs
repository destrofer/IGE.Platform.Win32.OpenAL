/*
 * Author: Viacheslav Soroka
 * 
 * This file is part of IGE <https://github.com/destrofer/IGE>.
 * 
 * IGE is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * IGE is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with IGE.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IGE.Platform.Win32 {
	public static partial class ALC {
	
		public static IntPtr GetProcAddress(IntPtr device, string lpszProc) {
			return Delegates.GetProcAddress == null ? IntPtr.Zero : Delegates.GetProcAddress(device, lpszProc);
		}
		
		
	
		public static bool IsExtensionPresent(string extname) {
			return Delegates.IsExtensionPresent(IntPtr.Zero, extname);
		}
	
		public static bool IsExtensionPresent(IntPtr device, string extname) {
			return Delegates.IsExtensionPresent(device, extname);
		}
	
		public static IntPtr OpenDevice(string device_name) {
			return Delegates.OpenDevice(device_name);
		}
	
		public static bool CloseDevice(IntPtr device) {
			return Delegates.CloseDevice(device);
		}
		
		public static List<string> GetStrings(AlcStringArrayName name) {
			List<string> ret = new List<string>();
			string str;
 			unsafe {
				sbyte *strlist = (sbyte *)Delegates.GetString(IntPtr.Zero, (AlcStringName)name);
 				if( strlist == null )
 					return null;
 				while( *strlist != 0 ) {
 					str = new string(strlist);
 					ret.Add(str);
 					strlist += str.Length + 1;
 				}
			}
 			return ret;
		}
		
		public static string GetString(AlcStringName name) {
 			unsafe { return new string((sbyte*)Delegates.GetString(IntPtr.Zero, name)); }		
		}
		
		public static string GetString(IntPtr device, AlcStringName name) {
 			unsafe { return new string((sbyte*)Delegates.GetString(device, name)); }		
		}
		
		/// <summary>
		/// Receives integer information for a specific device
		/// </summary>
		/// <param name="device">Device handle</param>
		/// <param name="pname">Parameter name constant</param>
		/// <param name="params">Array to be filled with integer values</param>
		public static void GetInteger(IntPtr device, AlcGetIntegerName iname, int max_count, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Delegates.GetIntegerv(device, iname, max_count * sizeof(int), (int*)ptr); } }
        }

        public static void GetInteger(IntPtr device, AlcGetIntegerName iname, [OutAttribute] out int int_val) {
            unsafe { fixed (int* ptr = &int_val) { Delegates.GetIntegerv(device, iname, sizeof(int), (int*)ptr); } }
        }

        public static int GetInteger(IntPtr device, AlcGetIntegerName iname) {
        	int retVal;
            unsafe { Delegates.GetIntegerv(device, iname, sizeof(int), &retVal); }
            return retVal;
        }

        public static unsafe void GetInteger(IntPtr device, AlcGetIntegerName iname, int size, [OutAttribute] int* int_ptr) {
			Delegates.GetIntegerv(device, iname, size, (int *)int_ptr);
        }
        
        
        
		public static IntPtr CreateContext(IntPtr device) {
			unsafe { return Delegates.CreateContext(device, (int *)null); }
        }
        
		public static IntPtr CreateContext(IntPtr device, int[] attribs) {
            unsafe { fixed (int* ptr = attribs) { return Delegates.CreateContext(device, (int*)ptr); } }
        }
        
		public static bool MakeContextCurrent(IntPtr context) {
			return Delegates.MakeContextCurrent(context);
        }
        
		public static void DestroyContext(IntPtr context) {
			Delegates.DestroyContext(context);
        }
	}
}
