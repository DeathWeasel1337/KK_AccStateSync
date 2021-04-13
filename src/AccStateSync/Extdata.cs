﻿using MessagePack;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AccStateSync
{
	public partial class AccStateSync
	{
		[Serializable]
		[MessagePackObject]
		public class AccTriggerInfo
		{
			[Key("Slot")]
			public int Slot { get; set; }
			[Key("Kind")]
			public int Kind { get; set; } = -1;
			[Key("Group")]
			public string Group { get; set; } = "";
			[Key("State")]
			public List<bool> State { get; set; } = new List<bool>() { true, false, false, false };

			public AccTriggerInfo(int slot) { Slot = slot; }
		}

		[Serializable]
		[MessagePackObject]
		public class OutfitTriggerInfo
		{
			[Key("Index")]
			public int Index { get; set; }
			[Key("Parts")]
			public Dictionary<int, AccTriggerInfo> Parts { get; set; } = new Dictionary<int, AccTriggerInfo>();

			public OutfitTriggerInfo(int index) { Index = index; }
		}

		[Serializable]
		[MessagePackObject]
		public class VirtualGroupInfo
		{
			[Key("Kind")]
			public int Kind { get; set; }
			[Key("Group")]
			public string Group { get; set; }
			[Key("Label")]
			public string Label { get; set; }
			[Key("Secondary")]
			public bool Secondary { get; set; } = false;
			[Key("State")]
			public bool State { get; set; } = true;

			public VirtualGroupInfo(string group, int kind, string label = "")
			{
				Group = group;
				Kind = kind;
				if (label.IsNullOrEmpty())
				{
					if (kind > 9)
						label = group.Replace("custom_", "Custom ");
					else if (kind == 9)
					{
						label = Group;
						if (Constants.AccessoryParentNames.ContainsKey(Group))
							label = Constants.AccessoryParentNames[Group];
					}
				}
				Label = label;
			}
		}

		public static void CopySlotTriggerInfo(AccTriggerInfo CopySource, AccTriggerInfo CopyDestination)
		{
			CopyDestination.Slot = CopySource.Slot;
			CopyDestination.Kind = CopySource.Kind;
			CopyDestination.Group = CopySource.Group;
			CopyDestination.State = CopySource.State.ToList();
		}
	}
}
