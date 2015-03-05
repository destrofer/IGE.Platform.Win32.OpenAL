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
using IGE;
using IGE.Platform;

namespace IGE.Platform.Win32 {
	public static partial class ALC {
		static ALC() {
			RuntimeImport();
		}
		
		public static void EnsureLoaded() {
		}
		
		public static void RuntimeImport() {
			IGE.Platform.API.RuntimeImport(typeof(IGE.Platform.Win32.ALC.Delegates), GetProcAddressInternal, IntPtr.Zero);
		}
		
		public static void RuntimeImport(IntPtr device) {
			IGE.Platform.API.RuntimeImport(typeof(IGE.Platform.Win32.ALC.Delegates), GetProcAddressInternal, device);
		}
		
		private static IntPtr GetProcAddressInternal(string lpszProc, object param) {
			return GetProcAddress((IntPtr)param, lpszProc);
		}
	}	
}
