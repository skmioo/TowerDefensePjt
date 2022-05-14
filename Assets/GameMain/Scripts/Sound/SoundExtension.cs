﻿using GameFramework.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDF.Data;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public static class SoundExtension
	{

		private const float FadeVolumeDuration = 1f;
		private static int? s_MusicSerialId = null;

		public static int? PlayMusic(this SoundComponent soundComponent, EnumSound enumSound, object userData = null)
		{
			if (enumSound == EnumSound.None)
				return null;

			soundComponent.StopMusic();
			s_MusicSerialId = soundComponent.PlaySound((int)enumSound, null, userData);

			return s_MusicSerialId;
		}


		public static void StopMusic(this SoundComponent soundComponent)
		{
			if (!s_MusicSerialId.HasValue)
			{
				return;
			}

			soundComponent.StopSound(s_MusicSerialId.Value, 0);
			s_MusicSerialId = null;
		}

		public static int? PlaySound(this SoundComponent soundComponent, EnumSound enumSound, Entity bindingEntity = null, object userData = null)
		{
			if (enumSound == EnumSound.None)
				return null;

			return soundComponent.PlaySound((int)enumSound, bindingEntity, userData);
		}

		public static int? PlaySound(this SoundComponent soundComponent, int soundId, Entity bindingEntity = null, object userData = null)
		{
			SoundData soundData = GameEntry.Data.GetData<DataSound>().GetSoundDataBySoundId(soundId);

			PlaySoundParams playSoundParams = PlaySoundParams.Create();
			playSoundParams.Time = soundData.SoundPlayParam.Time;
			playSoundParams.MuteInSoundGroup = soundData.SoundPlayParam.Mute;
			playSoundParams.Loop = soundData.SoundPlayParam.Loop;
			playSoundParams.Priority = soundData.SoundPlayParam.Priority;
			playSoundParams.VolumeInSoundGroup = soundData.SoundPlayParam.Volume;
			playSoundParams.FadeInSeconds = soundData.SoundPlayParam.FadeInSeconds;
			playSoundParams.Pitch = soundData.SoundPlayParam.Pitch;
			playSoundParams.PanStereo = soundData.SoundPlayParam.PanStereo;
			playSoundParams.SpatialBlend = soundData.SoundPlayParam.SpatialBlend;
			playSoundParams.MaxDistance = soundData.SoundPlayParam.MaxDistance;
			playSoundParams.DopplerLevel = soundData.SoundPlayParam.DopplerLevel;

			return soundComponent.PlaySound(soundData.AssetPath, soundData.SoundGroupData.Name, Constant.AssetPriority.MusicAsset, playSoundParams, bindingEntity, userData);
		}


	}
}