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

namespace IGE.Platform.Win32 {
	public enum AlcStringName : int {
		MajorVersion = 0x1000,
		MinorVersion = 0x1001,
		
		AttributesSize = 0x1002,
		AllAttributes = 0x1003,
		
		DefaultDeviceSpecifier = 0x1004,
		Extensions = 0x1006,
		
		DefaultAllDevicesSpecifier = 0x1012,
	}
	
	public enum AlcStringArrayName : int {
		DeviceSpecifier = 0x1005,
		AllDevicesSpecifier = 0x1013,
	}
	
	public enum AlcGetIntegerName : int {
		MajorVersion = 0x1000,
		MinorVersion = 0x1001,
	}
}
