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
using System.Reflection;

using IGE.Audio;

namespace IGE.Platform.Win32 {
	/// <summary>
	/// </summary>
	public sealed partial class OpenAL : IAudioDriver {
		public string DriverName { get { return "OpenAL"; } }
		public Version DriverVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
		public bool IsSupported { get { return true; } }

		internal static OpenAL Instance = null;
		public static OpenAL GetInstance() {
			if( Instance != null )
				return Instance;
			return Instance = new OpenAL();
		}

		private OpenAL() {
		}
		
		public bool Initialize() {
			RescanDevices();
			return true;
		}
		
		public bool Test() {
			return true;
		}
		
		
		private AudioDevice[] m_AudioDevices = null;
		private AudioDevice m_PrimaryPlaybackDevice = null;
		private AudioDevice m_PrimaryRecordingDevice = null;
		
		public IAudioDevice[] AudioDevices { get { return m_AudioDevices; } }
		
		public IAudioDevice PrimaryPlaybackDevice { get { return m_PrimaryPlaybackDevice; } }
		public IAudioDevice PrimaryRecordingDevice { get { return m_PrimaryRecordingDevice; } }

		public void RescanDevices() {
			Dictionary<string, AudioDevice> currentDevices = new Dictionary<string, AudioDevice>();
			Dictionary<string, AudioDevice> devices = new Dictionary<string, AudioDevice>();
			AudioDevice device;

			string[] deviceNames = AudioDevice.GetAvailableDevices();
			string defaultDeviceName = AudioDevice.GetDefaultDevice();
			
			if( m_AudioDevices != null ) {
				foreach( AudioDevice dev in m_AudioDevices )
					currentDevices.Add(dev.Name, dev);
			}
			
			foreach( string deviceName in deviceNames ) {
				if( currentDevices.TryGetValue(deviceName, out device) ) {
					devices.Add(device.Name, device);
					currentDevices.Remove(device.Name);
				}
				else
					devices.Add(deviceName, new AudioDevice(deviceName));
			}
			
			foreach( AudioDevice dev in currentDevices.Values )
				dev.Dispose();

			m_AudioDevices = new AudioDevice[devices.Count];
			
			if( devices.Count > 0 ) {
				devices.Values.CopyTo(m_AudioDevices, 0);
				
				if( devices.TryGetValue(defaultDeviceName, out device) )
					m_PrimaryPlaybackDevice = m_PrimaryRecordingDevice = device;
				else
					m_PrimaryPlaybackDevice = m_PrimaryRecordingDevice = m_AudioDevices[0];
			}
			else {
				m_PrimaryPlaybackDevice = m_PrimaryRecordingDevice = null;
			}
		}
	}
}