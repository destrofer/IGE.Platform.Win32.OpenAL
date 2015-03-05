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

using IGE.Audio;

// #pragma warning disable 0649

namespace IGE.Platform.Win32 {
	public class OpenALExternals {
		public OpenALExternals(OpenALContext context) {
			IGE.Platform.API.RuntimeImport(this, GetProcAddressInternal, null);
		}
		
		private IntPtr GetProcAddressInternal(string lpszProc, object param) {
			return (GetProcAddress != null) ? GetProcAddress(lpszProc) : IntPtr.Zero; 
		}
		
		// GetProcAddress MUST be first to get imported!
		[RuntimeImport("openal32")]
		public delegate IntPtr alGetProcAddress(String lpszProc);
		public alGetProcAddress GetProcAddress;
		
		
		
		[RuntimeImport("openal32")]
		public delegate void alEnable(AlEnableCap capability);
		public alEnable Enable;
		
		[RuntimeImport("openal32")]
		public delegate void alDisable(AlEnableCap capability);
		public alDisable Disable;
		
		[RuntimeImport("openal32")]
		public delegate bool alIsEnabled(AlEnableCap capability);
		public alIsEnabled IsEnabled;



		[RuntimeImport("openal32")]
		public delegate IntPtr alGetString(AlStringName name);
		public alGetString GetString;

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGetBooleanv(AlGetPName pname, [OutAttribute] bool *@params);
		public unsafe alGetBooleanv GetBooleanv;

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGetIntegerv(AlGetPName pname, [OutAttribute] int *@params);
		public unsafe alGetIntegerv GetIntegerv;

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGetFloatv(AlGetPName pname, [OutAttribute] float *@params);
		public unsafe alGetFloatv GetFloatv;

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGetDoublev(AlGetPName pname, [OutAttribute] double *@params);
		public unsafe alGetDoublev GetDoublev;
		
		[RuntimeImport("openal32")]
		public delegate bool alGetBoolean(AlGetPName pname);
		public alGetBoolean GetBoolean;
		
		[RuntimeImport("openal32")]
		public delegate int alGetInteger(AlGetPName pname);
		public alGetInteger GetInteger;
		
		[RuntimeImport("openal32")]
		public delegate float alGetFloat(AlGetPName pname);
		public alGetFloat GetFloat;
		
		[RuntimeImport("openal32")]
		public delegate double alGetDouble(AlGetPName pname);
		public alGetDouble GetDouble;



		[RuntimeImport("openal32")]
		public delegate ALError alGetError();
		public alGetError GetError;
		
		
		
		[RuntimeImport("openal32")]
		public delegate bool alIsExtensionPresent(string extname);
		public alIsExtensionPresent IsExtensionPresent;

		#region SOURCES

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGenSources(int n, [OutAttribute] int *buffers);
		public unsafe alGenSources GenSources;
		
		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alDeleteSources(int n, [OutAttribute] int *buffers);
		public unsafe alDeleteSources DeleteSources;
		
		[RuntimeImport("openal32")]
		public delegate bool alIsSource(int id);
		public alIsSource IsSource;
		
		[RuntimeImport("openal32")]
		public delegate void alSourcef(int id, AlSourceFloatParamName pname, float val);
		public alSourcef Sourcef;
		
		[RuntimeImport("openal32")]
		public delegate void alSource3f(int id, AlSourceVectorParamName pname, float val1, float val2, float val3);
		public alSource3f Source3f;
		
		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alSourcefv(int id, AlSourceVectorParamName pname, float *val);
		public unsafe alSourcefv Sourcefv;
		
		[RuntimeImport("openal32")]
		public delegate void alSourcei(int id, AlSourceIntParamName pname, int val);
		public alSourcei Sourcei;
		
		[RuntimeImport("openal32")]
		public delegate void alSource3i(int id, AlSourceVectorParamName pname, int val1, int val2, int val3);
		public alSource3i Source3i;
		
		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alSourceiv(int id, AlSourceVectorParamName pname, int *val);
		public unsafe alSourceiv Sourceiv;
		
		[RuntimeImport("openal32")]
		public delegate void alSourcePlay(int id);
		public alSourcePlay SourcePlay;
		
		[RuntimeImport("openal32")]
		public delegate void alSourceStop(int id);
		public alSourceStop SourceStop;
		
		[RuntimeImport("openal32")]
		public delegate void alSourceRewind(int id);
		public alSourceRewind SourceRewind;
		
		[RuntimeImport("openal32")]
		public delegate void alSourcePause(int id);
		public alSourcePause SourcePause;
		
		#endregion SOURCES

		#region BUFFERS

		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alGenBuffers(int n, [OutAttribute] int *buffers);
		public unsafe alGenBuffers GenBuffers;
		
		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alDeleteBuffers(int n, [OutAttribute] int *buffers);
		public unsafe alDeleteBuffers DeleteBuffers;
		
		[RuntimeImport("openal32")]
		public delegate bool alIsBuffer(int id);
		public alIsBuffer IsBuffer;
		
		[RuntimeImport("openal32")]
		[System.Security.SuppressUnmanagedCodeSecurity()]
		public unsafe delegate void alBufferData(int n, AlBufferFormat format, void *data, int size, uint freqency);
		public unsafe alBufferData BufferData;
		
		#endregion BUFFERS
	}
}
