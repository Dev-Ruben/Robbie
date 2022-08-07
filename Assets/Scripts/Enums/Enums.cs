using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameMode{
    Normal,
    Random
}

public enum GameState { 
    GameStarted,
    GamePaused,
    FloorPlaying,
    FloorCompleted,
    BossStage,
    PlayerDied,
    InAction,
}

public enum InventoryState {
    Open,
    Closed
}

public enum WeaponMode { 
    HandMode,
    MeleeMode,
    RangedMode,
    MageMode,
}

public enum BiomeType { 
    Hills,
    Rock,
    Cave,
    Forest,
    Lava
}

public enum FloorType {
    BasicFloor,
    BossFloor,
}

public enum Boss { 
    SlimeKing,
    Golem,
    LordCattius

}

public enum CameraType{
    PlayerCamera,
    BossCamera
}

public enum TileType{
    CornerTopLeft,
    CornerTopRight,
    CornerDownLeft,
    CornerDownRight,
    InnerCornerTopLeft,
    InnerCornerTopRight,
    InnerCornerDownLeft,
    InnerCornerDownRight,
    SideTop,
    SideLeft,
    SideRight,
    SideDown,
    InnerBase,
    OuterBase

}

public enum StorageSize{
    InventorySize = 24,
    ArmorySize = 6,
    HotbarSize = 7,
    ChestSize = 6
}

public enum ChestState{
    Open,
    Closed
}

public enum ArmorySlots{
    HelmetSlot,
    BodySlot,
    LegginsSlot,
    BootsSlot,
    PrimaryWeaponSlot,
    SecondaryWeaponSlot
}