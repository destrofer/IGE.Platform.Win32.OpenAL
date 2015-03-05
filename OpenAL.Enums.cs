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
	public enum ALError : int {
		NoError = 0,
		InvalidName = 0xA001,
		InvalidEnum = 0xA002,
		InvalidValue = 0xA003,
		InvalidOperation = 0xA004,
		OutOfMemory = 0xA005,
	}
	
	public enum AlEnableCap : int {
	
	}
	
	public enum AlStringName : int {
		Vendor = 0xB001,
		Version = 0xB002,
		Renderer = 0xB003,
		Extensions = 0xB004,
	}
	
	public enum AlStringArrayName : int {
	}
	
	public enum AlGetPName : int {
	
	}
	
	public enum AlSourceFloatParamName : int {
		Gain = 0x100A,
		MinGain = 0x100D,
		MaxGain = 0x100E,
		ReferenceDistance = 0x1020,
		RollOffFactor = 0x1021,
		MaxDistance = 0x1023,
		ConeInnerAngle = 0x1001,
		ConeOuterAngle = 0x1002,
		ConeOuterGain = 0x1022,
		Pitch = 0x1003,
		SecondsOffset = 0x1024,
		SampleOffset = 0x1025,
		ByteOffset = 0x1026,
	}
	
	public enum AlSourceIntParamName : int {
		SourceRelative = 0x202,
		ConeInnerAngle = AlSourceFloatParamName.ConeInnerAngle,
		ConeOuterAngle = AlSourceFloatParamName.ConeOuterAngle,
		ConeOuterGain = AlSourceFloatParamName.ConeOuterGain,
		Looping = 0x1007,
		MillisecondsOffset = AlSourceFloatParamName.SecondsOffset,
		SampleOffset = AlSourceFloatParamName.SampleOffset,
		ByteOffset = AlSourceFloatParamName.ByteOffset,
		Buffer = 0x1009,
		SourceState = 0x1010,
		Initial = 0x1011,
		Playing = 0x1012,
		Paused = 0x1013,
		Stopped = 0x1014,
		BuffersQueued = 0x1015,
		BuffersProcessed = 0x1016,
	}
	
	public enum AlSourceVectorParamName : int {
		Position = 0x1004,
		Direction = 0x1005,
		Velocity = 0x1006,
	}
	
	public enum AlListenerVectorParamName : int {
		Position = 0x1004,
		Direction = 0x1005,
		Velocity = 0x1006,
		Orientation = 0x100F,
	}
	
	public enum AlBufferFormat : int {
		Mono8 = 0x1100,
		Mono16 = 0x1101,
		Stereo8 = 0x1102,
		Stereo16 = 0x1103,
	}
}
