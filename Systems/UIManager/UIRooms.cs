//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class UIRooms : UIElement
//{
//    public Room room;

//    //public Scrollbar scrollbar;

//    [Range(0f,1f)]
//    public float value;

//    public override Type DerivedType()
//    {
//        return GetType();
//    }

//    // Use this for initialization
//    void Start()
//    {
//        //RoomProperties roomProperties = DB.titleDataProvider.roomProperties[TitleDataProvider.beginner];

//        RoomProperties roomProperties = new RoomProperties() { cost = 500, winxp = 100, rule = 45, drawxp = 25, losexp = 0 };

//        room.UpdateProperties(roomProperties);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //scrollbar.value = value;
//    }

//    [Serializable]
//    public class Room
//    {
//        public Text prize;
//        public Text fee;
//        public Text rule;
//        public Text exp;


//        public void UpdateProperties( RoomProperties roomProperties )
//        {
            
//            prize.text = string.Format("{0:N0}", (roomProperties.cost * 2));
//            fee.text = string.Format("{0:N0}", roomProperties.cost);
//            rule.text = "Rules : " + roomProperties.rule;
//            exp.text = roomProperties.winxp.ToString();
//        }

//        //public int cost;
//        //public int rule;
//        //public int winxp;
//        //public int losexp;
//        //public int drawxp;
//    }


//}
