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
	public class SoundSource : ISoundSource {
		protected int m_Id;
		public int Id { get { return m_Id; } }
		public bool IsOpen { get { return m_Id != 0; } }		
		
		protected ALError m_LastError;
		public ALError LastError { get { return m_LastError; } }

		protected AudioDevice m_Device;
		public IAudioDevice Device { get { return m_Device; } }
		
		public SoundSource(AudioDevice device) {
			m_Device = device;
			m_Id = m_Device.Context.GenSource();
			if( !CheckLastError() )
				m_Id = 0;
		}
		
		~SoundSource() {
			Dispose();
		}
		
		public virtual void Dispose() {
			if( m_Id != 0 ) {
				m_Device.Context.DeleteSource(m_Id);
				m_Id = 0;
			}
			m_Device = null;
		}
		
		protected bool CheckLastError() {
			m_LastError = m_Device.Context.GetError();
			switch( m_LastError ) {
				case ALError.OutOfMemory: throw new OutOfMemoryException();
				case ALError.InvalidOperation: throw new InvalidOperationException();
			}
			return m_LastError == ALError.NoError;
		}
		
		
		public bool SetBuffer(ISoundBuffer buffer) {
			if( !(buffer is SoundBuffer) )
				throw new UserFriendlyException("Application tried to set non OpenAL sound buffer to OpenAL sound source", "Sound system error");
			if( buffer == null )
				m_Device.Context.Source(m_Id, AlSourceIntParamName.Buffer, (int)0);
			else
				m_Device.Context.Source(m_Id, AlSourceIntParamName.Buffer, buffer.Id);
			return CheckLastError();
		}
		
		public void Play() {
			if( !IsOpen )
				return;
			m_Device.Context.SourcePlay(m_Id);
			CheckLastError();
		}
		
		public void Stop() {
			if( !IsOpen )
				return;
			m_Device.Context.SourceStop(m_Id);
			CheckLastError();
		}
		
		public void Rewind() {
			if( !IsOpen )
				return;
			m_Device.Context.SourceRewind(m_Id);
			CheckLastError();
		}
		
		public void Pause() {
			if( !IsOpen )
				return;
			m_Device.Context.SourcePause(m_Id);
			CheckLastError();
		}
	}
}
