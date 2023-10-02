using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0,0,0,0,0.15,0,0,0]")]
	public partial class PlayerInLobbyNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[2];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private ulong _Name;
		public event FieldEvent<ulong> NameChanged;
		public Interpolated<ulong> NameInterpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong Name
		{
			get { return _Name; }
			set
			{
				// Don't do anything if the value is the same
				if (_Name == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_Name = value;
				hasDirtyFields = true;
			}
		}

		public void SetNameDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_Name(ulong timestep)
		{
			if (NameChanged != null) NameChanged(_Name, timestep);
			if (fieldAltered != null) fieldAltered("Name", _Name, timestep);
		}
		[ForgeGeneratedField]
		private ulong _Classe;
		public event FieldEvent<ulong> ClasseChanged;
		public Interpolated<ulong> ClasseInterpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong Classe
		{
			get { return _Classe; }
			set
			{
				// Don't do anything if the value is the same
				if (_Classe == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_Classe = value;
				hasDirtyFields = true;
			}
		}

		public void SetClasseDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_Classe(ulong timestep)
		{
			if (ClasseChanged != null) ClasseChanged(_Classe, timestep);
			if (fieldAltered != null) fieldAltered("Classe", _Classe, timestep);
		}
		[ForgeGeneratedField]
		private int _PlayerNumber;
		public event FieldEvent<int> PlayerNumberChanged;
		public Interpolated<int> PlayerNumberInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int PlayerNumber
		{
			get { return _PlayerNumber; }
			set
			{
				// Don't do anything if the value is the same
				if (_PlayerNumber == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_PlayerNumber = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayerNumberDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_PlayerNumber(ulong timestep)
		{
			if (PlayerNumberChanged != null) PlayerNumberChanged(_PlayerNumber, timestep);
			if (fieldAltered != null) fieldAltered("PlayerNumber", _PlayerNumber, timestep);
		}
		[ForgeGeneratedField]
		private ulong _PlayerIcon;
		public event FieldEvent<ulong> PlayerIconChanged;
		public Interpolated<ulong> PlayerIconInterpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong PlayerIcon
		{
			get { return _PlayerIcon; }
			set
			{
				// Don't do anything if the value is the same
				if (_PlayerIcon == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_PlayerIcon = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayerIconDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_PlayerIcon(ulong timestep)
		{
			if (PlayerIconChanged != null) PlayerIconChanged(_PlayerIcon, timestep);
			if (fieldAltered != null) fieldAltered("PlayerIcon", _PlayerIcon, timestep);
		}
		[ForgeGeneratedField]
		private int _PlayerLevel;
		public event FieldEvent<int> PlayerLevelChanged;
		public Interpolated<int> PlayerLevelInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int PlayerLevel
		{
			get { return _PlayerLevel; }
			set
			{
				// Don't do anything if the value is the same
				if (_PlayerLevel == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_PlayerLevel = value;
				hasDirtyFields = true;
			}
		}

		public void SetPlayerLevelDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_PlayerLevel(ulong timestep)
		{
			if (PlayerLevelChanged != null) PlayerLevelChanged(_PlayerLevel, timestep);
			if (fieldAltered != null) fieldAltered("PlayerLevel", _PlayerLevel, timestep);
		}
		[ForgeGeneratedField]
		private bool _Ready;
		public event FieldEvent<bool> ReadyChanged;
		public Interpolated<bool> ReadyInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool Ready
		{
			get { return _Ready; }
			set
			{
				// Don't do anything if the value is the same
				if (_Ready == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_Ready = value;
				hasDirtyFields = true;
			}
		}

		public void SetReadyDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_Ready(ulong timestep)
		{
			if (ReadyChanged != null) ReadyChanged(_Ready, timestep);
			if (fieldAltered != null) fieldAltered("Ready", _Ready, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x40;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x40;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private ulong _VoteClasse1;
		public event FieldEvent<ulong> VoteClasse1Changed;
		public Interpolated<ulong> VoteClasse1Interpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong VoteClasse1
		{
			get { return _VoteClasse1; }
			set
			{
				// Don't do anything if the value is the same
				if (_VoteClasse1 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x80;
				_VoteClasse1 = value;
				hasDirtyFields = true;
			}
		}

		public void SetVoteClasse1Dirty()
		{
			_dirtyFields[0] |= 0x80;
			hasDirtyFields = true;
		}

		private void RunChange_VoteClasse1(ulong timestep)
		{
			if (VoteClasse1Changed != null) VoteClasse1Changed(_VoteClasse1, timestep);
			if (fieldAltered != null) fieldAltered("VoteClasse1", _VoteClasse1, timestep);
		}
		[ForgeGeneratedField]
		private ulong _VoteClasse2;
		public event FieldEvent<ulong> VoteClasse2Changed;
		public Interpolated<ulong> VoteClasse2Interpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong VoteClasse2
		{
			get { return _VoteClasse2; }
			set
			{
				// Don't do anything if the value is the same
				if (_VoteClasse2 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[1] |= 0x1;
				_VoteClasse2 = value;
				hasDirtyFields = true;
			}
		}

		public void SetVoteClasse2Dirty()
		{
			_dirtyFields[1] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_VoteClasse2(ulong timestep)
		{
			if (VoteClasse2Changed != null) VoteClasse2Changed(_VoteClasse2, timestep);
			if (fieldAltered != null) fieldAltered("VoteClasse2", _VoteClasse2, timestep);
		}
		[ForgeGeneratedField]
		private ulong _VoteClasse3;
		public event FieldEvent<ulong> VoteClasse3Changed;
		public Interpolated<ulong> VoteClasse3Interpolation = new Interpolated<ulong>() { LerpT = 0f, Enabled = false };
		public ulong VoteClasse3
		{
			get { return _VoteClasse3; }
			set
			{
				// Don't do anything if the value is the same
				if (_VoteClasse3 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[1] |= 0x2;
				_VoteClasse3 = value;
				hasDirtyFields = true;
			}
		}

		public void SetVoteClasse3Dirty()
		{
			_dirtyFields[1] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_VoteClasse3(ulong timestep)
		{
			if (VoteClasse3Changed != null) VoteClasse3Changed(_VoteClasse3, timestep);
			if (fieldAltered != null) fieldAltered("VoteClasse3", _VoteClasse3, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			NameInterpolation.current = NameInterpolation.target;
			ClasseInterpolation.current = ClasseInterpolation.target;
			PlayerNumberInterpolation.current = PlayerNumberInterpolation.target;
			PlayerIconInterpolation.current = PlayerIconInterpolation.target;
			PlayerLevelInterpolation.current = PlayerLevelInterpolation.target;
			ReadyInterpolation.current = ReadyInterpolation.target;
			positionInterpolation.current = positionInterpolation.target;
			VoteClasse1Interpolation.current = VoteClasse1Interpolation.target;
			VoteClasse2Interpolation.current = VoteClasse2Interpolation.target;
			VoteClasse3Interpolation.current = VoteClasse3Interpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _Name);
			UnityObjectMapper.Instance.MapBytes(data, _Classe);
			UnityObjectMapper.Instance.MapBytes(data, _PlayerNumber);
			UnityObjectMapper.Instance.MapBytes(data, _PlayerIcon);
			UnityObjectMapper.Instance.MapBytes(data, _PlayerLevel);
			UnityObjectMapper.Instance.MapBytes(data, _Ready);
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _VoteClasse1);
			UnityObjectMapper.Instance.MapBytes(data, _VoteClasse2);
			UnityObjectMapper.Instance.MapBytes(data, _VoteClasse3);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_Name = UnityObjectMapper.Instance.Map<ulong>(payload);
			NameInterpolation.current = _Name;
			NameInterpolation.target = _Name;
			RunChange_Name(timestep);
			_Classe = UnityObjectMapper.Instance.Map<ulong>(payload);
			ClasseInterpolation.current = _Classe;
			ClasseInterpolation.target = _Classe;
			RunChange_Classe(timestep);
			_PlayerNumber = UnityObjectMapper.Instance.Map<int>(payload);
			PlayerNumberInterpolation.current = _PlayerNumber;
			PlayerNumberInterpolation.target = _PlayerNumber;
			RunChange_PlayerNumber(timestep);
			_PlayerIcon = UnityObjectMapper.Instance.Map<ulong>(payload);
			PlayerIconInterpolation.current = _PlayerIcon;
			PlayerIconInterpolation.target = _PlayerIcon;
			RunChange_PlayerIcon(timestep);
			_PlayerLevel = UnityObjectMapper.Instance.Map<int>(payload);
			PlayerLevelInterpolation.current = _PlayerLevel;
			PlayerLevelInterpolation.target = _PlayerLevel;
			RunChange_PlayerLevel(timestep);
			_Ready = UnityObjectMapper.Instance.Map<bool>(payload);
			ReadyInterpolation.current = _Ready;
			ReadyInterpolation.target = _Ready;
			RunChange_Ready(timestep);
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_VoteClasse1 = UnityObjectMapper.Instance.Map<ulong>(payload);
			VoteClasse1Interpolation.current = _VoteClasse1;
			VoteClasse1Interpolation.target = _VoteClasse1;
			RunChange_VoteClasse1(timestep);
			_VoteClasse2 = UnityObjectMapper.Instance.Map<ulong>(payload);
			VoteClasse2Interpolation.current = _VoteClasse2;
			VoteClasse2Interpolation.target = _VoteClasse2;
			RunChange_VoteClasse2(timestep);
			_VoteClasse3 = UnityObjectMapper.Instance.Map<ulong>(payload);
			VoteClasse3Interpolation.current = _VoteClasse3;
			VoteClasse3Interpolation.target = _VoteClasse3;
			RunChange_VoteClasse3(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Name);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Classe);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _PlayerNumber);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _PlayerIcon);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _PlayerLevel);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Ready);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x80 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _VoteClasse1);
			if ((0x1 & _dirtyFields[1]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _VoteClasse2);
			if ((0x2 & _dirtyFields[1]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _VoteClasse3);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (NameInterpolation.Enabled)
				{
					NameInterpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					NameInterpolation.Timestep = timestep;
				}
				else
				{
					_Name = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_Name(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (ClasseInterpolation.Enabled)
				{
					ClasseInterpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					ClasseInterpolation.Timestep = timestep;
				}
				else
				{
					_Classe = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_Classe(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (PlayerNumberInterpolation.Enabled)
				{
					PlayerNumberInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					PlayerNumberInterpolation.Timestep = timestep;
				}
				else
				{
					_PlayerNumber = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_PlayerNumber(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (PlayerIconInterpolation.Enabled)
				{
					PlayerIconInterpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					PlayerIconInterpolation.Timestep = timestep;
				}
				else
				{
					_PlayerIcon = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_PlayerIcon(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (PlayerLevelInterpolation.Enabled)
				{
					PlayerLevelInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					PlayerLevelInterpolation.Timestep = timestep;
				}
				else
				{
					_PlayerLevel = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_PlayerLevel(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (ReadyInterpolation.Enabled)
				{
					ReadyInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					ReadyInterpolation.Timestep = timestep;
				}
				else
				{
					_Ready = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_Ready(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x80 & readDirtyFlags[0]) != 0)
			{
				if (VoteClasse1Interpolation.Enabled)
				{
					VoteClasse1Interpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					VoteClasse1Interpolation.Timestep = timestep;
				}
				else
				{
					_VoteClasse1 = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_VoteClasse1(timestep);
				}
			}
			if ((0x1 & readDirtyFlags[1]) != 0)
			{
				if (VoteClasse2Interpolation.Enabled)
				{
					VoteClasse2Interpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					VoteClasse2Interpolation.Timestep = timestep;
				}
				else
				{
					_VoteClasse2 = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_VoteClasse2(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[1]) != 0)
			{
				if (VoteClasse3Interpolation.Enabled)
				{
					VoteClasse3Interpolation.target = UnityObjectMapper.Instance.Map<ulong>(data);
					VoteClasse3Interpolation.Timestep = timestep;
				}
				else
				{
					_VoteClasse3 = UnityObjectMapper.Instance.Map<ulong>(data);
					RunChange_VoteClasse3(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (NameInterpolation.Enabled && !NameInterpolation.current.UnityNear(NameInterpolation.target, 0.0015f))
			{
				_Name = (ulong)NameInterpolation.Interpolate();
				//RunChange_Name(NameInterpolation.Timestep);
			}
			if (ClasseInterpolation.Enabled && !ClasseInterpolation.current.UnityNear(ClasseInterpolation.target, 0.0015f))
			{
				_Classe = (ulong)ClasseInterpolation.Interpolate();
				//RunChange_Classe(ClasseInterpolation.Timestep);
			}
			if (PlayerNumberInterpolation.Enabled && !PlayerNumberInterpolation.current.UnityNear(PlayerNumberInterpolation.target, 0.0015f))
			{
				_PlayerNumber = (int)PlayerNumberInterpolation.Interpolate();
				//RunChange_PlayerNumber(PlayerNumberInterpolation.Timestep);
			}
			if (PlayerIconInterpolation.Enabled && !PlayerIconInterpolation.current.UnityNear(PlayerIconInterpolation.target, 0.0015f))
			{
				_PlayerIcon = (ulong)PlayerIconInterpolation.Interpolate();
				//RunChange_PlayerIcon(PlayerIconInterpolation.Timestep);
			}
			if (PlayerLevelInterpolation.Enabled && !PlayerLevelInterpolation.current.UnityNear(PlayerLevelInterpolation.target, 0.0015f))
			{
				_PlayerLevel = (int)PlayerLevelInterpolation.Interpolate();
				//RunChange_PlayerLevel(PlayerLevelInterpolation.Timestep);
			}
			if (ReadyInterpolation.Enabled && !ReadyInterpolation.current.UnityNear(ReadyInterpolation.target, 0.0015f))
			{
				_Ready = (bool)ReadyInterpolation.Interpolate();
				//RunChange_Ready(ReadyInterpolation.Timestep);
			}
			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (VoteClasse1Interpolation.Enabled && !VoteClasse1Interpolation.current.UnityNear(VoteClasse1Interpolation.target, 0.0015f))
			{
				_VoteClasse1 = (ulong)VoteClasse1Interpolation.Interpolate();
				//RunChange_VoteClasse1(VoteClasse1Interpolation.Timestep);
			}
			if (VoteClasse2Interpolation.Enabled && !VoteClasse2Interpolation.current.UnityNear(VoteClasse2Interpolation.target, 0.0015f))
			{
				_VoteClasse2 = (ulong)VoteClasse2Interpolation.Interpolate();
				//RunChange_VoteClasse2(VoteClasse2Interpolation.Timestep);
			}
			if (VoteClasse3Interpolation.Enabled && !VoteClasse3Interpolation.current.UnityNear(VoteClasse3Interpolation.target, 0.0015f))
			{
				_VoteClasse3 = (ulong)VoteClasse3Interpolation.Interpolate();
				//RunChange_VoteClasse3(VoteClasse3Interpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[2];

		}

		public PlayerInLobbyNetworkObject() : base() { Initialize(); }
		public PlayerInLobbyNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerInLobbyNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
