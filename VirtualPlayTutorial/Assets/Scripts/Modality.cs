using System;


[Flags]
public enum Modality 
{
    SIGHT = 1 << 0,
    SOUND = 2 << 1,
    TOUCH = 4 << 2,
    SMELL = 8 << 3
}
