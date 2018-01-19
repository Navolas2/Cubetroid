using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Linker : MonoBehaviour {
	[Header("Room Attributes")]
	public Room_Vision this_room_block;
	public Door_Controller this_left_door;
	public Door_Trigger this_left_trigger;
	
	public Door_Controller this_right_door;
	public Door_Trigger this_right_trigger;
	[Space(15)]
	[Header("Attached Rooms")]
	public Room_Linker Left_Room;
	public Room_Linker Right_Room;
	public List<Room_Linker> Extended_Rooms;

	// Use this for initialization
	void Start () {
		if (Left_Room != null) {
			this_left_trigger.connected_doors.Add (Left_Room.this_right_door);
			this_left_trigger.connected_room.Add (Left_Room.this_room_block);
		}
		if (Right_Room != null) {
			this_right_trigger.connected_doors.Add (Right_Room.this_left_door);
			this_right_trigger.connected_room.Add (Right_Room.this_room_block);
		}
		if (Extended_Rooms != null) {
			if (Extended_Rooms.Count > 0) {
				foreach (Room_Linker r_l in Extended_Rooms) {
					if (r_l != this) {
						this_room_block.Extended_Vision.Add (r_l.this_room_block);
					}
				}
			}
		}
	}

}
