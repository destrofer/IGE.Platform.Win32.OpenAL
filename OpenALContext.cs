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
using System.Collections.Generic;
using System.Runtime.InteropServices;

using IGE;
using IGE.Platform;

namespace IGE.Platform.Win32 {
	public partial class OpenALContext : IDisposable {
		protected IntPtr m_Handle;
		public IntPtr Handle { get { return m_Handle; } }
		public bool Exists { get { return m_Handle != IntPtr.Zero; } }
		
		private AudioDevice m_Device;
		public AudioDevice Device { get { return m_Device; } }
		
		public readonly OpenALExternals Externals; 
		
		public OpenALContext(AudioDevice device) {
			m_Device = device;
			m_Handle = ALC.CreateContext(device.Handle);
			MakeCurrent();
			Externals = new OpenALExternals(this);
		}
		
		~OpenALContext() {
			Dispose();
		}
		
		public virtual void Dispose() {
			if( m_Handle != IntPtr.Zero ) {
				ALC.MakeContextCurrent(IntPtr.Zero);
				ALC.DestroyContext(m_Handle);
				m_Handle = IntPtr.Zero;
			}
			m_Device = null;
		}
		
		public virtual bool MakeCurrent() {
			if( m_Handle == IntPtr.Zero )
				return false;
			return ALC.MakeContextCurrent(m_Handle);
		}
		
		public ALError GetError() {
			return Externals.GetError();
		}

		public bool IsExtensionPresent(string extname) {
			return Externals.IsExtensionPresent(extname);
		}
	
		public List<string> GetStrings(AlStringArrayName name) {
			List<string> ret = new List<string>();
			string str;
 			unsafe {
				sbyte *strlist = (sbyte *)Externals.GetString((AlStringName)name);
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
		
		public string GetString(AlStringName name) {
 			unsafe { return new string((sbyte*)Externals.GetString(name)); }		
		}
		/*
		/// <summary>
		/// Receives integer information for a specific device
		/// </summary>
		/// <param name="device">Device handle</param>
		/// <param name="pname">Parameter name constant</param>
		/// <param name="params">Array to be filled with integer values</param>
		public static void GetInteger(IntPtr device, AlcGetIntegerName iname, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Externals.GetIntegerv(device, iname, int_array.Length * sizeof(int), (int*)ptr); } }
        }

        public static void GetInteger(IntPtr device, AlcGetIntegerName iname, [OutAttribute] out int int_val) {
            unsafe { fixed (int* ptr = &int_val) { Externals.GetIntegerv(device, iname, sizeof(int), (int*)ptr); } }
        }

        public static int GetInteger(IntPtr device, AlcGetIntegerName iname) {
        	int retVal;
            unsafe { Externals.GetIntegerv(device, iname, sizeof(int), &retVal); }
            return retVal;
        }

        public static unsafe void GetInteger(IntPtr device, AlcGetIntegerName iname, int size, [OutAttribute] int* int_ptr) {
			Externals.GetIntegerv(device, iname, size, (int *)int_ptr);
        }
        */
        
        #region SOURCES        
        
		public void GenSources(int count, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Externals.GenSources(count, (int*)ptr); } }
        }

        public int GenSource() {
        	int retVal = 0;
        	unsafe { Externals.GenSources(1, (int *)&retVal); }
            return retVal;
        }
        
		public void DeleteSources(int count, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Externals.DeleteSources(count, (int*)ptr); } }
        }

        public void DeleteSource(int buffer) {
       		unsafe { Externals.DeleteSources(1, (int *)&buffer); }
        }
        
        public bool IsSource(int id) {
        	return Externals.IsSource(id);
        }
        
        public void Source(int id, AlSourceFloatParamName pname, float val) {
        	Externals.Sourcef(id, pname, val);
        }
        
        public void Source(int id, AlSourceVectorParamName pname, float x, float y, float z) {
        	Externals.Source3f(id, pname, x, y, z);
        }
        
        public void Source(int id, AlSourceVectorParamName pname, float[] values) {
        	unsafe { fixed(float *ptr = values) { Externals.Sourcefv(id, pname, ptr); } }        	
        }
        
        public void Source(int id, AlSourceIntParamName pname, int val) {
        	Externals.Sourcei(id, pname, val);
        }
        
        public void Source(int id, AlSourceVectorParamName pname, int x, int y, int z) {
        	Externals.Source3i(id, pname, x, y, z);
        }
        
        public void Source(int id, AlSourceVectorParamName pname, int[] values) {
        	unsafe { fixed(int *ptr = values) { Externals.Sourceiv(id, pname, ptr); } }        	
        }
        
        public void SourcePlay(int id) {
        	Externals.SourcePlay(id);
        }
        
        public void SourceStop(int id) {
        	Externals.SourceStop(id);
        }
        
        public void SourceRewind(int id) {
        	Externals.SourceRewind(id);
        }
        
        public void SourcePause(int id) {
        	Externals.SourcePause(id);
        }
        
        #endregion SOURCES
        
        #region BUFFERS
        
		public void GenBuffers(int count, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Externals.GenBuffers(count, (int*)ptr); } }
        }

        public int GenBuffer() {
        	int retVal = 0;
        	unsafe { Externals.GenBuffers(1, (int *)&retVal); }
            return retVal;
        }
        
		public void DeleteBuffers(int count, [OutAttribute] int[] int_array) {
            unsafe { fixed (int* ptr = int_array) { Externals.DeleteBuffers(count, (int*)ptr); } }
        }

        public void DeleteBuffer(int buffer) {
       		unsafe { Externals.DeleteBuffers(1, (int *)&buffer); }
        }
        
        public bool IsBuffer(int id) {
        	return Externals.IsBuffer(id);
        }
        
      	public void BufferData(int buffer_id, bool stereo, byte[] data, uint freqency) {
       		unsafe { fixed (void* ptr = data) { Externals.BufferData(buffer_id, stereo ? AlBufferFormat.Stereo8 : AlBufferFormat.Mono8, ptr, data.Length, freqency); } }
        }
        
		public void BufferData(int buffer_id, bool stereo, short[] data, uint freqency) {
       		unsafe { fixed (void* ptr = data) { Externals.BufferData(buffer_id, stereo ? AlBufferFormat.Stereo16 : AlBufferFormat.Mono16, ptr, data.Length * sizeof(short), freqency); } }
        }
        
        #endregion BUFFERS
	}	
}