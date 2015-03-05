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
using System.Text;
using System.Runtime.InteropServices; // needed to import from dll

// #pragma warning disable 0649

namespace IGE.Platform.Win32 {
	public static partial class ALC {
		public partial class Delegates {
			// GetProcAddress MUST be first to get imported!
			[RuntimeImport("openal32")]
			[System.Security.SuppressUnmanagedCodeSecurity()]
			public delegate IntPtr alcGetProcAddress(IntPtr device, String lpszProc);
			public static alcGetProcAddress GetProcAddress;
			
			
		
			[RuntimeImport("openal32")]
			public delegate bool alcIsExtensionPresent(IntPtr device, string extname);
			public static alcIsExtensionPresent IsExtensionPresent;
			
			[RuntimeImport("openal32")]
			public delegate IntPtr alcOpenDevice(string device_name);
			public static alcOpenDevice OpenDevice;
			
			[RuntimeImport("openal32")]
			public delegate bool alcCloseDevice(IntPtr device);
			public static alcCloseDevice CloseDevice;
			
			[RuntimeImport("openal32")]
			public delegate IntPtr alcGetString(IntPtr device, AlcStringName name);
			public static alcGetString GetString;

			[RuntimeImport("openal32")]
			[System.Security.SuppressUnmanagedCodeSecurity()]
			public unsafe delegate void alcGetIntegerv(IntPtr device, AlcGetIntegerName pname, int size, [OutAttribute] int* @params);
			public unsafe static alcGetIntegerv GetIntegerv;



			[RuntimeImport("openal32")]
			[System.Security.SuppressUnmanagedCodeSecurity()]
			public unsafe delegate IntPtr alcCreateContext(IntPtr device, int* attrlist);
			public unsafe static alcCreateContext CreateContext;
			
			[RuntimeImport("openal32")]
			public delegate bool alcMakeContextCurrent(IntPtr context);
			public static alcMakeContextCurrent MakeContextCurrent;
			
			[RuntimeImport("openal32")]
			public delegate void alcDestroyContext(IntPtr context);
			public static alcDestroyContext DestroyContext;
		}
	}
}
