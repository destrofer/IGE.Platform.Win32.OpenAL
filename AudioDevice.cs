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

using IGE.Audio;

namespace IGE.Platform.Win32 {
	public class AudioDevice : IAudioDevice {
		protected IntPtr m_Handle;
		public IntPtr Handle { get { return m_Handle; } }
		public bool IsOpen { get { return m_Handle != IntPtr.Zero; } }
		
		protected string m_Name;
		public string Name { get { return m_Name; } }
		
		protected OpenALContext m_Context;
		public OpenALContext Context { get { return m_Context; } }
		public bool HasContext { get { return m_Context != null && m_Context.Exists; } }
		
		public int MajorVersion { get { return IsOpen ? ALC.GetInteger(m_Handle, AlcGetIntegerName.MajorVersion) : 0; } }
		public int MinorVersion { get { return IsOpen ? ALC.GetInteger(m_Handle, AlcGetIntegerName.MinorVersion) : 0; } }
		public bool SupportsEFX { get { return IsOpen && ALC.IsExtensionPresent(m_Handle, "ALC_EXT_EFX"); } }
		
		// for now make a single context per device, later maybe will move context into a separate class
		
		private SoundListener m_Listener;
		public ISoundListener Listener { get { return m_Listener; } }
		
		public AudioDevice(string device_name) {
			m_Handle = IntPtr.Zero;
			m_Name = device_name;
			m_Context = null;
			m_Listener = null;
		}
		
		public static string[] GetAvailableDevices() {
			if( ALC.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT") )
				return ALC.GetStrings(AlcStringArrayName.AllDevicesSpecifier).ToArray();
			if( ALC.IsExtensionPresent("ALC_ENUMERATION_EXT") )
				return ALC.GetStrings(AlcStringArrayName.DeviceSpecifier).ToArray();
			return new string[] { "" };
		}
		
		public static string GetDefaultDevice() {
			if( ALC.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT") )
				return ALC.GetString(AlcStringName.DefaultAllDevicesSpecifier);
			if( ALC.IsExtensionPresent("ALC_ENUMERATION_EXT") )
				return ALC.GetString(AlcStringName.DefaultDeviceSpecifier);
			return "";
		}
		
		/// <summary>
		/// Opens a default audio rendering device
		/// </summary>
		/// <returns>true on success, false otherwise</returns>
		public virtual void Initialize() {
			if( IsOpen )
				return;

			m_Handle = ALC.OpenDevice(m_Name);
			if( !IsOpen )
				throw new UserFriendlyException(String.Format("Failed to open audio device '{0}'", m_Name), "Failed to initialize audio");
			
			// ALC.RuntimeImport(m_Handle); // reimport because some functions may be device specific
			if( !CreateContext() ) {
				Close();
				throw new UserFriendlyException(String.Format("Failed to create audio context for device '{0}'", m_Name), "Failed to initialize audio");
			}

			if( !MakeContextCurrent() ) {
				Close();
				throw new UserFriendlyException(String.Format("Failed to switch to audio context of device '{0}'", m_Name), "Failed to initialize audio");
			}
			
			m_Listener = new SoundListener(this);
		}
		
		public virtual bool CreateContext() {
			if( m_Handle == IntPtr.Zero )
				return false;
			if( m_Context != null )
				m_Context.Dispose();
			m_Context = new OpenALContext(this);
			return m_Context.Exists;
		}
		
		public virtual bool MakeContextCurrent() {
			if( m_Context == null )
				return false;
			return m_Context.MakeCurrent();
		}
		
		public virtual void CloseContext() {
			if( m_Context != null ) {
				m_Context.Dispose();
				m_Context = null;
			}
		}
		
		public virtual void Close() {
			if( m_Handle != IntPtr.Zero ) {
				CloseContext();
				ALC.CloseDevice(m_Handle);
				m_Handle = IntPtr.Zero;
				m_Name = "";
			}
		}
		
		~AudioDevice() {
			Dispose();
		}
		
		public virtual void Dispose() {
			Close();
		}
		
		public virtual ISoundBuffer CreateBuffer() {
			return new SoundBuffer(this);
		}
		
		public virtual ISoundSource CreateSource() {
			return new SoundSource(this);
		}
	}
}
